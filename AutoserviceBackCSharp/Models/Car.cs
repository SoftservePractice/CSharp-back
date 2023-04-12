using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class Car
{
    public int Id { get; set; }

    public string Mark { get; set; } = null!;

    public DateOnly Year { get; set; }

    public string Vin { get; set; } = null!;

    public string CarNumber { get; set; } = null!;

    public int Client { get; set; }

    [JsonIgnore]
    public virtual Client ClientNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
