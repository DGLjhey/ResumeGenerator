using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TUPApp.Models;

namespace TUPApp.Controllers
{
    public class EducationBgsController : Controller
    {
        public class Dropdown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private readonly TupContext _context;

        public EducationBgsController(TupContext context)
        {
            _context = context;
        }

        // GET: EducationBgs
        public async Task<IActionResult> Index()
        {
            var tupContext = _context.EducationBgs.Include(e => e.Student);
            return View(await tupContext.ToListAsync());
        }

        // GET: EducationBgs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EducationBgs == null)
            {
                return NotFound();
            }

            var educationBg = await _context.EducationBgs
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationBg == null)
            {
                return NotFound();
            }

            return View(educationBg);
        }

        // GET: EducationBgs/Create
        public IActionResult Create()
        {
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            var dropdown = new List<Dropdown>();
            var students = _context.Students.ToList();

            foreach (var item in students)
            {
                dropdown.Add(new Dropdown
                {
                    Id = item.Id,
                    Name = item.Firstname + " " + item.Lastname

                });
            }
            ViewData["StudentId"] = new SelectList(dropdown, "Id", "Name");
            return View();
        }

        // POST: EducationBgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,School,Year,StudentId,Course")] EducationBg educationBg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationBg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", educationBg.StudentId);
            return View(educationBg);
        }

        // GET: EducationBgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EducationBgs == null)
            {
                return NotFound();
            }

            var EducationBgs = await _context.EducationBgs.FindAsync(id);
            if (EducationBgs == null)
            {
                return NotFound();
            }
            var dropdown = new List<Dropdown>();
            var students = _context.Students.Where(x => x.Id == id).ToList();

            foreach (var item in students)
            {
                dropdown.Add(new Dropdown
                {
                    Id = item.Id,
                    Name = item.Firstname + " " + item.Lastname

                });
            }
            ViewData["StudentId"] = new SelectList(dropdown, "Id", "Name");
            return View();
        }

        // POST: EducationBgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,School,Year,StudentId,Course")] EducationBg educationBg)
        {
            if (id != educationBg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationBg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationBgExists(educationBg.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", educationBg.StudentId);
            return View(educationBg);
        }

        // GET: EducationBgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EducationBgs == null)
            {
                return NotFound();
            }

            var educationBg = await _context.EducationBgs
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationBg == null)
            {
                return NotFound();
            }

            return View(educationBg);
        }

        // POST: EducationBgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EducationBgs == null)
            {
                return Problem("Entity set 'TupContext.EducationBgs'  is null.");
            }
            var educationBg = await _context.EducationBgs.FindAsync(id);
            if (educationBg != null)
            {
                _context.EducationBgs.Remove(educationBg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationBgExists(int id)
        {
          return (_context.EducationBgs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
