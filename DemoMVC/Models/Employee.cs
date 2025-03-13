using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Employee
    {
 
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public required string PersonId { get; set; } 

   
        [Required]
        [StringLength(100)]
        public required string FullName { get; set; } 

        [StringLength(200)]
        public string? Address { get; set; }


        [Range(18, 100)]
        public int Age { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
    }
}