using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Global
{
    public class GlobalVariables
    {
        public static string CompanyName;
        public static string Address1;
        public static string Address2;
        public static string Address3;
        public static string City;
        public static string State;
        public static string Country;
        public static string PinCode;
        public static string CompanyLogo;
        public static string CompanyTheme;
        public static string DecorationRate;
        public static string WifiValetParkingRate;

        public static int gRowsPerPage;

        public static string PGUserName;
        public static string PGPassword;
        public static string PGDatabase;
        public static int PGPort;
        public static string PGServer;


        //public static string gSessionId;

        public static bool gLicenseTrail;

        public static int gBanquetLimit;
        public static int gUserLimit;
        public static int gFoodMenuLimit;

        public static string gUserType;
        public static string gUser;

        public static SqlConnection gConn;
        public static SqlConnection gConn_New;


        public static string gConnectionString;
    }


    public struct RESPONSE_STATUS
    {
        public const string SUCCESS = "Success";
        public const string FAILED = "Failed";

        public const string AUTH_FAILED = "Authentication Failed";
        public const string AUTH_TOKEN_MISSING = "Authentication Token Missing";
        public const string AUTH_TOKEN_EXPIRED = "Authentication Token Expired";
        public const string AUTH_UNAUTHORIZED = "Authorization Failed";

    }
}