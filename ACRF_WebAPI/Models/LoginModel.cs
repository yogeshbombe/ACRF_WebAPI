using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class LoginModel
    {

        [Display(Name = "USER NAME")]
        [Required(ErrorMessage = "User Name can't be blank!")]
        [MaxLength(50)]
        public string UserName { get; set; }


        [Display(Name = "PASSWORD")]
        [Required(ErrorMessage = "Password can't be blank!")]
        [MaxLength(50)]
        public string Password { get; set; }


    }



    public class AuthModel
    {

        [Display(Name = "USER NAME")]
        [Required(ErrorMessage = "User Name can't be blank!")]
        [MaxLength(50)]
        public string UserName { get; set; }

        
    }


    public class adminLoginPasswordModel
    {
        [Required(ErrorMessage="Id can't be blank!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Old Password can't be blank!")]
        public string old_password { get; set; }

        [Required(ErrorMessage = "Confirm Password can't be blank!")]
        public string confirm_password { get; set; }

        [Required(ErrorMessage = "New Password can't be blank!")]
        public string new_password { get; set; }

        public string UpdatedBy { get; set; }
    }

}