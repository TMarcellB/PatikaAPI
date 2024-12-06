using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PatikaAPI.Models;

public partial class Betegseg
{
    public int Id { get; set; }

    public string Megnevezes { get; set; } = null!;

    public string Leiras { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Kezel> Kezels { get; set; } = new List<Kezel>();
}
