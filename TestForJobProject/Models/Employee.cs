using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestForJobProject.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adreess is required.")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "DOB is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "1950-01-01", "2024-05-23", ErrorMessage = "DOB must be in 01/01/1950 and today's date range")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(100, 50000, ErrorMessage = "Salary must be between 100 and 50000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Active State is required.")]
        public bool IsActive { get; set; }
    }
}
