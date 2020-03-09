using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_DestinationMasterModel
    {
        [Key]
        public int Id { get; set; }
	
        
        [Required(ErrorMessage="City Code can't be blank!")]
        [MaxLength(10)]
        public string CityCode { get; set; }
	
        
        
        [Required(ErrorMessage="City Name can't be blank!")]
        [MaxLength(100)]
        public string CityName { get; set; }
	
        
        [Required(ErrorMessage="Country Code can't be blank!")]
        [MaxLength(10)]
        public string CountryCode { get; set; }
	
        
        [Required(ErrorMessage="Country Name can't be blank!")]
        [MaxLength(100)]
        public string CountryName { get; set; }
	
        
        
        [MaxLength(10)]
        public string TimeDifference { get; set; }
	
        
        [MaxLength(10)]
        public string CustomAirport { get; set; }
	
        
        
        [MaxLength(100)]
        public string AirportName { get; set; }
	
        
        [MaxLength(10)]
        public string ISDCode { get; set; }
	
        
        [MaxLength(100)]
        public string Currency { get; set; }
	
        
        [MaxLength(100)]
        public string State { get; set; }


        [MaxLength(100)]
        public string IATAArea {  get; set; }

        

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }



    }


    public class Paged_ACRF_DestinationMasterModel
    {
        public List<ACRF_DestinationMasterModel> ACRF_DestinationMasterModelList { get; set; }

        public int PageCount { get; set; }
    }



    public class ACRF_DestinationSearchModel
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "City Code can't be blank!")]
        [MaxLength(10)]
        public string CityCode { get; set; }



        [Required(ErrorMessage = "City Name can't be blank!")]
        [MaxLength(100)]
        public string CityName { get; set; }


        public bool IsSelect { get; set; }


    }

}