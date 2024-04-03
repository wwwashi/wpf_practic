using System;
using System.Collections.Generic;

namespace PracticTaxi.Model;

public partial class Role
{
    public int Idrole { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
