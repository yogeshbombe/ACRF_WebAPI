using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class VendorDashboard
    {
    }

    public class QuotationStatusCount
    {
        public int InProgress { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        public int OnHold { get; set; }
    }

    public class DisplayChart
    {
        public List<string> Count { get; set; }
        public List<string> Text { get; set; }
    }

    public class DisplayMultiChart
    {
        public List<string> Count1 { get; set; }
        public List<string> Count2 { get; set; }
        public List<string> Text { get; set; }
    }


    public class QuotationSentCompletedCount
    {
        public int Sent { get; set; }
        public int Completed { get; set; }
    }


    public class MonthlySale
    {
        public decimal SaleAmount { get; set; }
        public string MonthName { get; set; }
    }

}