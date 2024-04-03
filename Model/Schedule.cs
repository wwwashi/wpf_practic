using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticTaxi.Model;

public partial class Schedule
{
    public int Idschedule { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Starttime { get; set; }

    public TimeOnly Endtime { get; set; }

    public int Driverid { get; set; }

    public virtual Driver Driver { get; set; } = null!;
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        // Проверка даты
        if (Date == null)
            errors.Add(new ValidationResult("Укажите дату"));

        if (Starttime == null)
            errors.Add(new ValidationResult("Укажите время начала смены"));
        
        if (Endtime == null)
            errors.Add(new ValidationResult("Укажите время начала смены"));
        
        if (Driverid == null)
            errors.Add(new ValidationResult("Укажите ID водителя"));
        else if (Driverid < 0 || Driverid > 43)
            errors.Add(new ValidationResult("ID водителя должен быть больше 0 и меньше 43"));

        return errors;
    }
}
