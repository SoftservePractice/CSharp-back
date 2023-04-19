using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AutoserviceBackCSharp.Models;

public partial class Technician
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указано имя техника")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Не указан телефон техника")]
    [Phone]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Не указана специализация техника")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
    public string Specialization { get; set; } = null!;

    public DateOnly StartWork { get; set; }

    public DateOnly StartWorkInCompany { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Value must be non-negative")]
    public float Raiting { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
