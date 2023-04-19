using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public int Client { get; set; }

    public string? Content { get; set; }

    public bool? Rating { get; set; }

    public int Order { get; set; }
    public virtual Client ClientNavigation { get; set; } = null!;
    public virtual Order OrderNavigation { get; set; } = null!;
}
