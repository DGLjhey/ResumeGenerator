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
    public class EmergenciesController : Controller
    {
        private readonly TupContext _context;

        public EmergenciesController(TupContext context)
        {
            _context = context;
        }

        // GET: Emergencies
        public async Task<IActionResult> Index()
        {
            var tupContext = _context.Emergencies.Include(e => e.Student);
            return View(await tupContext.ToListAsync());
        }

        // GET: Emergencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency = await _context.Emergencies
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emergency == null)
            {
                return NotFound();
            }

            return View(emergency);
        }

        // GET: Emergencies/Create
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

        // POST: Emergencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Address,ContactNumber,StudentId")] Emergency emergency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emergency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", emergency.StudentId);
            return View(emergency);
        }

        // GET: Emergencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency = await _context.Emergencies.FindAsync(id);
            if (emergency == null)
            {
                return NotFound();
            }
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", emergency.StudentId);
            //return View(emergency);

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

        // POST: Emergencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Address,ContactNumber,StudentId")] Emergency emergency)
        {
            if (id != emergency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emergency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergencyExists(emergency.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", emergency.StudentId);
            return View(emergency);
        }

        // GET: Emergencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency = await _context.Emergencies
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emergency == null)
            {
                return NotFound();
            }

            return View(emergency);
        }

        // POST: Emergencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emergencies == null)
            {
                return Problem("Entity set 'TupContext.Emergencies'  is null.");
            }
            var emergency = await _context.Emergencies.FindAsync(id);
            if (emergency != null)
            {
                _context.Emergencies.Remove(emergency);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmergencyExists(int id)
        {
          return (_context.Emergencies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
