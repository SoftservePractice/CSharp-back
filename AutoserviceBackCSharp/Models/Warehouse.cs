using System.Text.Json.Serialization;

namespace AutoserviceBackCSharp.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<DetailList> DetailLists { get; } = new List<DetailList>();
}
