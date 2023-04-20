using System.ComponentModel.DataAnnotations;

namespace AutoserviceBackCSharp.Models;

public partial class Work
{
    public int Id { get; set; }

    [Range(0,int.MaxValue,ErrorMessage = "Value must be non-negative")]
    public virtual int? Detail { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Value must be non-negative")]
    public float? DetailPrice { get; set; }

    [Required(ErrorMessage = "WorkPrice must be specified")]
    [Range(0, float.MaxValue, ErrorMessage = "Value must be non-negative")]
    public float WorkPrice { get; set; }

    [Required(ErrorMessage = "Order must be specified")]
    [Range(0, int.MaxValue, ErrorMessage = "Value must be non-negative")]
    public virtual int Order { get; set; }

    public virtual int WorkList { get; set; }

    public virtual Detail? DetailNavigation { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual WorkList WorkListNavigation { get; set; } = null!;
}
