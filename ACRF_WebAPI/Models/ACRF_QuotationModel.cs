using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ACRF_WebAPI.Models
{
    public class ACRF_QuotationModel
    {
        [Key]
        public int QuotationId { get; set; }

        [Required(ErrorMessage = "Vendor Id can't be blank!")]
        public int VendorId { get; set; }

        
        [Required(ErrorMessage="From Mail can't be blank!")]
        [MaxLength(100)]
        public string FromMail { get; set; }

        
        
        [Required(ErrorMessage="Client Mail can't be blank!")]
        [MaxLength(100)]
        public string ClientMail { get; set; }

        [MaxLength(100)]
        public string CC { get; set; }

        
        [MaxLength(100)]
        public string BCC { get; set; }

        
        [Required(ErrorMessage="Mail Subject can't be blank!")]
        [MaxLength(1000)]
        public string MailSubject { get; set; }

        
        [Required(ErrorMessage="Quotation Status can't be blank!")]
        [MaxLength(50)]
        public string QuotationStatus { get; set; }


        
        public DateTime CreatedOn { get; set; }


        [MaxLength(50)]
        public string CreatedBy { get; set; }

        
        public DateTime UpdatedOn { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }




        public string DCityName { get; set; }
        public string TariffMode { get; set; }
        public string Origin { get; set; }
        public List<ACRF_QuotationDetailsModel> ACRF_QuotationDetailsModelList { get; set; }

    }



    public class Paged_ACRF_QuotationDetailsModel
    {
        public List<ACRF_QuotationModel> ACRF_QuotationDetailsModelList { get; set; }

        public int PageCount { get; set; }
    }




    public class QuotationStatusModel
    {
        [Key]
        public int QuotationId { get; set; }

        [Required(ErrorMessage = "Vendor Id can't be blank!")]
        public int VendorId { get; set; }


        [Required(ErrorMessage = "Quotation Status can't be blank!")]
        [MaxLength(50)]
        public string QuotationStatus { get; set; }


        public DateTime UpdatedOn { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }



    }


}