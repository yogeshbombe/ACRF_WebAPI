using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_EmployeeDetailsModel
    {
        [Key]
        public int Id { get; set; }
	
        
        [Required(ErrorMessage="Vendor Id can't be blank!")]
        public int VendorId { get; set; }
	
        
        [Required(ErrorMessage="Employee No can't be blank!")]
        [MaxLength(10)]
        public string EmployeeNo { get; set; }
	
        
        [Required(ErrorMessage="Employee Name can't be blank!")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }
	
        
        [MaxLength(100)]
        public string Address { get; set; }
	
        
        [MaxLength(100)]
        public string ContactName { get; set; }
	
        
        [MaxLength(20)]
        public string Mobile { get; set; }
	
        
        [MaxLength(100)]
        [Required(ErrorMessage="Email can't be blank!")]
        public string Email { get; set; }
	
        
        [MaxLength(25)]
        public string FAX { get; set; }
	
        
        [MaxLength(100)]
        public string SkypeId { get; set; }
	
        
        
	
        
        [MaxLength(200)]
        public string MiscInfo { get; set ;}
	
        
        [MaxLength(100)]
        //[Required(ErrorMessage = "Password can't be blank!")]
        public string Password { get; set; }
	
        
        
        public int CountryId { get; set; }
	
        
        [MaxLength(10)]
        public string PostalCode { get; set; }
	
        
        public DateTime LastLogin { get; set; }


        [MaxLength(50)]
        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        [MaxLength(50)]
        public string UpdatedBy { get; set; }


        public DateTime UpdatedOn { get; set; }


        public string Country { get; set; }

        public string VendorName { get; set; }

        
    }



    public class Paged_ACRF_EmployeeDetailsModel
    {
        public List<ACRF_EmployeeDetailsModel> ACRF_EmployeeDetailsModelList { get; set; }

        public int PageCount { get; set; }
    }

}