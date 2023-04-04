using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<DetailList> DetailLists { get; } = new List<DetailList>();
}
