using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CenterApp.Core.Models;
using CenterApp.Infrasturcture.Data;
using CenterAppWeb.ViewModel;
using System.IO;

namespace CenterAppWeb.Controllers
{
    public class TeachersController : Controller
    {
        private readonly CenterDBContext _context;
        private readonly IWebHostEnvironment _hosting;


        public TeachersController(CenterDBContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        // GET: Teachers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            TeacherSearchIndexVM teacherSearchIndexVM = new TeacherSearchIndexVM();
            teacherSearchIndexVM.Teachers = await _context.Teacher.ToListAsync();
            return View(teacherSearchIndexVM);
        }
        [HttpPost]
        public async Task<IActionResult> Index(TeacherSearchIndexVM teacherSearchIndexVM)
        {
            teacherSearchIndexVM.Teachers = await _context.Teacher.ToListAsync();
            if (teacherSearchIndexVM.SearchByID is not null)
            {
                teacherSearchIndexVM.Teachers = teacherSearchIndexVM.Teachers.Where(x => x.Teacher_Id == teacherSearchIndexVM.SearchByID).ToList();
            }
            if (!String.IsNullOrEmpty(teacherSearchIndexVM.SearchByName))
                teacherSearchIndexVM.Teachers = teacherSearchIndexVM.Teachers.Where(x => x.Teacher_Name.ToLower()
                .Contains(teacherSearchIndexVM.SearchByName.ToLower())).ToList();
            if (!String.IsNullOrEmpty(teacherSearchIndexVM.SearchByPhone))
                teacherSearchIndexVM.Teachers = teacherSearchIndexVM.Teachers.Where(x => x.Teacher_Phone.ToLower()
               .Contains(teacherSearchIndexVM.SearchByPhone.ToLower())).ToList();
            if (teacherSearchIndexVM.SearchBySubject is not null)
            {
                var matrialTeachers = _context.TeacherMatrial.Where(x => x.Matrial_Id == teacherSearchIndexVM.SearchBySubject).ToList();
                foreach (var item in matrialTeachers)
                    teacherSearchIndexVM.Teachers = teacherSearchIndexVM.Teachers.Where(x => x.Teacher_Id == item.Teacher_Id).ToList();
            }

            return View(teacherSearchIndexVM);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Teacher_Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            ViewBag.Matrial_Id = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name");
            ViewBag.Stage_Id = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewBag.Level_Id = new SelectList(_context.Levels, "Level_Id", "Level_Name");
            return View(new TeacherStageMatrialVM());
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherStageMatrialVM teacherStageMatrial)
        {
            ViewData["Matrial_Id"] = new SelectList(_context.Matrials, "Matrial_Id", "Matrial_Name");
            ViewData["Stage_Id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Level_Id"] = new SelectList(_context.Levels, "Level_Id", "Level_Name");

            if (ModelState.IsValid)
            {

                string fileimage = String.Empty;
                if (teacherStageMatrial.file != null)
                {
                    string images = Path.Combine(_hosting.WebRootPath, "images");
                    fileimage = teacherStageMatrial.file.FileName;
                    string fullpathimage = Path.Combine(images, fileimage);
                    using (var stream = new FileStream(fullpathimage, FileMode.Create))
                    {
                        teacherStageMatrial.file.CopyTo(stream);
                    }

                    teacherStageMatrial.Teacher.Teacher_Image = fileimage;
                }

                _context.Teacher.Add(teacherStageMatrial.Teacher);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Teacher Is Added SuccessFull .... Please Add him to Subject ";
                return View(teacherStageMatrial);
            }
            foreach (var item in ModelState.Values)
            {
                foreach (var item2 in item.Errors)
                {
                    ModelState.AddModelError(string.Empty, item2.ErrorMessage);

                }

            }

            ViewBag.Message = "Error Please Add Teacher Again";
            return View(teacherStageMatrial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMatrial(TeacherStageMatrialVM teacherStageMatrial)
        {

            var teacherMatrial = new TeacherMatrial() { Matrial_Id = teacherStageMatrial.Matrial_Id ?? 0, Stage_Id = teacherStageMatrial.Stage_Id ?? 0, Teacher_Id = teacherStageMatrial.Teacher.Teacher_Id };
            _context.TeacherMatrial.Add(teacherMatrial);
            await _context.SaveChangesAsync();


            return Json(teacherMatrial);
            //return RedirectToAction(nameof(Index));
        }
        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Teacher teacher)
        {
            if (id != teacher.Teacher_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Teacher_Id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Teacher == null)
        //    {
        //        return NotFound();
        //    }

        //    var teacher = await _context.Teacher
        //        .FirstOrDefaultAsync(m => m.Teacher_Id == id);
        //    if (teacher == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(teacher);
        //}

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teacher == null)
            {
                return Problem("Entity set 'CenterDBContext.Teacher'  is null.");
            }
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher != null)
            {
                _context.Teacher.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TeacherExists(int id)
        {
            return (_context.Teacher?.Any(e => e.Teacher_Id == id)).GetValueOrDefault();
        }
    }
}
