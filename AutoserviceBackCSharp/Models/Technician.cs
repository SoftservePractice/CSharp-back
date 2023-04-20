using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AutoserviceBackCSharp.Models;

public partial class Technician
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Technician name must be specified")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid technician name field length")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Phone number must be specified")]
    [Phone]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Specialization must be specified")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "Invalid specialization field length")]
    public string Specialization { get; set; } = null!;

    public DateOnly StartWork { get; set; }

    public DateOnly StartWorkInCompany { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Value must be non-negative")]
    public float Raiting { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
