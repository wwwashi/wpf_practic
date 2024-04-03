using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Driver
{
    public int Iddriver { get; set; }

    public int Carid { get; set; }

    public int Usersid { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User Users { get; set; } = null!;
}
