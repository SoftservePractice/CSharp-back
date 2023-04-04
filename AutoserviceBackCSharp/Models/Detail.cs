using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class Detail
{
    public int Id { get; set; }

    public string Model { get; set; } = null!;

    public string VendorCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CompatibleVehicles { get; set; } = null!;

    public int Category { get; set; }

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<DetailList> DetailLists { get; } = new List<DetailList>();

    public virtual ICollection<Work> Works { get; } = new List<Work>();
}
