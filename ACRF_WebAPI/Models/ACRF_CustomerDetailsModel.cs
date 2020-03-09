using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_CustomerDetailsModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name can't be blank!")]
        [MaxLength(100)]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Vendor can't be blank!")]
        public int VendorId { get; set; }


        [MaxLength(200)]
        public string Address { get; set; }


        [MaxLength(100)]
        public string Email { get; set; }


        [MaxLength(200)]
        public string ContactName { get; set; }


        [MaxLength(20)]
        [Required(ErrorMessage = "Customer Mobile can't be blank!")]
        public string Mobile { get; set; }



        [MaxLength(25)]
        public string FAX { get; set; }


        [MaxLength(100)]
        public string SkypeId { get; set; }


        [MaxLength(100)]
        public string Website { get; set; }


        [MaxLength(200)]
        public string MiscInfo { get; set; }


        
        public int CountryId { get; set; }


        [MaxLength(20)]
        public string PostalCode { get; set; }


        public DateTime LastLogin { get; set; }


        [MaxLength(50)]
        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        [MaxLength(50)]
        public string UpdatedBy { get; set; }


        public DateTime UpdatedOn { get; set; }


        public string Country { get; set; } // For display only
        public string VendorName { get; set; } // For display only

    }


    public class Paged_ACRF_CustomerDetailsModel
    {
        public List<ACRF_CustomerDetailsModel> ACRF_CustomerDetailsModelList { get; set; }

        public int PageCount { get; set; }
    }
}