using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACarSample.Entities;

namespace RentACarSample.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubBrandController : Controller
    {
        private readonly DatabaseContext _context;

        public SubBrandController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/SubBrand
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.SubBrands.Include(s => s.Brand);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/SubBrand/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubBrands == null)
            {
                return NotFound();
            }

            var subBrand = await _context.SubBrands
                .Include(s => s.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subBrand == null)
            {
                return NotFound();
            }

            return View(subBrand);
        }

        // GET: Admin/SubBrand/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            return View();
        }

        // POST: Admin/SubBrand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BrandId")] SubBrand subBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", subBrand.BrandId);
            return View(subBrand);
        }

        // GET: Admin/SubBrand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubBrands == null)
            {
                return NotFound();
            }

            var subBrand = await _context.SubBrands.FindAsync(id);
            if (subBrand == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", subBrand.BrandId);
            return View(subBrand);
        }

        // POST: Admin/SubBrand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BrandId")] SubBrand subBrand)
        {
            if (id != subBrand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubBrandExists(subBrand.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", subBrand.BrandId);
            return View(subBrand);
        }

        // GET: Admin/SubBrand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubBrands == null)
            {
                return NotFound();
            }

            var subBrand = await _context.SubBrands
                .Include(s => s.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subBrand == null)
            {
                return NotFound();
            }

            return View(subBrand);
        }

        // POST: Admin/SubBrand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubBrands == null)
            {
                return Problem("Entity set 'DatabaseContext.SubBrands'  is null.");
            }
            var subBrand = await _context.SubBrands.FindAsync(id);
            if (subBrand != null)
            {
                _context.SubBrands.Remove(subBrand);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubBrandExists(int id)
        {
          return _context.SubBrands.Any(e => e.Id == id);
        }
    }
}
