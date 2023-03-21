using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Training
{
    public int Id { get; set; }

    public string? Trainingname { get; set; }

    public int? Year { get; set; }

    public string? Address { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
