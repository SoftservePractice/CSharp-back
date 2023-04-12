using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class Detail
{
    public int Id { get; set; }

    public string Model { get; set; } = null!;

    public string VendorCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CompatibleVehicles { get; set; } = null!;

    public int Category { get; set; }
    [JsonIgnore]
    public virtual Category CategoryNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<DetailList> DetailLists { get; } = new List<DetailList>();
    [JsonIgnore]
    public virtual ICollection<Work> Works { get; } = new List<Work>();
}
