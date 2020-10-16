using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_VendorDetailsModel
    {
        [Key]
        public int Id { get; set; }
	
        [Required(ErrorMessage="Project Name can't be blank!")]
        [MaxLength(100)]
        public string ProjectName { get; set; }


        //[MaxLength(200)]
        //public string Address { get; set; }


        [MaxLength(200)]
        public string ManagerName { get; set; }
	
        
        [MaxLength(20)]
        public string Mobile { get; set; }
	
        
        [MaxLength(100)]
        [Required(ErrorMessage = "Manager Email can't be blank!")]
        public string Email { get; set; }


        //[MaxLength(25)]
        //public string FAX { get; set ;}
	
        
        [MaxLength(100)]
        public string SkypeId { get; set ; }
	
        
        //[MaxLength(100)]
        //public string Website { get; set; }


        //[MaxLength(200)]
        //public string MiscInfo { get; set; }

	
        
       // [MaxLength(100)]
        //[Required(ErrorMessage = "Login Password can't be blank!")]
        public string Password { get; set; }


        //public int CountryId { get; set; }


        //[MaxLength(20)]
        //public string PostalCode { get; set; }
        
        
        public DateTime LastLogin { get; set; }
	
        
        [MaxLength(50)]
        public string CreatedBy { get; set ; }
	
        
        public DateTime CreatedOn { get; set ; }
	
        
        [MaxLength(50)]
        public string UpdatedBy { get; set;}


        public DateTime UpdatedOn { get; set; }

        //[Required(ErrorMessage = "Provide current sprint start Date")]
        public string SprintStartDate { get; set; }

        //[Required(ErrorMessage = "Provide current sprint end Date")]
        public string SprintEndDate { get; set; }

        //[Required(ErrorMessage = "Provide current sprint Name")]
        //[MaxLength(100)]
        public string CurrentSprintName { get; set; }

        //[Required(ErrorMessage = "Provide current sprint Development hours")]
        public int Devhours { get; set; }

        //[Required(ErrorMessage = "Provide current sprint Testing hours")]
        public int Testhours { get; set; }


        [MaxLength(500)]
        public string ProfilePicture { get; set; }

    }



    public class Paged_ACRF_VendorDetailsModel
    {
        public List<ACRF_VendorDetailsModel> ACRF_VendorDetailsModelList { get; set; }

        public int PageCount { get; set; }
    }
}