using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_3.Data;
using Garage_3.Models;
using Garage_3.Models.ViewModel;

namespace Garage_3
{
    public class ColorsController : Controller
    {
        private readonly Garage_3Context _context;

        public ColorsController(Garage_3Context context)
        {
            _context = context;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
            var list = await _context.Colors
                .OrderBy(c => c.ColorName)
                .ToListAsync();
            var model = list.Select(c => new ColorViewModel
            {
                ColorName = c.ColorName,
                Id = c.Id,
                IsUsed = _context.Vehicle.Any(v => v.ColorId == c.Id)
            });


            return View(model);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColorName")] Color color)
        {

            color.ColorName = color.ColorName.ToUpper();
            if (ModelState.IsValid)
            {
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ColorName")] Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            color.ColorName = color.ColorName.ToUpper();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Id))
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
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool ColorExists(int id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
