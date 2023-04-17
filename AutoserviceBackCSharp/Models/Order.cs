using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? Client { get; set; }

    public int? Technician { get; set; }

    public DateOnly? Start { get; set; }

    public DateOnly? End { get; set; }

    public int? FinalPrice { get; set; }

    public int? Car { get; set; }

    public int? CarMileage { get; set; }

    public DateOnly? AppointmentTime { get; set; }
    public virtual Car? CarNavigation { get; set; }
    public virtual Client ClientNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();
    public virtual Technician? TechnicianNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<Work> Works { get; } = new List<Work>();
}
