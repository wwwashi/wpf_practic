using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Statusorder
{
    public int Idstatus { get; set; }

    public string NameStatus { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
