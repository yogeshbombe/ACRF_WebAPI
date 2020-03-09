using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_AirlinesModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Airline Code can't be blank!")]
        [MaxLength(5)]
        public string AirlineCode { get; set; }
	
        
        [Required(ErrorMessage = "Sub Code can't be blank!")]
        [MaxLength(5)]
        public string SubCode { get; set; }
	
        
        
        [Required(ErrorMessage = "Airline Name can't be blank!")]
        [MaxLength(100)]
        public string AirlineName { get; set; }
	
	
        
        [Required(ErrorMessage = "Head Quarter Address can't be blank!")]
        [MaxLength(100)]
        public string HQAddress { get; set; }
	
        
        
        [MaxLength(30)]
        public string HQPhone { get; set; }
	
        
        [MaxLength(20)]
        public string HQFAX { get; set; }
	
        
        [MaxLength(50)]
        public string HQEmail { get; set; }
	
        
        [MaxLength(10)]
        public string HQIATAMember { get; set; }
	
	
        [MaxLength(100)]
        public string SalGsaCsaName { get; set; }
	
        
        [MaxLength(100)]
        public string SalAddress { get; set; }
	
        
        [MaxLength(30)]
        public string SalPhone { get; set; }
	
        
        [MaxLength(20)]
        public string SalFAX { get; set; }
	
        
        [MaxLength(50)]
        public string SalEmail { get; set; }
	
	
        [MaxLength(100)]
        public string OprAddress { get; set; }
	
        
        [MaxLength(30)]
        public string OprPhone { get; set; }
	
        
        [MaxLength(20)]
        public string OprFAX { get; set; }


        [MaxLength(50)]
        public string OprEmail { get; set; }


        [MaxLength(50)]
        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        [MaxLength(50)]
        public string UpdatedBy { get; set; }


        public DateTime UpdatedOn { get; set; }


        public string AirlinePhoto { get; set; }

        public string AirlineDemoPhoto { get; set; } // if image not available

    }



    public class Paged_ACRF_AirlinesModel
    {
        public List<ACRF_AirlinesModel> ACRF_AirlinesModelList { get; set; }

        public int PageCount { get; set; }
    }




    public class ACRF_AirlinesSearchModel
    {
        //[Key]
        //public int Id { get; set; }

        //[Required(ErrorMessage = "Airline Code can't be blank!")]
        //[MaxLength(5)]
        //public string AirlineCode { get; set; }


        //[Required(ErrorMessage = "Sub Code can't be blank!")]
        //[MaxLength(5)]
        //public string SubCode { get; set; }



        //[Required(ErrorMessage = "Airline Name can't be blank!")]
        //[MaxLength(100)]
        //public string AirlineName { get; set; }


        //public bool IsSelect { get; set; }

        public string id { get; set; }

        public string itemName { get; set; }
        
    }


}