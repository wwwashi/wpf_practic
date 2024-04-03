using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Orderservice
{
    public int Idordser { get; set; }

    public int? Ordersid { get; set; }

    public int? Servicesid { get; set; }

    public virtual Order? Orders { get; set; }

    public virtual Service? Services { get; set; }
}
