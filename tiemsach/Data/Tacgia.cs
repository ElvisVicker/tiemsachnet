using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tiemsach.Data;

public partial class Tacgia
{
    public long Id { get; set; }

    public string Ten { get; set; } = null!;

    public bool Tinhtrang { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Namsinh must be greater than 0.")]
    [LessThanCurrentYear(ErrorMessage = "Namsinh must be less than the current year.")]
    public int Namsinh { get; set; }
    public class LessThanCurrentYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year)
            {
                if (year > DateTime.Now.Year)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
