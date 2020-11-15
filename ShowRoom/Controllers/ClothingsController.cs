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
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;

namespace ShowRoom.Controllers
{
    
    public class ClothingsController : Controller
    {
        private readonly ShowRoomContext _context;

        public ClothingsController(ShowRoomContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (userEmail == null)
            {
                return RedirectToAction("LoginBeforeShopping", "Account");
            }
            var query = _context.Cart.Where(s => s.Clothings.Id == id).Where(s => s.account.Email == userEmail).FirstOrDefault<Cart>();
            if (query == null)
            {
                Account account = _context.Account.First(s => s.Email == userEmail);
                Clothings product = _context.Clothings.First(s => s.Id == id);
                Cart c = new Cart { Clothings = product, Count = 1, account = account };
                _context.Cart.Add(c);
            }
            else
                query.Count++;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public class MyObject
        {
            public int Id { get; set; }
            public int count { get; set; }

        }

        // GET: Clothings
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (userEmail != null)
            {
                var myList = new List<MyObject>();
                var temp = _context.Click_Details.Where(x => x.Account.Email == userEmail).Include(x=>x.Clothings).GroupBy(x => x.Clothings.Category).ToList();
                if(temp.Count()==0)
                    return View(await _context.Clothings.ToListAsync());
                foreach (Microsoft.EntityFrameworkCore.Query.Internal.Grouping<string, Click_Details> item in temp)
                {
                    List<ShowRoom.Models.Click_Details> lis = item.ToList();
                    int category = Int32.Parse(lis[0].Clothings.Category);
                    int count = lis.Count();
                    myList.Add(new MyObject { Id = category, count = count });
                }
                myList.Sort((x, y) => y.count-x.count);
                ViewData["BestCategory"] = myList[0].Id.ToString();
            }
            return View(await _context.Clothings.ToListAsync());
        }

        // Search-By name
        public async Task<IActionResult> SearchByName(string name)
        {
            var q = _context.Clothings.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();

            return View("Index", await q); //Return Clothings list from context
        }


        // Search-Auto Complete
        public async Task<IActionResult> SearchAuto(string term)
        {
            var query = from p in _context.Clothings
                        where p.Name.ToLower().Contains(term.ToLower())
                        select new { id = p.Id, label = p.Name, value = p.Id };

            return Json(await query.ToListAsync());
        }

        // Filter By Category
        public async Task<IActionResult> FilterByCategoryAndPrice(String id, String min, String max)
        {
            int minim = Int32.Parse(min);
            int maxim = Int32.Parse(max);
            int category_id = Int32.Parse(id);
            var query = from p in _context.Clothings
                        where p.Category.Equals(category_id) && p.Price >= minim && p.Price <= maxim
                        select p;

            return Json(await query.ToListAsync());

        }

        // Search By Category ID
        public async Task<IActionResult> FilterByCategory(List<string> category)
        {
            List<Clothings> results;

            if (category != null && category.Count > 0)
            {
                //filterbycategory
                var q = _context.Clothings.Where(p => category.Exists(filteredCategory => filteredCategory.Equals(p.Category.ToString()))).ToListAsync();
                results = await q;
            }
            else
            {
                results = await _context.Clothings.ToListAsync();
            }

            return Json(results); //Return Clothings list from context
        }

        // GET: Clothings/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Clothings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (userEmail != null)
            {
                Account account = _context.Account.First(s => s.Email == userEmail);

                Click_Details c = new Click_Details { Clothings = product, Account = account, ClickTime = DateTime.Now };
                _context.Click_Details.Add(c);
                await _context.SaveChangesAsync();

            }
            return View(product);
        }

        // GET: Clothings/Create
        [Authorize (Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clothings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Brand,Description,Category,ImageUrl")] Clothings product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Clothings/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Clothings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Brand,Description,Category,ImageUrl")] Clothings product)
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
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

        // GET: Clothings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Clothings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Clothings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Clothings.FindAsync(id);
            _context.Clothings.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Clothings.Any(e => e.Id == id);
        }
    }
}
