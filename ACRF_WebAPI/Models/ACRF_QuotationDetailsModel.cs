using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_QuotationDetailsModel
    {
        [Key]
        public int Id { get; set; }
	
        
        public int QuotationId { get; set; }
	
        
        public int AirlineId { get; set; }
	
        
        public int TariffModeId { get; set; }
	
        
        [MaxLength(10)]
        public string OCountryCode { get; set; }
	
        
        [MaxLength(10)]
        public string OCityCode { get; set; }
	
        
        [MaxLength(100)]
        public string OAirportName { get; set ;}
	
        [MaxLength(10)]
        public string DCountryCode { get; set; }
	
        [MaxLength(10)]
        public string DCityCode { get; set; }
	
        
        [MaxLength(100)]
        public string DAirportName { get; set; }
	
        
        
        public decimal Slab1 { get; set; }
        public decimal Rate { get; set; }
        public decimal Freight { get; set; }
        public decimal FSC { get; set; }
        public decimal WSC { get; set; }
        public decimal Xray { get; set; }
        public decimal Mcc { get; set; }
        public decimal Ctc { get; set; }
        public decimal AMS { get; set; }
        public decimal TotalCost { get; set; }


	
        public bool IsRate1 { get; set; }
	
        public bool IsRate2 { get; set; }
	
        public bool IsRate3 { get; set; }
	
        public decimal Rate1 { get; set; }
	
        public decimal Rate2 { get; set; }
	
        public decimal Rate3 { get; set; }
	
        public string DisplayRateName1 { get; set; }
	
        public string DisplayRateName2 { get; set; }	
        
        public string DisplayRateName3 { get; set; }


	
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }


        public string Airline { get; set; }
        public string AirlinePhoto { get; set; }
        public string AirlineDemoPhoto { get; set; }


    }
}