using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Pay
{
    public int Idpay { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
