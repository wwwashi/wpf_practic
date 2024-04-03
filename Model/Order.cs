using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PracticTaxi.Model;

public partial class Order
{
    public int Idorders { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string Startaddress { get; set; } = null!;

    public string Endaddress { get; set; } = null!;

    public decimal Cost { get; set; }

    public int Statusid { get; set; }

    public int? Usersid { get; set; }

    public int? Driverid { get; set; }

    public int Payid { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();

    public virtual Pay Pay { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Statusorder Status { get; set; }

    public virtual User? Users { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Startaddress))
            errors.Add(new ValidationResult("Укажите адрес подачи"));

        if (string.IsNullOrEmpty(Endaddress))
            errors.Add(new ValidationResult("Укажите адрес назначения"));
        return errors;
    }
}
