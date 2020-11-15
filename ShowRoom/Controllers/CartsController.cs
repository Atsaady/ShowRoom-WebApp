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

namespace ShowRoom.Controllers
{
    public class CartsController : Controller
    {
        private readonly ShowRoomContext _context;

        public CartsController(ShowRoomContext context)
        {
            _context = context;
        }

        public double GetSumPayment()
        {
            double sum = 0;
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            Account account = _context.Account.First(s => s.Email == userEmail);

            var entryPoint = (from c in _context.Cart
                              join p in _context.Clothings on c.Clothings.Id equals p.Id
                              where c.account.Id == account.Id
                              select new
                              {
                                  price = c.Count * p.Price
                              }); ;

            foreach (var x in entryPoint)
                sum += x.price;
            return sum;
        }
        public async Task<IActionResult> Plus(int id)
        {


            var query = _context.Cart.Where(s => s.Id == id).FirstOrDefault<Cart>();
            if (query != null)
                query.Count++;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int id)
        {
            var query = _context.Cart.Where(s => s.Id == id).FirstOrDefault<Cart>();
            if (query != null)
            {
                if (query.Count != 1)
                    query.Count--;
                else
                {
                    var cart = await _context.Cart.FindAsync(id);
                    _context.Cart.Remove(cart);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Carts
        
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (userEmail == null)
            {
                return RedirectToAction("LoginBeforeShopping", "Account");
            }
            ViewData["Message"] = GetSumPayment();
            return View(await _context.Cart.Include(p => p.Clothings).Where(x => x.account.Email ==userEmail).ToListAsync());
          
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Count")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Count")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
