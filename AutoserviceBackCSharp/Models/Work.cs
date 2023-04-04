using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class Work
{
    public int Id { get; set; }

    public int? Detail { get; set; }

    public float? DetailPrice { get; set; }

    public float WorkPrice { get; set; }

    public int Order { get; set; }

    public virtual Detail? DetailNavigation { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;
}
