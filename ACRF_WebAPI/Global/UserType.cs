using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Global
{
    public static class UserType
    {
        public const string AdminUser = "ADMIN";
        public const string VendorUser = "VENDOR";
        public const string EmployeeUser = "EMPLOYEE";

        public const string AdminVendor = "ADMIN,VENDOR";

        public const string AdminUserShort = "A";
        public const string VendorUserShort = "V";
    }
}