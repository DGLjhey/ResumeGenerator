using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Summary { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public virtual ICollection<EducationBg> EducationBgs { get; } = new List<EducationBg>();

    public virtual ICollection<Emergency> Emergencies { get; } = new List<Emergency>();

    public virtual ICollection<Experience> Experiences { get; } = new List<Experience>();

    public virtual ICollection<Skill> Skills { get; } = new List<Skill>();

    public virtual ICollection<Training> Training { get; } = new List<Training>();
}
