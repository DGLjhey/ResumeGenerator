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
    public class SkillsController : Controller
    {
        public class Dropdown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private readonly TupContext _context;

        public SkillsController(TupContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            var tupContext = _context.Skills.Include(s => s.Student);
            return View(await tupContext.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Skills == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // GET: Skills/Create
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

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Skills,StudentId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", skill.StudentId);
            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Skills == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", skill.StudentId);
            //return View(skill);

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

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Skills,StudentId")] Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", skill.StudentId);
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Skills == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Skills == null)
            {
                return Problem("Entity set 'TupContext.Skills'  is null.");
            }
            var skill = await _context.Skills.FindAsync(id);
            if (skill != null)
            {
                _context.Skills.Remove(skill);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
          return (_context.Skills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
