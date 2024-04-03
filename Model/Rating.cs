using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Rating
{
    public int Idrating { get; set; }

    public int Grade { get; set; }

    public string Comment { get; set; } = null!;

    public int Ordersid { get; set; }

    public virtual Order Orders { get; set; } = null!;
}
