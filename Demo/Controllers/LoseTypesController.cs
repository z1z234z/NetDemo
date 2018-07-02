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
    public class LoseTypesController : Controller
    {
        private readonly DBContext _context;

        public LoseTypesController(DBContext context)
        {
            _context = context;
        }

        // GET: LoseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoseTypes.ToListAsync());
        }

        // GET: LoseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loseType = await _context.LoseTypes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (loseType == null)
            {
                return NotFound();
            }

            return View(loseType);
        }

        // GET: LoseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoseTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] LoseType loseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loseType);
        }

        // GET: LoseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loseType = await _context.LoseTypes.SingleOrDefaultAsync(m => m.ID == id);
            if (loseType == null)
            {
                return NotFound();
            }
            return View(loseType);
        }

        // POST: LoseTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] LoseType loseType)
        {
            if (id != loseType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoseTypeExists(loseType.ID))
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
            return View(loseType);
        }

        // GET: LoseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loseType = await _context.LoseTypes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (loseType == null)
            {
                return NotFound();
            }

            return View(loseType);
        }

        // POST: LoseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loseType = await _context.LoseTypes.SingleOrDefaultAsync(m => m.ID == id);
            _context.LoseTypes.Remove(loseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoseTypeExists(int id)
        {
            return _context.LoseTypes.Any(e => e.ID == id);
        }
    }
}
