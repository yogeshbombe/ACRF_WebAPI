using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ACRF_CargoRatesModel
    {
        [Key]
        public int Id { get; set; }
        //[Key]
        public int Project_Id { get; set; }

        
        //[Required(ErrorMessage="Project can't be blank!")]
        public string ProjectName { get; set; }
        
        
        [Required(ErrorMessage="TrackerType Can't be blank!")]
        public string TrackerType { get; set; }


        //[Required(ErrorMessage = "Origin Country Can't be blank!")]
        //[MaxLength(10)]
        public string TrackerURL { get; set; }

        public string TrackerToken { get; set; }
        //[Required(ErrorMessage="Origin City can't be blank!")]
        //[MaxLength(10)]
        public string TrackerUserName { get; set; }

        
        [MaxLength(100)]
        public string TrackerPassword { get; set; }




        //[Required(ErrorMessage = "Destination Country Can't be blank!")]
        //[MaxLength(10)]
        public string assignInTracker { get; set; }


        //[Required(ErrorMessage = "Destination City can't be blank!")]
        //[MaxLength(10)]
        //public string DCityCode { get; set; }


        //[MaxLength(100)]
        //public string DAirportName { get; set; }




        
        //public decimal MinPrice { get; set; }

        //public decimal MinWeight { get; set; }

        //public decimal Normal { get; set; }

        //public decimal plus45 { get; set; }

        //public decimal plus100 { get; set; }

        //public decimal plus250 { get; set; }

        //public decimal plus300 { get; set; }

        //public decimal plus500 { get; set; }

        //public decimal plus1000 { get; set; }

        //public decimal FSCMin { get; set; }

        //public decimal FSCKg { get; set; }

        //public decimal WSCMin { get; set; }

        //public decimal WSCKg { get; set; }

        //public decimal XrayMin { get; set; }

        //public decimal XrayKg { get; set; }

        //public decimal MccMin { get; set; }

        //public decimal MccKg { get; set; }

        //public decimal CtcMin { get; set; }

        //public decimal CtcKg { get; set; }

        //public decimal Oth1 { get; set; }

        //public decimal Oth2 { get; set; }

        //public decimal Dgr { get; set; }

        //public decimal GrossWeight { get; set; }

        //public decimal TotalCost { get; set; }



        //[MaxLength(50)]
        //public string CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }

        //[MaxLength(50)]
        //public string UpdatedBy { get; set; }

        //public DateTime UpdatedOn { get; set; }



        //public string DCity { get; set; }
        //public string DCountry { get; set; }
        //public string OCity { get; set; }
        //public string OCountry { get; set; }
        //public string TariffMode { get; set; }
        //public string Airline { get; set; }

        //public string AirlinePhoto { get; set; }

        //public string AirlineDemoPhoto { get; set; }
        
    }




    public class Paged_ACRF_CargoRatesModel
    {
        public List<ACRF_CargoRatesModel> data { get; set; }

        public int PageCount { get; set; }
    }




    public class CargoRatesModel
    {
        [Key]
        public int Id { get; set; }

        public int AirlineId { get; set; }

        public int TariffModeId { get; set; }

        [MaxLength(10)]
        public string OCountryCode { get; set; }

        [MaxLength(10)]
        public string OCityCode { get; set; }


        [MaxLength(100)]
        public string OAirportName { get; set; }


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




        public string DCity { get; set; }
        public string DCountry { get; set; }
        public string OCity { get; set; }
        public string OCountry { get; set; }
        public string TariffMode { get; set; }
        public string Airline { get; set; }
        public decimal Rate1 { get; set; }
        public decimal Rate2 { get; set; }
        public decimal Rate3 { get; set; }
        public bool IsRate1 { get; set; }
        public bool IsRate2 { get; set; }
        public bool IsRate3 { get; set; }

        public bool IsSelect { get; set; }


        public string AirlinePhoto { get; set; }

        public string AirlineDemoPhoto { get; set; }

    }



    //public class CargoRatesForMulitSelectModel
    //{
    //    public string Origin { get; set; }

    //    public int TariffMode { get; set; }

    //    public int GWeight { get; set; }

    //    public int VendorId { get; set; }

    //    public string destination { get; set; }

    //    //public List<ACRF_DestinationSearchModel> objDestList { get; set; }

    //    //public List<ACRF_AirlinesSearchModel> objAirlinesList { get; set; }

    //    public string[] objDestList { get; set; }

    //    public string[] objAirlinesList { get; set; }

        
    //}

}