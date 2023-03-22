using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Emergency
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Address { get; set; }

    public string? ContactNumber { get; set; }

    public int? StudentId { get; set; }

    public string? Description { get; set; }

    public virtual Student? Student { get; set; }
}
