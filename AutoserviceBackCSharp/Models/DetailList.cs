using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class DetailList
{
    public int Id { get; set; }

    public int Warehouse { get; set; }

    public int Detail { get; set; }

    public int Count { get; set; }

    public virtual Detail DetailNavigation { get; set; } = null!;

    public virtual Warehouse WarehouseNavigation { get; set; } = null!;
}
