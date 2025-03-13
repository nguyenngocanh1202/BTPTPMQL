using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
public class Person
    {
        internal string? PersonID;

        [Key] // Khóa chính nhưng không tự động tăng
        public string PersonId { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string FullName { get; set; } = string.Empty;
 
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
         public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
    }
}