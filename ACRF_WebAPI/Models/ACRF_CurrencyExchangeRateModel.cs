using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_CurrencyExchangeRateModel
    {
        [Key]
        public int Id { get; set; }
	
        [Required(ErrorMessage="Foreign Country can't be blank!")]
        public int FCountryId { get; set; }
	
        
        [Required(ErrorMessage="Foreign Currency can't be blank!")]
        [MaxLength(100)]
        public string FCurrency { get; set; }


        [Required(ErrorMessage = "Foreign Currency Code can't be blank!")]
        [MaxLength(10)]
        public string FCurrencyCode { get; set; }


        [Required(ErrorMessage = "Unit can't be blank!")]
        public int Unit { get; set; }


        [Required(ErrorMessage = "Import Rate can't be blank!")]
        public decimal ImportRate { get; set; }


        [Required(ErrorMessage = "Export Rate can't be blank!")]
        public decimal ExportRate { get; set; }
	
        
        [MaxLength(50)]
        public string CreatedBy { get; set; }
	
                
        public DateTime CreatedOn { get; set; }
	
        
        [MaxLength(50)]
        public string UpdatedBy { get; set; }


        public DateTime UpdatedOn { get; set; }



        public string Country { get; set; } // for showing Country in List

    }



    public class Paged_ACRF_CurrencyExchangeRateModel
    {
        public List<ACRF_CurrencyExchangeRateModel> ACRF_CurrencyExchangeRateModelList { get; set; }

        public int PageCount { get; set; }
    }

}