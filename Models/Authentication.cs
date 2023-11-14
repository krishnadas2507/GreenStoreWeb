using System;
using System.Collections.Generic;

namespace GreenStoreWeb.Models;

public partial class Authentication
{
    public int Userid { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Vegelist> Vegelists { get; set; } = new List<Vegelist>();

    public virtual ICollection<Vegetableprice> Vegetableprices { get; set; } = new List<Vegetableprice>();
}
