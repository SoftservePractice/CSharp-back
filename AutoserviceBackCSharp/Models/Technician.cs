using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class Technician
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public DateOnly StartWork { get; set; }

    public DateOnly StartWorkInCompany { get; set; }

    public float Raiting { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
