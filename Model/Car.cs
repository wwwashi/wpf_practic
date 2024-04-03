using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Car
{
    public int Idcar { get; set; }

    public string Model { get; set; } = null!;

    public string Number { get; set; } = null!;

    public int Capacity { get; set; }

    public int Trunkid { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual Trunktype Trunk { get; set; } = null!;
}
