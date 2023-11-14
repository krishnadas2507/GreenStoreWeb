using System;
using System.Collections.Generic;

namespace GreenStoreWeb.Models;

public partial class Vegelist
{
    public int Vegid { get; set; }

    public string? Vegname { get; set; }

    public int? Userid { get; set; }

    public decimal? Price { get; set; }

    public virtual Authentication? User { get; set; }

    public virtual ICollection<Vegetableprice> Vegetableprices { get; set; } = new List<Vegetableprice>();
}
