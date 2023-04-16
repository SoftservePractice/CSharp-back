using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AutoserviceBackCSharp.Models;

public partial class Work
{
    public int Id { get; set; }

    [Range(0,int.MaxValue,ErrorMessage = "Value must be non-negative")]
    public int? Detail { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Value must be non-negative")]
    public float? DetailPrice { get; set; }

    [Required(ErrorMessage = "Не указана WorkPrice")]
    [Range(0, float.MaxValue, ErrorMessage = "Value must be non-negative")]
    public float WorkPrice { get; set; }

    [Required(ErrorMessage = "Не указана Order")]
    [Range(0, int.MaxValue, ErrorMessage = "Value must be non-negative")]
    public int Order { get; set; }
    [JsonIgnore]
    public virtual Detail? DetailNavigation { get; set; }
    [JsonIgnore]
    public virtual Order OrderNavigation { get; set; } = null!;
}
