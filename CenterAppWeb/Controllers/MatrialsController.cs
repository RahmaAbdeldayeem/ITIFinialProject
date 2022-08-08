using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenterApp.Core.Models;
using CenterApp.Infrasturcture.Data;

namespace CenterAppWeb.Controllers
{
    public class MatrialsController : Controller
    {
        private readonly CenterDBContext _context;

        public MatrialsController(CenterDBContext context)
        {
            _context = context;
        }

        // GET: Matrials
        public async Task<IActionResult> Index()
        {
            return _context.Matrials != null ?
                        View(await _context.Matrials.ToListAsync()) :
                        Problem("Entity set 'CenterDBContext.Matrials'  is null.");
        }

        // GET: Matrials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matrials == null)
            {
                return NotFound();
            }

            var matrial = await _context.Matrials
                .FirstOrDefaultAsync(m => m.Matrial_Id == id);
            if (matrial == null)
            {
                return NotFound();
            }

            return View(matrial);
        }

        // GET: Matrials/Create
        public IActionResult Create(int id)
        {
            ViewBag.Level_Id = id;
            return View();
        }

        // POST: Matrials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Matrial matrial, int id)
        {
            if (ModelState.IsValid)
            {
                var martialExist =
                    _context.Matrials.Where(x => x.Matrial_Name.ToLower() == matrial.Matrial_Name.ToLower());
                if (martialExist.Count()<=0)
                {
                    _context.Matrials.Add(matrial);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewBag.error = "This Matrial Is Exists ";
                    return View(matrial);
                }

                var lstLevelMatrials =
                    _context.LevelMatrial.Where(x => x.Level_Id == id && x.Matrial_Id == matrial.Matrial_Id);
                if (lstLevelMatrials.Count()<=0)
                    _context.LevelMatrial.Add(new LevelMatrial() { Level_Id = id, Matrial_Id = matrial.Matrial_Id });
                else
                {
                    ViewBag.error = "This Matrial Is Exists ";
                    return View(matrial);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("AddingToLevel", "Levels", new { id = id });

            }
            return View(matrial);
        }

        // GET: Matrials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matrials == null)
            {
                return NotFound();
            }

            var matrial = await _context.Matrials.FindAsync(id);
            if (matrial == null)
            {
                return NotFound();
            }
            return View(matrial);
        }

        // POST: Matrials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matrial_Id,Matrial_Name")] Matrial matrial)
        {
            if (id != matrial.Matrial_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matrial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatrialExists(matrial.Matrial_Id))
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
            return View(matrial);
        }

        // GET: Matrials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matrials == null)
            {
                return NotFound();
            }

            var matrial = await _context.Matrials
                .FirstOrDefaultAsync(m => m.Matrial_Id == id);
            if (matrial == null)
            {
                return NotFound();
            }

            return View(matrial);
        }

        // POST: Matrials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matrials == null)
            {
                return Problem("Entity set 'CenterDBContext.Matrials'  is null.");
            }
            var matrial = await _context.Matrials.FindAsync(id);
            if (matrial != null)
            {
                _context.Matrials.Remove(matrial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatrialExists(int id)
        {
            return (_context.Matrials?.Any(e => e.Matrial_Id == id)).GetValueOrDefault();
        }
    }
}
