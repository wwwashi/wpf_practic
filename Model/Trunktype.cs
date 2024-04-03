using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Trunktype
{
    public int Idtrunk { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
