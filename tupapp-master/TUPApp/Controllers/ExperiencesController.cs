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
    public class ExperiencesController : Controller
    {
        public class Dropdown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private readonly TupContext _context;

        public ExperiencesController(TupContext context)
        {
            _context = context;
        }

        // GET: Experiences
        public async Task<IActionResult> Index()
        {
            var tupContext = _context.Experiences.Include(e => e.Student);
            return View(await tupContext.ToListAsync());
        }

        // GET: Experiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experiences/Create
        public IActionResult Create()
        {
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            //return View();

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

        // POST: Experiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Companyname,Role,Year,StudentId")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", experience.StudentId);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
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


            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", experience.StudentId);
            //return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Companyname,Role,Year,StudentId")] Experience experience)
        {
            if (id != experience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", experience.StudentId);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Experiences == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                 .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Experiences == null)
            {
                return Problem("Entity set 'TupContext.Experiences'  is null.");
            }
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
          return (_context.Experiences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
