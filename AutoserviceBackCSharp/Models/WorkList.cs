using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class WorkList
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public float? Price { get; set; }

    public float? Duration { get; set; }
    [JsonIgnore]
    public virtual ICollection<Work> Works { get; } = new List<Work>();
}
