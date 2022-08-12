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

namespace CenterAppWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly CenterDBContext _context;

        public StudentsController(CenterDBContext context)
        {
            _context = context;
        }

        //new
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Students = await _context.Students.ToListAsync();
            ViewBag.Students = Students;
            StudentsSearchIndexVM studentsSearch = new StudentsSearchIndexVM();
            studentsSearch.Students = Students;
            return View(studentsSearch);
        }

        [HttpPost]

        public async Task<IActionResult> Index(StudentsSearchIndexVM studentsSearch)
        {
            studentsSearch.Students = await _context.Students.ToListAsync();

            if (!String.IsNullOrEmpty(studentsSearch.SearchByName))
                studentsSearch.Students = _context.Students.Where(x => x.Student_Name.ToLower()
                .Contains(studentsSearch.SearchByName.ToLower())).ToList();


            //if (!String.IsNullOrEmpty(studentsSearch.SearchById))
            //    studentsSearch.Students = _context.Students.Where(x => x.Student_Id.ToLower()
            //      .Contains(studentsSearch.SearchByName.ToLower())).ToList();

            return View(studentsSearch);
        }

      
        public IActionResult Details()
        {
           // ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            //ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Stage)
                .FirstOrDefaultAsync(m => m.Student_Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            ViewData["Material_id"] = new SelectList(_context.Stages, "Material_Id", "Material_Name");
            return View(new StudentStageMaterialVM());
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( StudentStageMaterialVM studentstagematerialvm)
        {

            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name");
            ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            ViewData["Material_id"] = new SelectList(_context.Stages, "Material_Id", "Material_Name");
            try
            {


                if (ModelState.IsValid)
                {
                    IFormFile file = (Request.Form.Files["imageFile"]);

                    string folderPath = AppDomain.CurrentDomain.BaseDirectory+"Images";
                    if (!Directory.Exists(folderPath)) { 
                    
                    Directory.CreateDirectory(folderPath);
                    }
                    Student student = studentstagematerialvm.Student;
                    student.Student_Image =Path.Combine(folderPath,file.FileName);
                    using (Stream fileStream = new FileStream(student.Student_Image, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    _context.Students.Add(studentstagematerialvm.Student);
                  
                    await _context.SaveChangesAsync();
                    return View(studentstagematerialvm);
                }

                foreach (var item in ModelState.Values)
                {
                    foreach (var item2 in item.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item2.ErrorMessage);
                    }
                }
                // ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", student.Stage_id);
                //ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
                return View(studentstagematerialvm);
            }
                catch(Exception ex)
            {
                ex = ex;
                return View(studentstagematerialvm);
            }


        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", student.Stage_id);
          //  ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Student_Id,Student_Name,Student_Address,Student_BirthOfDate,Gender,Student_RegisterDate,Student_Email,Student_Image,Student_StdPhone,Student_ParentPhone,Stage_id")] Student student)
        {
            if (id != student.Student_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Student_Id))
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
            ViewData["Stage_id"] = new SelectList(_context.Stages, "Stage_Id", "Stage_Name", student.Stage_id);
           // ViewData["Level_id"] = new SelectList(_context.Stages, "Level_Id", "Level_Name");
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Stage)
                .FirstOrDefaultAsync(m => m.Student_Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'CenterDBContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Student_Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult payForSupject(int id)
        {
            var materials = _context.Matrials.ToList();
            var students = _context.Students.ToList();
            ViewBag.Materials = materials;
            ViewBag.Students = students;
            return View();
        }
        [HttpPost]
        public IActionResult payForSupject(PaymentVM model)
        {
            var materials = _context.Matrials.ToList();
            var students = _context.Students.ToList();
            ViewBag.Materials = materials;
            ViewBag.Students = students;
            _context.StudentPayments.Add(new StudentPayments
            {
                Matrial_Id = model.MateriaId,
                Price = model.Price,
                Student_Id = model.StudentId,
                IsPaid = model.IsPaid,
                Date = model.Date
            });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetPaymentDetails(int studentId)
        {
            ViewBag.Materials = _context.Matrials.ToList();
            List<PaymentStatus> statusModels = _context.StudentPayments
                .Where(s => s.Student_Id == studentId).Include(d => d.Matrial).Select(c => new PaymentStatus
                {
                    Id = c.Id,
                    IsPaid = c.IsPaid,
                    MaterialName = c.Matrial.Matrial_Name,
                    Price = c.Price,
                    Date = c.Date.ToString("MM")
                }).ToList();

            return View(statusModels);
        }


        public IActionResult DeletePayment(int id)
        {
            int stdId = 0;
            var payment = _context.StudentPayments.FirstOrDefault(d => d.Id == id);
            stdId = payment.Student_Id;
            _context.StudentPayments.Remove(payment);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetPaymentDetails), new { studentId = stdId });
        }

        [HttpGet]
        public IActionResult EditPayment(int id)
        {
            var materials = _context.Matrials.ToList();
            var students = _context.Students.ToList();
            ViewBag.Materials = materials;
            ViewBag.Students = students;
            StudentPayments studentP = _context.StudentPayments.FirstOrDefault(d => d.Id == id);
            return View(studentP);

        }
        [HttpPost]
        public IActionResult EditPayment(StudentPayments payment)
        {
            var materials = _context.Matrials.ToList();
            var students = _context.Students.ToList();
            ViewBag.Materials = materials;
            ViewBag.Students = students;
            StudentPayments studentP = _context.StudentPayments.FirstOrDefault(d => d.Id == payment.Id);
            studentP.Matrial_Id = payment.Matrial_Id;
            studentP.Price = payment.Price;
            studentP.IsPaid = payment.IsPaid;
            studentP.Date = payment.Date;

            _context.SaveChanges();
            return RedirectToAction(nameof(GetPaymentDetails), new { studentId = studentP.Student_Id });

        }




    }
}
