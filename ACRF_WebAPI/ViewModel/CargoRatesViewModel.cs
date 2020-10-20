using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using EnCryptDecrypt;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class CargoRatesViewModel
    {

        #region Create Cargo Rates

        public string CreateCargoRates(ACRF_CargoRatesModel objModel)
        {
            string result = "Error on Saving project settings!";
            try
            {
                //objModel = NullToBlank(objModel);
                objModel.ProjectName = getProjectName(objModel.Project_Id);
                //if (result == "")
                //{
                

                var connection = gConnection.Connection();
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = connection;
                    try
                    {
                        string sqlstr = "";
                    //sqlstr = "insert into ACRF_CargoRates(AirlineId,TariffModeId,OCountryCode,OCityCode,OAirportName,DCountryCode,DCityCode,DAirportName,MinPrice,MinWeight,Normal, "
                    //    + " plus45,plus100,plus250,plus300,plus500,plus1000,FSCMin,FSCKg,WSCMin,WSCKg,XrayMin,XrayKg,MccMin,MccKg,CtcMin, "
                    //    + " CtcKg,Oth1,Oth2,Dgr,GrossWeight,TotalCost,CreatedBy,CreatedOn) values "
                    //    + " (@AirlineId,@TariffModeId,@OCountryCode,@OCityCode,@OAirportName,@DCountryCode,@DCityCode,@DAirportName,@MinPrice,@MinWeight,@Normal, "
                    //    + " @plus45,@plus100,@plus250,@plus300,@plus500,@plus1000,@FSCMin,@FSCKg,@WSCMin,@WSCKg,@XrayMin,@XrayKg,@MccMin,@MccKg,@CtcMin, "
                    //    + " @CtcKg,@Oth1,@Oth2,@Dgr,@GrossWeight,@TotalCost,@CreatedBy,@CreatedOn)";
                    if (objModel.TrackerPassword == null) {
                        objModel.TrackerPassword = "";
                    }
                    if (objModel.TrackerUserName == null)
                    {
                        objModel.TrackerUserName = "";
                    }
                    sqlstr = "INSERT INTO Settings(Project_Id,ProjectName,TrackerType,TrackerURL,TrackerUserName,TrackerPassword,TrackerToken,assignInTracker) values (@projectId,@ProjectName,@TrackerType,@TrackerURL,@TrackerUserName,@TrackerPassword,@TrackerToken,@assignInTracker)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@projectId", objModel.Project_Id);
                        cmd.Parameters.AddWithValue("@ProjectName", objModel.ProjectName);
                        cmd.Parameters.AddWithValue("@TrackerType", objModel.TrackerType);
                        cmd.Parameters.AddWithValue("@TrackerURL", objModel.TrackerURL);
                        cmd.Parameters.AddWithValue("@TrackerToken", EnCryptDecrypt.Encryption.encrypt(objModel.TrackerToken));
                        cmd.Parameters.AddWithValue("@TrackerUserName", objModel.TrackerUserName);
                        cmd.Parameters.AddWithValue("@TrackerPassword", EnCryptDecrypt.Encryption.encrypt(objModel.TrackerPassword.Trim()));
                        cmd.Parameters.AddWithValue("@assignInTracker", objModel.assignInTracker);
                        //cmd.Parameters.AddWithValue("@DAirportName", objModel.DAirportName);
                        //cmd.Parameters.AddWithValue("@MinPrice", objModel.MinPrice);
                        //cmd.Parameters.AddWithValue("@MinWeight", objModel.MinWeight);
                        //cmd.Parameters.AddWithValue("@Normal", objModel.Normal);
                        //cmd.Parameters.AddWithValue("@plus45", objModel.plus45);
                        //cmd.Parameters.AddWithValue("@plus100", objModel.plus100);
                        //cmd.Parameters.AddWithValue("@plus250", objModel.plus250);
                        //cmd.Parameters.AddWithValue("@plus300", objModel.plus300);
                        //cmd.Parameters.AddWithValue("@plus500", objModel.plus500);
                        //cmd.Parameters.AddWithValue("@plus1000", objModel.plus1000);
                        //cmd.Parameters.AddWithValue("@FSCMin", objModel.FSCMin);
                        //cmd.Parameters.AddWithValue("@FSCKg", objModel.FSCKg);
                        //cmd.Parameters.AddWithValue("@WSCMin", objModel.WSCMin);
                        //cmd.Parameters.AddWithValue("@WSCKg", objModel.WSCKg);
                        //cmd.Parameters.AddWithValue("@XrayMin", objModel.XrayMin);
                        //cmd.Parameters.AddWithValue("@XrayKg", objModel.XrayKg);
                        //cmd.Parameters.AddWithValue("@MccMin", objModel.MccMin);
                        //cmd.Parameters.AddWithValue("@MccKg", objModel.MccKg);
                        //cmd.Parameters.AddWithValue("@CtcMin", objModel.CtcMin);
                        //cmd.Parameters.AddWithValue("@CtcKg", objModel.CtcKg);
                        //cmd.Parameters.AddWithValue("@Oth1", objModel.Oth1);
                        //cmd.Parameters.AddWithValue("@Oth2", objModel.Oth2);
                        //cmd.Parameters.AddWithValue("@Dgr", objModel.Dgr);
                        //cmd.Parameters.AddWithValue("@GrossWeight", objModel.GrossWeight);
                        //cmd.Parameters.AddWithValue("@TotalCost", objModel.TotalCost);

                        //cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        //cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Project Settings Added Successfully!";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                        Global.ErrorHandlerClass.LogError(ex);
                        result = ex.Message;
                    }
               // }
                //else
                //{
                //    return result;
                //}
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion


        #region Get One Cargo Rates

        public ACRF_CargoRatesModel GetOneCargoRates(int id)
        {
            ACRF_CargoRatesModel objModel = new ACRF_CargoRatesModel();
            try
            {
                string sqlstr = "select Id, isnull(project_Id,0) as project_Id, isnull(ProjectName,'') as ProjectName,isnull(TrackerType,'') as TrackerType, "
                + " isnull(TrackerURL,'') as TrackerURL, isnull(TrackerUserName,'') as TrackerUserName,isnull(TrackerPassword,'') as TrackerPassword,isnull(TrackerToken,'') as TrackerToken,"
                + " isnull(assignInTracker,'') as assignInTracker from settings where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objModel.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.Project_Id = Convert.ToInt32(sdr["project_Id"].ToString());
                    objModel.ProjectName = sdr["ProjectName"].ToString();
                    objModel.TrackerType = sdr["TrackerType"].ToString();
                    objModel.TrackerURL = sdr["TrackerURL"].ToString();
                    objModel.TrackerUserName = sdr["TrackerUserName"].ToString();
                    objModel.TrackerPassword = EnCryptDecrypt.Encryption.decrypt(sdr["TrackerPassword"].ToString());
                    objModel.TrackerToken = sdr["TrackerToken"].ToString();
                    objModel.assignInTracker = sdr["assignInTracker"].ToString();
                    //objModel.DCityCode = sdr["DCityCode"].ToString();
                    //objModel.DAirportName = sdr["DAirportName"].ToString();
                    //objModel.MinPrice = Convert.ToDecimal(sdr["MinPrice"].ToString());
                    //objModel.MinWeight = Convert.ToDecimal(sdr["MinWeight"].ToString());
                    //objModel.Normal = Convert.ToDecimal(sdr["Normal"].ToString());
                    //objModel.plus45 = Convert.ToDecimal(sdr["plus45"].ToString());
                    //objModel.plus100 = Convert.ToDecimal(sdr["plus100"].ToString());
                    //objModel.plus250 = Convert.ToDecimal(sdr["plus250"].ToString());
                    //objModel.plus300 = Convert.ToDecimal(sdr["plus300"].ToString());
                    //objModel.plus500 = Convert.ToDecimal(sdr["plus500"].ToString());
                    //objModel.plus1000 = Convert.ToDecimal(sdr["plus1000"].ToString());
                    //objModel.FSCMin = Convert.ToDecimal(sdr["FSCMin"].ToString());
                    //objModel.FSCKg = Convert.ToDecimal(sdr["FSCKg"].ToString());
                    //objModel.WSCMin = Convert.ToDecimal(sdr["WSCMin"].ToString());
                    //objModel.WSCKg = Convert.ToDecimal(sdr["WSCKg"].ToString());
                    //objModel.XrayMin = Convert.ToDecimal(sdr["XrayMin"].ToString());
                    //objModel.XrayKg = Convert.ToDecimal(sdr["XrayKg"].ToString());
                    //objModel.MccMin = Convert.ToDecimal(sdr["MccMin"].ToString());
                    //objModel.MccKg = Convert.ToDecimal(sdr["MccKg"].ToString());
                    //objModel.CtcMin = Convert.ToDecimal(sdr["CtcMin"].ToString());
                    //objModel.CtcKg = Convert.ToDecimal(sdr["CtcKg"].ToString());
                    //objModel.Oth1 = Convert.ToDecimal(sdr["Oth1"].ToString());
                    //objModel.Oth2 = Convert.ToDecimal(sdr["Oth2"].ToString());
                    //objModel.Dgr = Convert.ToDecimal(sdr["Dgr"].ToString());
                    //objModel.GrossWeight = Convert.ToDecimal(sdr["GrossWeight"].ToString());
                    //objModel.TotalCost = Convert.ToDecimal(sdr["TotalCost"].ToString());
                    //objModel.CreatedBy = sdr["CreatedBy"].ToString();
                    //objModel.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }

        #endregion


        #region List Cargo Rates

        //public List<ACRF_CargoRatesModel> ListCargoRates()
        //{
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    try
        //    {
        //        //string sqlstr = "select Id, isnull(AirlineId,0) as AirlineId, isnull(TariffModeId,0) as TariffModeId,isnull(OCountryCode,'') as OCountryCode, "
        //        //+ " isnull(OCityCode,'') as OCityCode, isnull(OAirportName,'') as OAirportName,isnull(DCountryCode,'') as DCountryCode, "
        //        //+ " isnull(DCityCode,'') as DCityCode, isnull(DAirportName,'') as DAirportName, isnull(MinPrice,0) as MinPrice, "
        //        //+ " isnull(MinWeight,0) as MinWeight, isnull(Normal,0) as Normal, isnull(plus45,0) as plus45, isnull(plus100,0) as plus100, "
        //        //+ " isnull(plus250,0) as plus250, isnull(plus300,0) as plus300, isnull(plus500,0) as plus500, isnull(plus1000,0) as plus1000, "
        //        //+ " isnull(FSCMin,0) as FSCMin, isnull(FSCKg,0) as FSCKg, isnull(WSCMin,0) as WSCMin, isnull(WSCKg,0) as WSCKg, "
        //        //+ " isnull(XrayMin,0) as XrayMin, isnull(XrayKg,0) as XrayKg, isnull(MccMin,0) as MccMin, isnull(MccKg,0) as MccKg, "
        //        //+ " isnull(CtcMin,0) as CtcMin, isnull(CtcKg,0) as CtcKg, isnull(Oth1,0) as Oth1, isnull(Oth2,0) as Oth2, isnull(Dgr,0) as Dgr, "
        //        //+ " isnull(GrossWeight,0) as GrossWeight, isnull(TotalCost,0) as TotalCost,CreatedBy,CreatedOn, "
        //        //+ " isnull((Select CountryName From ACRF_DestinationMaster D where D.CountryCode=CR.DCountryCode),'') as DCountry, "
        //        //+ " isnull((Select CityName From ACRF_DestinationMaster D where D.CityCode=CR.DCityCode),'') as DCity, "
        //        //+ " isnull((Select CountryName From ACRF_DestinationMaster D where D.CountryCode=CR.OCountryCode),'') as OCountry, "
        //        //+ " isnull((Select CityName From ACRF_DestinationMaster D where D.CityCode=CR.OCityCode),'') as OCity, "
        //        //+ " isnull((Select TariffMode From ACRF_TariffMode D where D.Id=CR.TariffModeId),'') as TariffMode, "
        //        //+ " isnull((Select AirlineName From ACRF_Airlines D where D.Id=CR.AirlineId),'') as Airline "
        //        //+ " from ACRF_CargoRates CR order by Id";


        //        string sqlstr = "select Id, isnull(AirlineId,0) as AirlineId, isnull(TariffModeId,0) as TariffModeId,isnull(OCountryCode,'') as "
        //        + " OCountryCode,  isnull(OCityCode,'') as OCityCode, isnull(OAirportName,'') as OAirportName,isnull(DCountryCode,'')  "
        //        + " as DCountryCode,  isnull(DCityCode,'') as DCityCode, isnull(DAirportName,'') as DAirportName, isnull(MinPrice,0)  "
        //        + " as MinPrice,  isnull(MinWeight,0) as MinWeight, isnull(Normal,0) as Normal, isnull(plus45,0) as plus45,  "
        //        + " isnull(plus100,0) as plus100,  isnull(plus250,0) as plus250, isnull(plus300,0) as plus300, isnull(plus500,0)  "
        //        + " as plus500, isnull(plus1000,0) as plus1000,  isnull(FSCMin,0) as FSCMin, isnull(FSCKg,0) as FSCKg, "
        //        + " isnull(WSCMin,0) as WSCMin, isnull(WSCKg,0) as WSCKg,  isnull(XrayMin,0) as XrayMin, isnull(XrayKg,0)  "
        //        + " as XrayKg, isnull(MccMin,0) as MccMin, isnull(MccKg,0) as MccKg,  isnull(CtcMin,0) as CtcMin, isnull(CtcKg,0)  "
        //        + " as CtcKg, isnull(Oth1,0) as Oth1, isnull(Oth2,0) as Oth2, isnull(Dgr,0) as Dgr,  isnull(GrossWeight,0) as "
        //        + " GrossWeight, isnull(TotalCost,0) as TotalCost,CreatedBy,CreatedOn,  "
        //        + " isnull((Select TariffMode From ACRF_TariffMode D where D.Id=CR.TariffModeId),'') as TariffMode ,  "
        //        + " isnull((Select AirlineName From ACRF_Airlines D where D.Id=CR.AirlineId),'') as Airline  "
        //        + " from ACRF_CargoRates CR order by Id";

        //        var connection = gConnection.Connection();
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(sqlstr, connection);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        SqlDataReader sdr = cmd.ExecuteReader();

        //        while (sdr.Read())
        //        {
        //            ACRF_CargoRatesModel tempobj = new ACRF_CargoRatesModel();
        //            tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
        //            tempobj.AirlineId = Convert.ToInt32(sdr["AirlineId"].ToString());
        //            tempobj.TariffModeId = Convert.ToInt32(sdr["TariffModeId"].ToString());
        //            tempobj.OCountryCode = sdr["OCountryCode"].ToString();
        //            tempobj.OCityCode = sdr["OCityCode"].ToString();
        //            tempobj.OAirportName = sdr["OAirportName"].ToString();
        //            tempobj.DCountryCode = sdr["DCountryCode"].ToString();
        //            tempobj.DCityCode = sdr["DCityCode"].ToString();
        //            tempobj.DAirportName = sdr["DAirportName"].ToString();
        //            tempobj.MinPrice = Convert.ToDecimal(sdr["MinPrice"].ToString());
        //            tempobj.MinWeight = Convert.ToDecimal(sdr["MinWeight"].ToString());
        //            tempobj.Normal = Convert.ToDecimal(sdr["Normal"].ToString());
        //            tempobj.plus45 = Convert.ToDecimal(sdr["plus45"].ToString());
        //            tempobj.plus100 = Convert.ToDecimal(sdr["plus100"].ToString());
        //            tempobj.plus250 = Convert.ToDecimal(sdr["plus250"].ToString());
        //            tempobj.plus300 = Convert.ToDecimal(sdr["plus300"].ToString());
        //            tempobj.plus500 = Convert.ToDecimal(sdr["plus500"].ToString());
        //            tempobj.plus1000 = Convert.ToDecimal(sdr["plus1000"].ToString());
        //            tempobj.FSCMin = Convert.ToDecimal(sdr["FSCMin"].ToString());
        //            tempobj.FSCKg = Convert.ToDecimal(sdr["FSCKg"].ToString());
        //            tempobj.WSCMin = Convert.ToDecimal(sdr["WSCMin"].ToString());
        //            tempobj.WSCKg = Convert.ToDecimal(sdr["WSCKg"].ToString());
        //            tempobj.XrayMin = Convert.ToDecimal(sdr["XrayMin"].ToString());
        //            tempobj.XrayKg = Convert.ToDecimal(sdr["XrayKg"].ToString());
        //            tempobj.MccMin = Convert.ToDecimal(sdr["MccMin"].ToString());
        //            tempobj.MccKg = Convert.ToDecimal(sdr["MccKg"].ToString());
        //            tempobj.CtcMin = Convert.ToDecimal(sdr["CtcMin"].ToString());
        //            tempobj.CtcKg = Convert.ToDecimal(sdr["CtcKg"].ToString());
        //            tempobj.Oth1 = Convert.ToDecimal(sdr["Oth1"].ToString());
        //            tempobj.Oth2 = Convert.ToDecimal(sdr["Oth2"].ToString());
        //            tempobj.Dgr = Convert.ToDecimal(sdr["Dgr"].ToString());
        //            tempobj.GrossWeight = Convert.ToDecimal(sdr["GrossWeight"].ToString());
        //            tempobj.TotalCost = Convert.ToDecimal(sdr["TotalCost"].ToString());
        //            tempobj.CreatedBy = sdr["CreatedBy"].ToString();
        //            tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
        //            //tempobj.DCountry = sdr["DCountry"].ToString();
        //            //tempobj.DCity = sdr["DCity"].ToString();
        //            //tempobj.OCountry = sdr["OCountry"].ToString();
        //            //tempobj.OCity = sdr["OCity"].ToString();
        //            tempobj.TariffMode = sdr["TariffMode"].ToString();
        //            tempobj.Airline = sdr["Airline"].ToString();
        //            objList.Add(tempobj);
        //        }
        //        sdr.Close();

        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }
        //    return objList;
        //}

        #endregion



        #region List Cargo Rates With Pagination

        public Paged_ACRF_CargoRatesModel ListCargoRatesByPagination(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_CargoRatesModel objPaged = new Paged_ACRF_CargoRatesModel();
            List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetSettingsByPage";


                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startRowIndex", startIndex);
                cmd.Parameters.AddWithValue("@pageSize", max);
                cmd.Parameters.AddWithValue("@search", search);
                cmd.Parameters.AddWithValue("@sort_col", sort_col);
                cmd.Parameters.AddWithValue("@sort_dir", sort_dir);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_CargoRatesModel tempobj = new ACRF_CargoRatesModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.Project_Id = Convert.ToInt32(sdr["Project_Id"].ToString());
                    tempobj.ProjectName = sdr["ProjectName"].ToString();
                    tempobj.TrackerType = sdr["TrackerType"].ToString();
                    tempobj.TrackerURL = sdr["TrackerURL"].ToString();
                    tempobj.assignInTracker = sdr["assignInTracker"].ToString();
                    //tempobj.OCityCode = sdr["OCityCode"].ToString();
                    //tempobj.OAirportName = sdr["OAirportName"].ToString();
                    //tempobj.DCountryCode = sdr["DCountryCode"].ToString();
                    //tempobj.DCityCode = sdr["DCityCode"].ToString();
                    //tempobj.DAirportName = sdr["DAirportName"].ToString();
                    //tempobj.MinPrice = Convert.ToDecimal(sdr["MinPrice"].ToString());
                    //tempobj.MinWeight = Convert.ToDecimal(sdr["MinWeight"].ToString());
                    //tempobj.Normal = Convert.ToDecimal(sdr["Normal"].ToString());
                    //tempobj.plus45 = Convert.ToDecimal(sdr["plus45"].ToString());
                    //tempobj.plus100 = Convert.ToDecimal(sdr["plus100"].ToString());
                    //tempobj.plus250 = Convert.ToDecimal(sdr["plus250"].ToString());
                    //tempobj.plus300 = Convert.ToDecimal(sdr["plus300"].ToString());
                    //tempobj.plus500 = Convert.ToDecimal(sdr["plus500"].ToString());
                    //tempobj.plus1000 = Convert.ToDecimal(sdr["plus1000"].ToString());
                    //tempobj.FSCMin = Convert.ToDecimal(sdr["FSCMin"].ToString());
                    //tempobj.FSCKg = Convert.ToDecimal(sdr["FSCKg"].ToString());
                    //tempobj.WSCMin = Convert.ToDecimal(sdr["WSCMin"].ToString());
                    //tempobj.WSCKg = Convert.ToDecimal(sdr["WSCKg"].ToString());
                    //tempobj.XrayMin = Convert.ToDecimal(sdr["XrayMin"].ToString());
                    //tempobj.XrayKg = Convert.ToDecimal(sdr["XrayKg"].ToString());
                    //tempobj.MccMin = Convert.ToDecimal(sdr["MccMin"].ToString());
                    //tempobj.MccKg = Convert.ToDecimal(sdr["MccKg"].ToString());
                    //tempobj.CtcMin = Convert.ToDecimal(sdr["CtcMin"].ToString());
                    //tempobj.CtcKg = Convert.ToDecimal(sdr["CtcKg"].ToString());
                    //tempobj.Oth1 = Convert.ToDecimal(sdr["Oth1"].ToString());
                    //tempobj.Oth2 = Convert.ToDecimal(sdr["Oth2"].ToString());
                    //tempobj.Dgr = Convert.ToDecimal(sdr["Dgr"].ToString());
                    //tempobj.GrossWeight = Convert.ToDecimal(sdr["GrossWeight"].ToString());
                    //tempobj.TotalCost = Convert.ToDecimal(sdr["TotalCost"].ToString());
                    //tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    //tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    ////tempobj.DCountry = sdr["DCountry"].ToString();
                    ////tempobj.DCity = sdr["DCity"].ToString();
                    ////tempobj.OCountry = sdr["OCountry"].ToString();
                    ////tempobj.OCity = sdr["OCity"].ToString();
                    //tempobj.TariffMode = sdr["TariffMode"].ToString();
                    //tempobj.Airline = sdr["Airline"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.data = objList;


                sqlstr = "select count(*) as cnt from settings where ProjectName like  @search ";
                cmd.Parameters.Clear();
                cmd.CommandText = sqlstr;
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@search", '%' + search + '%');
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objPaged.PageCount = Convert.ToInt32(sdr["cnt"].ToString());
                }


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objPaged;
        }

        #endregion




        #region Update Cargo Rates

        public string UpdateCargoRates(ACRF_CargoRatesModel objModel)
        {
            string result = "Error on Updating Project Settings!";
            try
            {
                //objModel = NullToBlank(objModel);
                objModel.ProjectName = getProjectName(objModel.Project_Id);
                
                    var connection = gConnection.Connection();
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = connection;
                if (objModel.TrackerPassword == null) {
                    objModel.TrackerPassword = "";
                }
                if (objModel.TrackerUserName == null)
                {
                    objModel.TrackerUserName = "";
                }
                try
                    {
                        string sqlstr = "";
                        sqlstr = "update settings set Project_Id=@projectId,ProjectName=@ProjectName,TrackerType=@TrackerType,TrackerURL=@TrackerURL,"
                        + " TrackerUserName=@TrackerUserName,TrackerPassword=@TrackerPassword,TrackerToken=@TrackerToken,assignInTracker=@assignInTracker,"
                        + " where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                    cmd.Parameters.AddWithValue("@projectId", objModel.Project_Id);
                        cmd.Parameters.AddWithValue("@ProjectName", objModel.ProjectName);
                        cmd.Parameters.AddWithValue("@TrackerType", objModel.TrackerType);
                        cmd.Parameters.AddWithValue("@TrackerURL", objModel.TrackerURL);
                        cmd.Parameters.AddWithValue("@TrackerToken", EnCryptDecrypt.Encryption.encrypt(objModel.TrackerToken));
                        cmd.Parameters.AddWithValue("@TrackerUserName", objModel.TrackerUserName);
                        cmd.Parameters.AddWithValue("@TrackerPassword", EnCryptDecrypt.Encryption.encrypt(objModel.TrackerPassword.Trim()));
                        cmd.Parameters.AddWithValue("@assignInTracker", objModel.assignInTracker);

                        //cmd.Parameters.AddWithValue("@AirlineId", objModel.AirlineId);
                        //cmd.Parameters.AddWithValue("@TariffModeId", objModel.TariffModeId);
                        //cmd.Parameters.AddWithValue("@OCountryCode", objModel.OCountryCode);
                        //cmd.Parameters.AddWithValue("@OCityCode", objModel.OCityCode);
                        //cmd.Parameters.AddWithValue("@OAirportName", objModel.OAirportName);
                        //cmd.Parameters.AddWithValue("@DCountryCode", objModel.DCountryCode);
                        //cmd.Parameters.AddWithValue("@DCityCode", objModel.DCityCode);
                        //cmd.Parameters.AddWithValue("@DAirportName", objModel.DAirportName);
                        //cmd.Parameters.AddWithValue("@MinPrice", objModel.MinPrice);
                        //cmd.Parameters.AddWithValue("@MinWeight", objModel.MinWeight);
                        //cmd.Parameters.AddWithValue("@Normal", objModel.Normal);
                        //cmd.Parameters.AddWithValue("@plus45", objModel.plus45);
                        //cmd.Parameters.AddWithValue("@plus100", objModel.plus100);
                        //cmd.Parameters.AddWithValue("@plus250", objModel.plus250);
                        //cmd.Parameters.AddWithValue("@plus300", objModel.plus300);
                        //cmd.Parameters.AddWithValue("@plus500", objModel.plus500);
                        //cmd.Parameters.AddWithValue("@plus1000", objModel.plus1000);
                        //cmd.Parameters.AddWithValue("@FSCMin", objModel.FSCMin);
                        //cmd.Parameters.AddWithValue("@FSCKg", objModel.FSCKg);
                        //cmd.Parameters.AddWithValue("@WSCMin", objModel.WSCMin);
                        //cmd.Parameters.AddWithValue("@WSCKg", objModel.WSCKg);
                        //cmd.Parameters.AddWithValue("@XrayMin", objModel.XrayMin);
                        //cmd.Parameters.AddWithValue("@XrayKg", objModel.XrayKg);
                        //cmd.Parameters.AddWithValue("@MccMin", objModel.MccMin);
                        //cmd.Parameters.AddWithValue("@MccKg", objModel.MccKg);
                        //cmd.Parameters.AddWithValue("@CtcMin", objModel.CtcMin);
                        //cmd.Parameters.AddWithValue("@CtcKg", objModel.CtcKg);
                        //cmd.Parameters.AddWithValue("@Oth1", objModel.Oth1);
                        //cmd.Parameters.AddWithValue("@Oth2", objModel.Oth2);
                        //cmd.Parameters.AddWithValue("@Dgr", objModel.Dgr);
                        //cmd.Parameters.AddWithValue("@GrossWeight", objModel.GrossWeight);
                        //cmd.Parameters.AddWithValue("@TotalCost", objModel.TotalCost);
                        //cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        //cmd.Parameters.AddWithValue("@UpdatedBy", objModel.CreatedBy);
                        //cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Project settings Updated Successfully!";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                        Global.ErrorHandlerClass.LogError(ex);
                        result = ex.Message;
                    }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion

        
        #region Delete Cargo Rates

        public string DeleteCargoRates(int Id)
        {
            string result = "Error on Deleting Project Settings!";
            try
            {
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = connection;
                try
                {
                    string sqlstr = "";
                    //sqlstr = "insert into ACRF_CargoRates_Log(Id,AirlineId,TariffModeId,OCountryCode,OCityCode,OAirportName,DCountryCode,DCityCode,"
                    //    + " DAirportName,MinPrice,MinWeight,Normal,  plus45,plus100,plus250,plus300,plus500,plus1000,FSCMin,FSCKg,WSCMin,WSCKg,"
                    //    + " XrayMin,XrayKg,MccMin,MccKg,CtcMin, CtcKg,Oth1,Oth2,Dgr,GrossWeight,TotalCost,CreatedBy,CreatedOn) "
                    //    + " select Id,AirlineId,TariffModeId,OCountryCode,OCityCode,OAirportName,DCountryCode,DCityCode,DAirportName,MinPrice,MinWeight,Normal, "
                    //    + " plus45,plus100,plus250,plus300,plus500,plus1000,FSCMin,FSCKg,WSCMin,WSCKg,XrayMin,XrayKg,MccMin,MccKg,CtcMin, "
                    //    + " CtcKg,Oth1,Oth2,Dgr,GrossWeight,TotalCost,@CreatedBy,@CreatedOn from ACRF_CargoRates where Id=@Id";
                    //cmd.CommandText = sqlstr;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Id", Id);
                    //cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    //cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    //cmd.ExecuteNonQuery();
                    
                    sqlstr = "delete from settings where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                    result = "Project settings Deleted Successfully!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    connection.Close();
                    Global.ErrorHandlerClass.LogError(ex);
                    result = ex.Message;
                }

            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        //private ACRF_CargoRatesModel NullToBlank(ACRF_CargoRatesModel objModel)
        //{
        //    if (objModel.AirlineId == null)
        //    {
        //        objModel.AirlineId = 0;
        //    }
        //    if (objModel.OAirportName == null)
        //    {
        //        objModel.OAirportName = "";
        //    }
        //    if (objModel.OCityCode == null)
        //    {
        //        objModel.OCityCode = "";
        //    }
        //    if (objModel.OCountryCode == null)
        //    {
        //        objModel.OCountryCode = "";
        //    }

        //    if (objModel.DAirportName == null)
        //    {
        //        objModel.DAirportName = "";
        //    }
        //    if (objModel.DCityCode == null)
        //    {
        //        objModel.DCityCode = "";
        //    }
        //    if (objModel.DCountryCode == null)
        //    {
        //        objModel.DCountryCode = "";
        //    }

        //    if (objModel.CtcKg == null)
        //    {
        //        objModel.CtcKg = 0;
        //    }
        //    if (objModel.CtcMin == null)
        //    {
        //        objModel.CtcMin = 0;
        //    }
        //    if (objModel.Dgr == null)
        //    {
        //        objModel.Dgr = 0;
        //    }
        //    if (objModel.FSCKg == null)
        //    {
        //        objModel.FSCKg = 0;
        //    }
        //    if (objModel.FSCMin == null)
        //    {
        //        objModel.FSCMin = 0;
        //    }
        //    if (objModel.GrossWeight == null)
        //    {
        //        objModel.GrossWeight = 0;
        //    }
        //    if (objModel.MccKg == null)
        //    {
        //        objModel.MccKg = 0;
        //    }
        //    if (objModel.MccMin == null)
        //    {
        //        objModel.MccMin = 0;
        //    }
        //    if (objModel.MinPrice == null)
        //    {
        //        objModel.MinPrice = 0;
        //    }
        //    if (objModel.MinWeight == null)
        //    {
        //        objModel.MinWeight = 0;
        //    }
        //    if (objModel.Normal == null)
        //    {
        //        objModel.Normal = 0;
        //    }
        //    if (objModel.Oth1 == null)
        //    {
        //        objModel.Oth1 = 0;
        //    }
        //    if (objModel.Oth2 == null)
        //    {
        //        objModel.Oth2 = 0;
        //    }
        //    if (objModel.plus100 == null)
        //    {
        //        objModel.plus100 = 0;
        //    }
        //    if (objModel.plus1000 == null)
        //    {
        //        objModel.plus1000 = 0;
        //    }
        //    if (objModel.plus250 == null)
        //    {
        //        objModel.plus250 = 0;
        //    }
        //    if (objModel.plus300 == null)
        //    {
        //        objModel.plus300 = 0;
        //    }
        //    if (objModel.plus45 == null)
        //    {
        //        objModel.plus45 = 0;
        //    }
        //    if (objModel.plus500 == null)
        //    {
        //        objModel.plus500 = 0;
        //    }
        //    if (objModel.TariffModeId == null)
        //    {
        //        objModel.TariffModeId = 0;
        //    }
        //    if (objModel.TotalCost == null)
        //    {
        //        objModel.TotalCost = 0;
        //    }
        //    if (objModel.WSCKg == null)
        //    {
        //        objModel.WSCKg = 0;
        //    }
        //    if (objModel.WSCMin == null)
        //    {
        //        objModel.WSCMin = 0;
        //    }
        //    if (objModel.XrayKg == null)
        //    {
        //        objModel.XrayKg = 0;
        //    }
        //    if (objModel.XrayMin == null)
        //    {
        //        objModel.XrayMin = 0;
        //    }

        //    return objModel;
        //}



        #region Check If Cargo Rates Already Exists

        //public string CheckIfCargoRatesExists(ACRF_CargoRatesModel objModel)
        //{
        //    string result = "";
        //    try
        //    {
        //        string sqlstr = "Select * from ACRF_CargoRates Where ISNULL(AirlineId,0)=@AirlineId and isnull(TariffModeId,0)=@TariffModeId "
        //        + " and isnull(OCountryCode,'') =@OCountryCode and isnull(OCityCode,'')=@OCityCode and isnull(DCountryCode,'') =@DCountryCode "
        //        + " and isnull(DCityCode,'')=@DCityCode and Isnull(Id,0)!=@Id ";

        //        var connection = gConnection.Connection();
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(sqlstr, connection);
        //        cmd.Parameters.AddWithValue("@AirlineId", objModel.AirlineId);
        //        cmd.Parameters.AddWithValue("@TariffModeId", objModel.TariffModeId);
        //        cmd.Parameters.AddWithValue("@OCountryCode", objModel.OCountryCode);
        //        cmd.Parameters.AddWithValue("@OCityCode", objModel.OCityCode);
        //        cmd.Parameters.AddWithValue("@DCountryCode", objModel.DCountryCode);
        //        cmd.Parameters.AddWithValue("@DCityCode", objModel.DCityCode);
        //        cmd.Parameters.AddWithValue("@Id", objModel.Id);
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            result = "Cargo Rates already exists!";
        //        }
        //        sdr.Close();

        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }
        //    return result;
        //}

        #endregion
        public string getProjectName(int projectid)
        {
            string projectName = "";
            try
            {
                
                string projectsql = "select ID as ProjectID, isnull(Project,'') as ProjectName from tbl_Projects where ID=" +projectid;

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd1 = new SqlCommand(projectsql, connection);
                cmd1.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd1.ExecuteReader();
                
                while (sdr.Read())
                {
                    projectName = sdr["ProjectName"].ToString();
                    
                }
                sdr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return projectName;
        }



        #region ListCargoRatesFromODTGA

        //public List<ACRF_CargoRatesModel> ListCargoRatesFromODTGA(string Origin, string Destination, int TariffMode, int Weight, int Airlines)
        //{
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    try
        //    {
        //        string sqlstr = "select  Id, isnull(AirlineId,0) as AirlineId, isnull(TariffModeId,0) as TariffModeId,isnull(OCountryCode,'') as OCountryCode, "
        //        + " isnull(OCityCode,'') as OCityCode, isnull(OAirportName,'') as OAirportName,isnull(DCountryCode,'') as DCountryCode, "
        //        + " isnull(DCityCode,'') as DCityCode, isnull(DAirportName,'') as DAirportName, isnull(MinPrice,0) as MinPrice, "
        //        + " isnull(MinWeight,0) as MinWeight, isnull(Normal,0) as Normal, isnull(plus45,0) as plus45, isnull(plus100,0) as plus100, "
        //        + " isnull(plus250,0) as plus250, isnull(plus300,0) as plus300, isnull(plus500,0) as plus500, isnull(plus1000,0) as plus1000, "
        //        + " isnull(FSCMin,0) as FSCMin, isnull(FSCKg,0) as FSCKg, isnull(WSCMin,0) as WSCMin, isnull(WSCKg,0) as WSCKg, "
        //        + " isnull(XrayMin,0) as XrayMin, isnull(XrayKg,0) as XrayKg, isnull(MccMin,0) as MccMin, isnull(MccKg,0) as MccKg, "
        //        + " isnull(CtcMin,0) as CtcMin, isnull(CtcKg,0) as CtcKg, isnull(Oth1,0) as Oth1, isnull(Oth2,0) as Oth2, isnull(Dgr,0) as Dgr, "
        //        + " isnull(GrossWeight,0) as GrossWeight, isnull(TotalCost,0) as TotalCost,CreatedBy,CreatedOn, "
        //        + " isnull((Select top 1 CountryName From ACRF_DestinationMaster D where D.CountryCode=CR.DCountryCode),'') as DCountry, "
        //        + " isnull((Select top 1 CityName From ACRF_DestinationMaster D where D.CityCode=CR.DCityCode),'') as DCity, "
        //        + " isnull((Select top 1 CountryName From ACRF_DestinationMaster D where D.CountryCode=CR.OCountryCode),'') as OCountry, "
        //        + " isnull((Select top 1 CityName From ACRF_DestinationMaster D where D.CityCode=CR.OCityCode),'') as OCity, "
        //        + " isnull((Select top 1 TariffMode From ACRF_TariffMode D where D.Id=CR.TariffModeId),'') as TariffMode, "
        //        + " isnull((Select top 1 AirlineName From ACRF_Airlines D where D.Id=CR.AirlineId),'') as Airline, "
        //        + " isnull((Select top 1 AirlinePhoto From ACRF_Airlines D where D.Id=CR.AirlineId),'') as AirlinePhoto "
        //        + " from ACRF_CargoRates CR where isnull(OCityCode,'')=@Origin and isnull(DCityCode,'')=@Destination  and "
        //        + " isnull(TariffModeId,0)=@TariffModeId ";

        //        if(Airlines>0)
        //        {
        //            sqlstr = sqlstr + " and isnull(AirlineId,0)=@AirlineId ";
        //        }

        //        var connection = gConnection.Connection();
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(sqlstr, connection);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.Parameters.AddWithValue("@Origin",Origin);
        //        cmd.Parameters.AddWithValue("@Destination",Destination);
        //        if (Airlines > 0)
        //        {
        //            cmd.Parameters.AddWithValue("@AirlineId", Airlines);
        //        }
        //        cmd.Parameters.AddWithValue("@TariffModeId",TariffMode);
        //        cmd.Parameters.AddWithValue("@GrossWeight", Weight);
        //        SqlDataReader sdr = cmd.ExecuteReader();

        //        while (sdr.Read())
        //        {
        //            ACRF_CargoRatesModel tempobj = new ACRF_CargoRatesModel();
        //            tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
        //            tempobj.AirlineId = Convert.ToInt32(sdr["AirlineId"].ToString());
        //            tempobj.TariffModeId = Convert.ToInt32(sdr["TariffModeId"].ToString());
        //            tempobj.OCountryCode = sdr["OCountryCode"].ToString();
        //            tempobj.OCityCode = sdr["OCityCode"].ToString();
        //            tempobj.OAirportName = sdr["OAirportName"].ToString();
        //            tempobj.DCountryCode = sdr["DCountryCode"].ToString();
        //            tempobj.DCityCode = sdr["DCityCode"].ToString();
        //            tempobj.DAirportName = sdr["DAirportName"].ToString();
        //            tempobj.MinPrice = Convert.ToDecimal(sdr["MinPrice"].ToString());
        //            tempobj.MinWeight = Convert.ToDecimal(sdr["MinWeight"].ToString());
        //            tempobj.Normal = Convert.ToDecimal(sdr["Normal"].ToString());
        //            tempobj.plus45 = Convert.ToDecimal(sdr["plus45"].ToString());
        //            tempobj.plus100 = Convert.ToDecimal(sdr["plus100"].ToString());
        //            tempobj.plus250 = Convert.ToDecimal(sdr["plus250"].ToString());
        //            tempobj.plus300 = Convert.ToDecimal(sdr["plus300"].ToString());
        //            tempobj.plus500 = Convert.ToDecimal(sdr["plus500"].ToString());
        //            tempobj.plus1000 = Convert.ToDecimal(sdr["plus1000"].ToString());
        //            tempobj.FSCMin = Convert.ToDecimal(sdr["FSCMin"].ToString());
        //            tempobj.FSCKg = Convert.ToDecimal(sdr["FSCKg"].ToString());
        //            tempobj.WSCMin = Convert.ToDecimal(sdr["WSCMin"].ToString());
        //            tempobj.WSCKg = Convert.ToDecimal(sdr["WSCKg"].ToString());
        //            tempobj.XrayMin = Convert.ToDecimal(sdr["XrayMin"].ToString());
        //            tempobj.XrayKg = Convert.ToDecimal(sdr["XrayKg"].ToString());
        //            tempobj.MccMin = Convert.ToDecimal(sdr["MccMin"].ToString());
        //            tempobj.MccKg = Convert.ToDecimal(sdr["MccKg"].ToString());
        //            tempobj.CtcMin = Convert.ToDecimal(sdr["CtcMin"].ToString());
        //            tempobj.CtcKg = Convert.ToDecimal(sdr["CtcKg"].ToString());
        //            tempobj.Oth1 = Convert.ToDecimal(sdr["Oth1"].ToString());
        //            tempobj.Oth2 = Convert.ToDecimal(sdr["Oth2"].ToString());
        //            tempobj.Dgr = Convert.ToDecimal(sdr["Dgr"].ToString());
        //            tempobj.GrossWeight = Convert.ToDecimal(sdr["GrossWeight"].ToString());
        //            tempobj.TotalCost = Convert.ToDecimal(sdr["TotalCost"].ToString());
        //            tempobj.CreatedBy = sdr["CreatedBy"].ToString();
        //            tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());

        //            tempobj.DCountry = sdr["DCountry"].ToString();
        //            tempobj.DCity = sdr["DCity"].ToString();
        //            tempobj.OCountry = sdr["OCountry"].ToString();
        //            tempobj.OCity = sdr["OCity"].ToString();
        //            tempobj.TariffMode = sdr["TariffMode"].ToString();
        //            tempobj.Airline = sdr["Airline"].ToString();

        //            if (sdr["AirlinePhoto"].ToString() == "")
        //            {
        //                tempobj.AirlinePhoto = "";
        //                tempobj.AirlineDemoPhoto = tempobj.Airline[0].ToString();
        //            }
        //            else
        //            {
        //                tempobj.AirlinePhoto = GlobalFunction.GetAPIUrl() + sdr["AirlinePhoto"].ToString();
        //            }

        //            objList.Add(tempobj);
        //        }
        //        sdr.Close();

        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }
        //    return objList;
        //}

        #endregion



        #region ListCargoRatesFromODTGA

        //public List<ACRF_CargoRatesModel> ListCargoRatesFromODTGA(CargoRatesForMulitSelectModel objModel)
        //{
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    try
        //    {
        //        string Destination = "";
        //        string Airlines = "";

        //        Airlines = "";
        //        if (objModel.objAirlinesList != null)
        //        {
        //            for (int i = 0; i < objModel.objAirlinesList.Length; i++)
        //            {
        //                //if (objModel.objAirlinesList[i].IsSelect == true)
        //                //{
        //                Airlines = Airlines + objModel.objAirlinesList[i];
        //                Airlines = Airlines + ",";
        //                //}
        //            }
        //            Airlines = Airlines.TrimEnd(',');
        //        }

        //        Destination = "";
        //        if (objModel.objDestList != null)
        //        {
        //            if (objModel.objDestList.Length > 0)
        //            {
        //                for (int i = 0; i < objModel.objDestList.Length; i++)
        //                {
        //                    Destination = Destination + "'";
        //                    //if (objModel.objDestList[i].IsSelect == true)
        //                    //{
        //                        Destination = Destination + objModel.objDestList[i];
        //                        Destination = Destination + "'";
        //                    //}
        //                    //Destination = Destination + "'";
        //                    //if (i + 1 < objModel.objDestList.Count)
        //                    //{
        //                        Destination = Destination + ",";
        //                    //}
        //                }
        //                Destination = Destination.TrimEnd(',');
        //            }
        //        }
        //        else
        //        {
        //            if (objModel.destination != "")
        //            {
        //                Destination = Destination + "'";
        //                Destination = Destination + objModel.destination;
        //                Destination = Destination + "'";
        //            }
        //        }


        //        string sqlstr = "select  Id, isnull(AirlineId,0) as AirlineId, isnull(TariffModeId,0) as TariffModeId,isnull(OCountryCode,'') as OCountryCode, "
        //        + " isnull(OCityCode,'') as OCityCode, isnull(OAirportName,'') as OAirportName,isnull(DCountryCode,'') as DCountryCode, "
        //        + " isnull(DCityCode,'') as DCityCode, isnull(DAirportName,'') as DAirportName, isnull(MinPrice,0) as MinPrice, "
        //        + " isnull(MinWeight,0) as MinWeight, isnull(Normal,0) as Normal, isnull(plus45,0) as plus45, isnull(plus100,0) as plus100, "
        //        + " isnull(plus250,0) as plus250, isnull(plus300,0) as plus300, isnull(plus500,0) as plus500, isnull(plus1000,0) as plus1000, "
        //        + " isnull(FSCMin,0) as FSCMin, isnull(FSCKg,0) as FSCKg, isnull(WSCMin,0) as WSCMin, isnull(WSCKg,0) as WSCKg, "
        //        + " isnull(XrayMin,0) as XrayMin, isnull(XrayKg,0) as XrayKg, isnull(MccMin,0) as MccMin, isnull(MccKg,0) as MccKg, "
        //        + " isnull(CtcMin,0) as CtcMin, isnull(CtcKg,0) as CtcKg, isnull(Oth1,0) as Oth1, isnull(Oth2,0) as Oth2, isnull(Dgr,0) as Dgr, "
        //        + " isnull(GrossWeight,0) as GrossWeight, isnull(TotalCost,0) as TotalCost,CreatedBy,CreatedOn, "
        //        + " isnull((Select top 1 CountryName From ACRF_DestinationMaster D where D.CountryCode=CR.DCountryCode),'') as DCountry, "
        //        + " isnull((Select top 1 CityName From ACRF_DestinationMaster D where D.CityCode=CR.DCityCode),'') as DCity, "
        //        + " isnull((Select top 1 CountryName From ACRF_DestinationMaster D where D.CountryCode=CR.OCountryCode),'') as OCountry, "
        //        + " isnull((Select top 1 CityName From ACRF_DestinationMaster D where D.CityCode=CR.OCityCode),'') as OCity, "
        //        + " isnull((Select top 1 TariffMode From ACRF_TariffMode D where D.Id=CR.TariffModeId),'') as TariffMode, "
        //        + " isnull((Select top 1 AirlineName From ACRF_Airlines D where D.Id=CR.AirlineId),'') as Airline, "
        //        + " isnull((Select top 1 AirlinePhoto From ACRF_Airlines D where D.Id=CR.AirlineId),'') as AirlinePhoto "
        //        + " from ACRF_CargoRates CR where isnull(OCityCode,'')=@Origin  and "
        //        + " isnull(TariffModeId,0)=@TariffModeId ";

        //        if(Destination != "")
        //        {
        //            sqlstr = sqlstr + " and isnull(DCityCode,'') in (" + Destination + ") ";
        //        }

        //        if (Airlines != "")
        //        {
        //            sqlstr = sqlstr + " and isnull(AirlineId,0) in (" + Airlines + ")";
        //        }

        //        var connection = gConnection.Connection();
        //        connection.Open();
        //        SqlCommand cmd = new SqlCommand(sqlstr, connection);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.Parameters.AddWithValue("@Origin", objModel.Origin);

        //        //Destination = "";
        //        //for (int i=0; i< objModel.objDestList.Count; i++)
        //        //{
        //        //    Destination = Destination + "'";
        //        //    if (objModel.objDestList[i].IsSelect == true)
        //        //    {                        
        //        //        Destination = Destination + objModel.objDestList[i].CityCode;                        
        //        //    }
        //        //    Destination = Destination + "'";
        //        //    if (i + 1 < objModel.objDestList.Count)
        //        //    {
        //        //        Destination = Destination + ",";
        //        //    }
        //        //}
        //        //cmd.Parameters.AddWithValue("@Destination", Destination);



        //        //Airlines = "";
        //        //for (int i = 0; i < objModel.objAirlinesList.Count; i++)
        //        //{
        //        //    if (objModel.objAirlinesList[i].IsSelect == true)
        //        //    {
        //        //        Airlines = Airlines + objModel.objAirlinesList[i].Id;
        //        //        Airlines = Airlines + ",";
        //        //    }
        //        //}
        //        //Airlines = Airlines.TrimEnd(',');
        //        //cmd.Parameters.AddWithValue("@AirlineId", Airlines);
                
        //        cmd.Parameters.AddWithValue("@TariffModeId", objModel.TariffMode);
        //        //cmd.Parameters.AddWithValue("@GrossWeight", objModel.GWeight);
        //        SqlDataReader sdr = cmd.ExecuteReader();

        //        while (sdr.Read())
        //        {
        //            ACRF_CargoRatesModel tempobj = new ACRF_CargoRatesModel();
        //            tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
        //            tempobj.AirlineId = Convert.ToInt32(sdr["AirlineId"].ToString());
        //            tempobj.TariffModeId = Convert.ToInt32(sdr["TariffModeId"].ToString());
        //            tempobj.OCountryCode = sdr["OCountryCode"].ToString();
        //            tempobj.OCityCode = sdr["OCityCode"].ToString();
        //            tempobj.OAirportName = sdr["OAirportName"].ToString();
        //            tempobj.DCountryCode = sdr["DCountryCode"].ToString();
        //            tempobj.DCityCode = sdr["DCityCode"].ToString();
        //            tempobj.DAirportName = sdr["DAirportName"].ToString();
        //            tempobj.MinPrice = Convert.ToDecimal(sdr["MinPrice"].ToString());
        //            tempobj.MinWeight = Convert.ToDecimal(sdr["MinWeight"].ToString());
        //            tempobj.Normal = Convert.ToDecimal(sdr["Normal"].ToString());
        //            tempobj.plus45 = Convert.ToDecimal(sdr["plus45"].ToString());
        //            tempobj.plus100 = Convert.ToDecimal(sdr["plus100"].ToString());
        //            tempobj.plus250 = Convert.ToDecimal(sdr["plus250"].ToString());
        //            tempobj.plus300 = Convert.ToDecimal(sdr["plus300"].ToString());
        //            tempobj.plus500 = Convert.ToDecimal(sdr["plus500"].ToString());
        //            tempobj.plus1000 = Convert.ToDecimal(sdr["plus1000"].ToString());
        //            tempobj.FSCMin = Convert.ToDecimal(sdr["FSCMin"].ToString());
        //            tempobj.FSCKg = Convert.ToDecimal(sdr["FSCKg"].ToString());
        //            tempobj.WSCMin = Convert.ToDecimal(sdr["WSCMin"].ToString());
        //            tempobj.WSCKg = Convert.ToDecimal(sdr["WSCKg"].ToString());
        //            tempobj.XrayMin = Convert.ToDecimal(sdr["XrayMin"].ToString());
        //            tempobj.XrayKg = Convert.ToDecimal(sdr["XrayKg"].ToString());
        //            tempobj.MccMin = Convert.ToDecimal(sdr["MccMin"].ToString());
        //            tempobj.MccKg = Convert.ToDecimal(sdr["MccKg"].ToString());
        //            tempobj.CtcMin = Convert.ToDecimal(sdr["CtcMin"].ToString());
        //            tempobj.CtcKg = Convert.ToDecimal(sdr["CtcKg"].ToString());
        //            tempobj.Oth1 = Convert.ToDecimal(sdr["Oth1"].ToString());
        //            tempobj.Oth2 = Convert.ToDecimal(sdr["Oth2"].ToString());
        //            tempobj.Dgr = Convert.ToDecimal(sdr["Dgr"].ToString());
        //            tempobj.GrossWeight = Convert.ToDecimal(sdr["GrossWeight"].ToString());
        //            tempobj.TotalCost = Convert.ToDecimal(sdr["TotalCost"].ToString());
        //            tempobj.CreatedBy = sdr["CreatedBy"].ToString();
        //            tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());

        //            tempobj.DCountry = sdr["DCountry"].ToString();
        //            tempobj.DCity = sdr["DCity"].ToString();
        //            tempobj.OCountry = sdr["OCountry"].ToString();
        //            tempobj.OCity = sdr["OCity"].ToString();
        //            tempobj.TariffMode = sdr["TariffMode"].ToString();
        //            tempobj.Airline = sdr["Airline"].ToString();

        //            if (sdr["AirlinePhoto"].ToString() == "")
        //            {
        //                tempobj.AirlinePhoto = "";
        //                tempobj.AirlineDemoPhoto = tempobj.Airline[0].ToString();
        //            }
        //            else
        //            {
        //                tempobj.AirlinePhoto = GlobalFunction.GetAPIUrl() + sdr["AirlinePhoto"].ToString();
        //            }

        //            objList.Add(tempobj);
        //        }
        //        sdr.Close();

        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }
        //    return objList;
        //}

        #endregion


    }
}