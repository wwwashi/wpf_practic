using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Service
{
    public int Idservices { get; set; }

    public string Name { get; set; } = null!;

    public decimal Cost { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
