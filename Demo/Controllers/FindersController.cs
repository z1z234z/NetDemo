using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Models;

namespace Demo.Controllers
{
    public class FindersController : Controller
    {
        private readonly DBContext _context;

        public FindersController(DBContext context)
        {
            _context = context;
        }

        // GET: Finders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Finders.ToListAsync());
        }

        // GET: Finders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finder = await _context.Finders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (finder == null)
            {
                return NotFound();
            }

            return View(finder);
        }

        // GET: Finders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Finders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Content,Complete,Question,Answer,Time")] Finder finder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(finder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(finder);
        }

        // GET: Finders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finder = await _context.Finders.SingleOrDefaultAsync(m => m.ID == id);
            if (finder == null)
            {
                return NotFound();
            }
            return View(finder);
        }

        // POST: Finders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Content,Complete,Question,Answer,Time")] Finder finder)
        {
            if (id != finder.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(finder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinderExists(finder.ID))
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
            return View(finder);
        }

        // GET: Finders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var finder = await _context.Finders
                .SingleOrDefaultAsync(m => m.ID == id);
            if (finder == null)
            {
                return NotFound();
            }

            return View(finder);
        }

        // POST: Finders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var finder = await _context.Finders.SingleOrDefaultAsync(m => m.ID == id);
            _context.Finders.Remove(finder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinderExists(int id)
        {
            return _context.Finders.Any(e => e.ID == id);
        }
    }
}
