using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Experience
{
    public int Id { get; set; }

    public string? Companyname { get; set; }

    public string? Role { get; set; }

    public int? Year { get; set; }

    public int? StudentId { get; set; }

    public string? Address { get; set; }
    public string? Description { get; set; }

    public virtual Student? Student { get; set; }
}
