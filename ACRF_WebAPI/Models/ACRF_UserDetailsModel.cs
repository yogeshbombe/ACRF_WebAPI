using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_UserDetailsModel
    {
        [Key]
        public int id { get; set; }


        public string fullName { get; set; }

        public string email { get; set; }


        public string token { get;set ; }


        public string userType { get; set; }


        public string profileimage { get; set; }


        //[MaxLength(50)]
        //[Display(Name = "USER ID")]
        //[Required(ErrorMessage = "User Id can't be blank!")]
        //public string UserId { get; set; }





        //[Display(Name = "USERTYPE")]
        //[MaxLength(15)]
        //public string UserType { get; set; }


        //[Display(Name = "FIRST NAME")]
        //[MaxLength(50)]
        //public string FirstName { get; set; }


        //[Display(Name = "LAST NAME")]
        //[MaxLength(100)]
        //public string LastName { get; set; }


        //[Display(Name = "Address")]
        //[MaxLength(150)]
        //public string Address { get; set; }


        //[Display(Name = "LASTLOGIN")]
        //public DateTime LastLoginTime { get; set; }


    }
}