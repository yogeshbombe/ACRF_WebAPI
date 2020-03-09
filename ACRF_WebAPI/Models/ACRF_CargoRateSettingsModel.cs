using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_CargoRateSettingsModel
    {
        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }

        public int Rate1 { get; set; }

        public int Rate2 { get; set; }

        public int Rate3 { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Boolean IsRate1 { get; set; }

        public Boolean IsRate2 { get; set; }

        public Boolean IsRate3 { get; set; }

        public string DisplayRate1 { get; set; }

        public string DisplayRate2 { get; set; }

        public string DisplayRate3 { get; set; }



    }
}