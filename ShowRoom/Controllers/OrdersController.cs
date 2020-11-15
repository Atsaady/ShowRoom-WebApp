using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShowRoom.Data;
using ShowRoom.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace ShowRoom.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShowRoom.Data.ShowRoomContext _context;

        public OrdersController(ShowRoom.Data.ShowRoomContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Ordered()
        {
            return View();
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [Authorize]
        public IActionResult Create()
        {

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (userEmail == null)

                return RedirectToAction("Login", "Account");
            else
            {
                var cart = _context.Cart.Include(p => p.Clothings).Where(x=>x.account.Email== userEmail).ToList();

                double totalPrice = 0;
                foreach (var cartItem in cart)
                {
                    totalPrice += cartItem.Clothings.Price * cartItem.Count;
                }

                ViewData["Message"] = totalPrice;
                ViewData["cart_to_view"] = cart;
                return View();
        
            }
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Address,OrderTime,Phone,SumToPay")] Order order)
        {

            order.OrderTime = DateTime.Now;
            order.SumToPay = 0; // TODO - DEAN!!!
            order.ClothingOrders = new List<ClothingOrders>();
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            Account account = _context.Account.First(s => s.Email == userEmail);
            order.Account = account;
            var cart = _context.Cart.Include(p => p.Clothings).Where(s => s.account == account);
            foreach (var item in cart)
            {
                order.ClothingOrders.Add(new ClothingOrders() { ProductId = item.Clothings.Id, OrderId = order.Id, Count = item.Count });
                order.SumToPay += item.Clothings.Price * item.Count;
            }

            _context.Cart.RemoveRange(_context.Cart.Where(p => p.account.Id == account.Id));
            _context.Add(order);
            await _context.SaveChangesAsync();
            String orderID = order.Id.ToString();
            ViewData["Message"] = orderID;
            return View("Ordered");
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,OrderTime,Phone,SumToPay")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
