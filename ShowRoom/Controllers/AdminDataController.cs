using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowRoom.Models;
using ShowRoom.Controllers;
using ShowRoom.Data;
using Microsoft.AspNetCore.Authorization;

namespace ShowRoom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDataController : Controller
    {
        private readonly ShowRoomContext _context;

        public AdminDataController(ShowRoomContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var myadmin = new AdminPanel();
            myadmin.Accounts = _context.Account.ToList();
            myadmin.Orders = _context.Order.ToList();
            myadmin.Clothings = _context.Clothings.ToList();
            return View(myadmin);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> OrderDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _context.Order.FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> OrderEdit(int? id)
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
        public async Task<IActionResult> OrderEdit(int id, [Bind("Id,Address,OrderTime,Phone,SumToPay")] Order order)
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
                catch (Exception)
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
        public async Task<IActionResult> OrderDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =  _context.Order.FirstOrDefault(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("OrderDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDeleteConfirmed(int id)
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

        ///// GET: Accounts
        // GET: Accounts/Details/5
        public async Task<IActionResult> AccountDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account =  _context.Account
                .FirstOrDefault(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult AccountCreate()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountCreate([Bind("Id,Email,Password,ConfirmPassword,FullName,Gender,Type")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> AccountEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountEdit(int id, [Bind("Id,Email,Username,Password,ConfirmPassword,FullName,Gender,Type")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> AccountDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account =  _context.Account
                .FirstOrDefault(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("AccountDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountDeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ClothingsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Clothings
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult ClothingsCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClothingsCreate([Bind("Id,Name,Price,Brand,Description,Category,ImageUrl")] Clothings product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> ClothingsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Clothings.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClothingsEdit(int id, [Bind("Id,Name,Price,Brand,Description,Category,ImageUrl")] Clothings product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!ClothingsExists(product.Id))
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
            return View(product);
        }

        public async Task<IActionResult> ClothingsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product =  _context.Clothings
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost, ActionName("ClothingsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClothingsDeleteConfirmed(int id)
        {
            var product = await _context.Clothings.FindAsync(id);
            _context.Clothings.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothingsExists(int id)
        {
            return _context.Clothings.Any(e => e.Id == id);
        }

        //GET:

        // Search-By Name
        public async Task<IActionResult> SearchByName(string name)
        {

            var product = _context.Clothings.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
            if (product == null)
            {
                return NotFound();
            }

            return View("ClothingsDetails", product);
        }

        // Search by product ID
        public async Task<IActionResult> SearchByClothingsID(string product_id)
        {
            int? id = Int32.Parse(product_id);
            
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Clothings.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View("ClothingsDetails", product);

        }

        // Search by order ID
        public async Task<IActionResult> SearchByOrderID(string order_id)
        {
            int? id = Int32.Parse(order_id);

            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View("OrderDetails", order);

        }

        // Search by account ID
        public async Task<IActionResult> SearchByAccountEmail(string email)
        {
            var account = _context.Account.FirstOrDefault(a => a.Email.ToLower().Contains(email.ToLower()));
            if (account == null)
            {
                return NotFound();
            }

            return View("AccountDetails", account);
        }
    }
}