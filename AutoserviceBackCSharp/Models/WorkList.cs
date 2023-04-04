using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class WorkList
{
    public uint Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Price { get; set; }

    public string? Duration { get; set; }
}
