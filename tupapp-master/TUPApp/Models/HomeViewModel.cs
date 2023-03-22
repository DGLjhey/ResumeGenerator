namespace TUPApp.Models
{
    public class HomeViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Summary { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public List<Skill1> Skills { get; set; }

        public List<Education1> EducationBgs { get; set; }

        public List<Trainings1> Trainings { get; set; }
        public List<Experiences1> Experiences { get; set; }
        public List<Emergency1>Emergencies { get; set; }

    }

    public class Skill1
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
    }

    public class Education1
    {
        public int Id { get; set; }
        public string School{ get; set; }
        public string Course { get; set; }
        public string Address { get; set; }
        public int Year { get; set; }
        public int YearEnd { get; set; }
    }

    public class Trainings1
    {
        public int Id { get; set; }
        public string Trainingname { get; set; }
        public string Address { get; set; }
        public int Year { get; set; }
    }

    public class Experiences1
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Companyname { get; set; }
        public int Year { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }

    public class Emergency1
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }

    }
}
