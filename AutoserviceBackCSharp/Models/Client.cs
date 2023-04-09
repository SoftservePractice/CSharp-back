using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class Client
{
    public int Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? TelegramId { get; set; }

    public string? Email { get; set; }

    public bool IsConfirm { get; set; }

    public virtual ICollection<Car> Cars { get; } = new List<Car>();

    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
