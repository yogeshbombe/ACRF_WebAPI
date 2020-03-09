using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_AirportsModel
    {
        [Key]
        public int Id { get; set; }
	
        
        [Required(ErrorMessage = "Short Code can't be blank!")]
        [MaxLength(10)]
        public string ShortCode { get;set; }
	
        
        [Required(ErrorMessage = "Airports can't be blank!")]
        [MaxLength(100)]
        public string Airports { get; set; }


        [Required(ErrorMessage = "Country can't be blank!")]
        public int CountryId { get; set; }


        [MaxLength(50)]
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        [MaxLength(50)]
        public string UpdatedBy { get; set; }
        
        public DateTime UpdatedOn { get; set; }


    }
}