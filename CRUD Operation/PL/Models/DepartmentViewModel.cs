using DAL.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "must not exceed 100 And Not Less Than 5")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
