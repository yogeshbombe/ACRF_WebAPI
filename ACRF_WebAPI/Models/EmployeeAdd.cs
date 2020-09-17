using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ACRF_WebAPI.Models
{
    public class EmployeeAdd
    {
        [Key]
        [Required(ErrorMessage = "Employee OD can't be blank!")]
        [MaxLength(20)]
        public int EmpID { get; set; }



        [Required(ErrorMessage = "Profile can't be blank!")]
        public int Profile { get; set; }

        [Required(ErrorMessage = "Project can't be blank!")]
        public int ProjectID { get; set; }

        //[Required(ErrorMessage = "Password can't be blank!")]
        [MaxLength(10)]

        [Required]
        public string Status { get; set; }

        public string Password { get; set; }
        public int[] Expertise { get; set; }

        public int[] stream { get; set; }
    }
}