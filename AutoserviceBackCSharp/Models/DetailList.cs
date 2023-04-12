using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class DetailList
{
    public int Id { get; set; }

    public int Warehouse { get; set; }

    public int Detail { get; set; }

    public int Count { get; set; }
    [JsonIgnore]
    public virtual Detail DetailNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Warehouse WarehouseNavigation { get; set; } = null!;
}
