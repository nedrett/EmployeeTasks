namespace EmployeeTasks.Data.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using static Constants.Employee;

    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        [DisplayName("Full Name")]
        public string FullName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; } = null!;

        [Required]
        [DisplayName("Phone Number")]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Salary { get; set; }

        public int CompletedTasksCount { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }
}
