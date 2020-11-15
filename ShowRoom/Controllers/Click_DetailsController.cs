using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShowRoom.Data;
using ShowRoom.Models;

namespace ShowRoom.Controllers
{
    public class Click_DetailsController : Controller
    {
        private readonly ShowRoomContext _context;

        public Click_DetailsController(ShowRoomContext context)
        {
            _context = context;
        }

        // GET: Click_Details
        public async Task<IActionResult> Index()
        {
            return View(await _context.Click_Details.ToListAsync());
        }

        // GET: Click_Details/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var click_Details = await _context.Click_Details
                .FirstOrDefaultAsync(m => m.Id == id);
            if (click_Details == null)
            {
                return NotFound();
            }

            return View(click_Details);
        }

        // GET: Click_Details/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Click_Details/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderTime")] Click_Details click_Details)
        {
            if (ModelState.IsValid)
            {
                _context.Add(click_Details);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(click_Details);
        }

        // GET: Click_Details/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var click_Details = await _context.Click_Details.FindAsync(id);
            if (click_Details == null)
            {
                return NotFound();
            }
            return View(click_Details);
        }

        // POST: Click_Details/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderTime")] Click_Details click_Details)
        {
            if (id != click_Details.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(click_Details);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Click_DetailsExists(click_Details.Id))
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
            return View(click_Details);
        }

        // GET: Click_Details/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var click_Details = await _context.Click_Details
                .FirstOrDefaultAsync(m => m.Id == id);
            if (click_Details == null)
            {
                return NotFound();
            }

            return View(click_Details);
        }

        // POST: Click_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var click_Details = await _context.Click_Details.FindAsync(id);
            _context.Click_Details.Remove(click_Details);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Click_DetailsExists(int id)
        {
            return _context.Click_Details.Any(e => e.Id == id);
        }
    }
}
