﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "Employee OD can't be blank!")]
        [MaxLength(20)]
        public int EmpID { get; set; }

        //[Required(ErrorMessage = "Employee Name can't be blank!")]
        [MaxLength(50)]
        public string Name { get; set; }

       // [Required(ErrorMessage = "Manager Name can't be blank!")]
        [MaxLength(50)]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "Profile can't be blank!")]
        public int Profile { get; set; }

        [Required(ErrorMessage = "Project can't be blank!")]
        public int ProjectID { get; set; }

        //[Required(ErrorMessage = "Password can't be blank!")]
        [MaxLength(10)]

        [Required]
        public string Status { get; set; }

        public string StatusName { get; set; }
        public string Password { get; set; }

        public string ProfilelName { get; set; }

        public string ProjectName { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
    }

    public class Paged_EmployeeModel
    {
        public List<Employee> EmployeeModelList { get; set; }

        public int PageCount { get; set; }
    }
}