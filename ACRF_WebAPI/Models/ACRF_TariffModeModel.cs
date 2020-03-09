using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_TariffModeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tariff Mode can't be blank!")]
        [MaxLength(100)]
        public string TariffMode { get; set; }


        [MaxLength(50)]
        public string CreatedBy { get; set; }


        public DateTime CreatedOn { get; set; }


        [MaxLength(50)]
        public string UpdatedBy { get; set; }


        public DateTime UpdatedOn { get; set; }

    }


    public class Paged_ACRF_TariffModeModel
    {
        public List<ACRF_TariffModeModel> ACRF_TariffModeModelList { get; set; }

        public int PageCount { get; set; }
    }


}