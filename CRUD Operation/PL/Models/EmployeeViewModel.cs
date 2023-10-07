using DAL.Entites;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } 
        [Required(ErrorMessage = "Must entered your name")]
        [MaxLength(50, ErrorMessage = "Name must exceed 50 character")]
        [MinLength(5, ErrorMessage = "Minimal length of Character is 5")]
        public string Name { get; set; }
        [Range(20, 60, ErrorMessage = "Age must between 20 and 60")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,10}-[a-zA-Z]{1,40}-[a-zA-Z]{1,40}-[a-zA-Z]{1,40}$"
            , ErrorMessage = "Address must like this '123-street-region-city'")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        [Range(4000, 8000, ErrorMessage = "Salary must between 4000 and 8000")]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
    }
}
