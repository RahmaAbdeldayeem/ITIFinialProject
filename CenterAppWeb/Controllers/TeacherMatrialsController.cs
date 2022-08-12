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
    public class TeacherMatrialsController : Controller
    {
        private readonly CenterDBContext _context;

        public TeacherMatrialsController(CenterDBContext context)
        {
            _context = context;
        }

        // GET: TeacherMatrials
        public async Task<IActionResult> Index()
        {
            var centerDBContext = _context.TeacherMatrial.Include(t => t.Matrial).Include(t => t.Stage).Include(t => t.Teacher);
            return View(await centerDBContext.ToListAsync());
        }

        // GET: TeacherMatrials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherMatrial == null)
            {
                return NotFound();
            }

            var teacherMatrial = await _context.TeacherMatrial
                .Include(t => t.Matrial)
                .Include(t => t.Stage)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Matrial_Id == id);
            if (teacherMatrial == null)
            {
                return NotFound();
            }

            return View(teacherMatrial);
        }

        // GET: TeacherMatrials/Create
        public IActionResult Create()
        {
            ViewData["Matrial_Id"] = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name");
            ViewData["Stage_Id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Email");
            return View();
        }

        // POST: TeacherMatrials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matrial_Id,Teacher_Id,Stage_Id")] TeacherMatrial teacherMatrial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherMatrial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Matrial_Id"] = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name", teacherMatrial.Matrial_Id);
            ViewData["Stage_Id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", teacherMatrial.Stage_Id);
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Email", teacherMatrial.Teacher_Id);
            return View(teacherMatrial);
        }

        // GET: TeacherMatrials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherMatrial == null)
            {
                return NotFound();
            }

            var teacherMatrial = await _context.TeacherMatrial.FindAsync(id);
            if (teacherMatrial == null)
            {
                return NotFound();
            }
            ViewData["Matrial_Id"] = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name", teacherMatrial.Matrial_Id);
            ViewData["Stage_Id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", teacherMatrial.Stage_Id);
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Email", teacherMatrial.Teacher_Id);
            return View(teacherMatrial);
        }

        // POST: TeacherMatrials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Matrial_Id,Teacher_Id,Stage_Id")] TeacherMatrial teacherMatrial)
        {
            if (id != teacherMatrial.Matrial_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherMatrial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherMatrialExists(teacherMatrial.Matrial_Id))
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
            ViewData["Matrial_Id"] = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name", teacherMatrial.Matrial_Id);
            ViewData["Stage_Id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", teacherMatrial.Stage_Id);
            ViewData["Teacher_Id"] = new SelectList(_context.Teacher, "Teacher_Id", "Teacher_Email", teacherMatrial.Teacher_Id);
            return View(teacherMatrial);
        }

        // GET: TeacherMatrials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherMatrial == null)
            {
                return NotFound();
            }

            var teacherMatrial = await _context.TeacherMatrial
                .Include(t => t.Matrial)
                .Include(t => t.Stage)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Matrial_Id == id);
            if (teacherMatrial == null)
            {
                return NotFound();
            }

            return View(teacherMatrial);
        }

        // POST: TeacherMatrials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherMatrial == null)
            {
                return Problem("Entity set 'CenterDBContext.TeacherMatrial'  is null.");
            }
            var teacherMatrial = await _context.TeacherMatrial.FindAsync(id);
            if (teacherMatrial != null)
            {
                _context.TeacherMatrial.Remove(teacherMatrial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherMatrialExists(int id)
        {
          return (_context.TeacherMatrial?.Any(e => e.Matrial_Id == id)).GetValueOrDefault();
        }
    }
}
