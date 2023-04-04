using System;
using System.Collections.Generic;

namespace AutoserviceBackCSharp.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentCategory { get; set; }

    public virtual ICollection<Detail> Details { get; } = new List<Detail>();

    public virtual ICollection<Category> InverseParentCategoryNavigation { get; } = new List<Category>();

    public virtual Category? ParentCategoryNavigation { get; set; }
}
