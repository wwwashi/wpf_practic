using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PracticTaxi.Model;

public partial class User
{
    public int Idusers { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Midname { get; set; }

    public string Mail { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int Roleid { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Surname) || !(Surname.Length >= 3 && Surname.Length <= 20))
            errors.Add(new ValidationResult("Фамилия должна быть от 3 до 20 символов."));

        if (string.IsNullOrEmpty(Name) || !(Name.Length >= 2 && Name.Length <= 15))
            errors.Add(new ValidationResult("Имя должно быть от 3 до 15 символов."));

        if (string.IsNullOrEmpty(Phone) || Phone.Length <= 11)
            errors.Add(new ValidationResult("Телефон может состоять от 11 до 22 символов"));

        if (string.IsNullOrEmpty(Login))
            errors.Add(new ValidationResult("Логин не должен быть пустым."));

        if (string.IsNullOrEmpty(Password))
            errors.Add(new ValidationResult("Пароль не должен быть пустым."));

        return errors;
    }
}
