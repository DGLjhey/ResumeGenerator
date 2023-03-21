using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TUPApp.Models;

namespace TUPApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TupContext _context;
        private readonly ILogger<HomeController> _logger;

        public static string Title = "";
        public static string Description = "";
        public static string Gender = "";

        public HomeController(ILogger<HomeController> logger, TupContext context)
        {
            _logger = logger;
            _context = context;
        }

        //WelcomePage
        public IActionResult Index()
        {
            return View();
        }

        //MenuPage
        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult Resume()
        {
            var student = _context.Students
                            //.Where(x => x.Id == 2)
                            .FirstOrDefault();

            var vm = new HomeViewModel()
            {
                Firstname = student.Firstname,
                Middlename = student.Middlename,
                Lastname = student.Lastname,
                Address = student.Address,
                Email = student.Email,
                Summary = student.Summary,
                Contact = student.Contact
            };
            
            //Skill
            var skills = _context.Skills
                                .Include(s => s.Student)
                                .Where(x => x.StudentId == student.Id)
                                .ToList();


            vm.Skills = new List<Skill1>();

            foreach (var lj in skills)
            {
                var sk = new Skill1
                {
                    Id = lj.Id,
                    SkillName = lj.Skills
                };

                vm.Skills.Add(sk);
            }

            //educ
            var educationBGs = _context.EducationBgs
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.EducationBgs = new List<Education1>();

            foreach (var lj in educationBGs)
            {
                var ed = new Education1
                {
                    Id = lj.Id,
                    School = lj.School,
                    Course = lj.Course,
                    Year = (int)lj.Year,
                    Address = lj.Address
                };

                vm.EducationBgs.Add(ed);
            }

            //trainings
            var trainings = _context.Trainings
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Trainings = new List<Trainings1>();

            foreach (var lj in trainings)
            {
                var tr = new Trainings1
                {
                    Id = lj.Id,
                    Trainingname = lj.Trainingname,
                    Address = lj.Address,
                    Year = (int)lj.Year
                };

                vm.Trainings.Add(tr);
            }

            //Experience
            var experiences = _context.Experiences
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Experiences = new List<Experiences1>();

            foreach (var lj in experiences)
            {
                var ex = new Experiences1
                {
                    Id = lj.Id,
                    Companyname = lj.Companyname,
                    Role = lj.Role,
                    Year = (int)lj.Year,
                    Address = lj.Address,
                    Description = lj.Description
                };

                vm.Experiences.Add(ex);
            }
            
            //Emergency
            var emergencies = _context.Emergencies
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Emergencies = new List<Emergency1>();

            foreach (var lj in emergencies)
            {
                var em = new Emergency1
                {
                    Id = lj.Id,
                    Firstname = lj.Firstname,
                    Lastname = lj.Lastname,
                    Address = lj.Address,
                    ContactNumber = lj.ContactNumber
                };

                vm.Emergencies.Add(em);
            }



            return View(vm);
        }

        [HttpPost]
        public IActionResult Insert(HomeModel model)
        {
            Title = model.Title;
            Description = model.Description;
            Gender = model.Gender;

            return RedirectToAction("Index", "Sample");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}