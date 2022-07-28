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
    public class StagesController : Controller
    {
        private readonly CenterDBContext _context;

        public StagesController(CenterDBContext context)
        {
            _context = context;
        }

        // GET: Stages
        public async Task<IActionResult> Index(int level_id)
        {
            var centerDBContext = _context.Stages.Where(x => x.Level_Id == level_id);
            return View(await centerDBContext.ToListAsync());
        }

        // GET: Stages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stages == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .Include(s => s.Level)
                .FirstOrDefaultAsync(m => m.Stage_Id == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // GET: Stages/Create
        public IActionResult Create(int id)
        {
            ViewBag.LevelID = id;
            // ViewData["Level_Id"] = new SelectList(_context.Levels, "Level_Id", "Level_Name");
            return View();
        }

        // POST: Stages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Stage stage)
        {
            if (ModelState.IsValid)
            {
                _context.Stages.Add(stage);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddingToLevel", "Levels",new {id= stage.Level_Id});
            }
            ViewData["Level_Id"] = new SelectList(_context.Levels, "Level_Id", "Level_Name", stage.Level_Id);
            return View(stage);
        }

        // GET: Stages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stages == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }
            ViewData["Level_Id"] = new SelectList(_context.Levels, "Level_Id", "Level_Name", stage.Level_Id);
            return View(stage);
        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Stage_Id,Stage_Name,Level_Id")] Stage stage)
        {
            if (id != stage.Stage_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StageExists(stage.Stage_Id))
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
            ViewData["Level_Id"] = new SelectList(_context.Levels, "Level_Id", "Level_Name", stage.Level_Id);
            return View(stage);
        }

        // GET: Stages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stages == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .Include(s => s.Level)
                .FirstOrDefaultAsync(m => m.Stage_Id == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stages == null)
            {
                return Problem("Entity set 'CenterDBContext.Stages'  is null.");
            }
            var stage = await _context.Stages.FindAsync(id);
            if (stage != null)
            {
                _context.Stages.Remove(stage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StageExists(int id)
        {
            return (_context.Stages?.Any(e => e.Stage_Id == id)).GetValueOrDefault();
        }
    }
}
