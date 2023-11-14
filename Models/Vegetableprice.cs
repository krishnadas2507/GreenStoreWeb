using System;
using System.Collections.Generic;

namespace GreenStoreWeb.Models;

public partial class Vegetableprice
{
    public int Priceid { get; set; }

    public int? Vegid { get; set; }

    public int? Userid { get; set; }

    public decimal? Price { get; set; }

    public virtual Authentication? User { get; set; }

    public virtual Vegelist? Veg { get; set; }
}
