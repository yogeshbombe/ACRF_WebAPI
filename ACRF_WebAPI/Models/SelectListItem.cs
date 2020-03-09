using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }


    public class MultiSelectListItem
    {
        public string itemName { get; set; }
        public string id { get; set; }
    }
}