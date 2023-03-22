using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class EducationBg
{
    public int Id { get; set; }

    public string? School { get; set; }

    public int? Year { get; set; }

    public int? StudentId { get; set; }

    public string? Course { get; set; }

    public string? Address { get; set; }

    public int? YearEnd { get; set; }

    public virtual Student? Student { get; set; }
}
