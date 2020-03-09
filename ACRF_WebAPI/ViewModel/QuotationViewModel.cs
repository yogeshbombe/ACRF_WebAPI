using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class QuotationViewModel
    {

        #region Create Quotation

        public string CreateQuotation(ACRF_QuotationModel objModel)
        {
            string result = "Error on Saving Quotation!";
            try
            {
                objModel = NullToBlank(objModel);

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
                    sqlstr = "insert into ACRF_Quotation(VendorId,FromMail,ClientMail,CC,BCC,MailSubject,QuotationStatus,CreatedOn,CreatedBy)";
                    sqlstr = sqlstr + " values (@VendorId,@FromMail,@ClientMail,@CC,@BCC,@MailSubject,@QuotationStatus,@CreatedOn,@CreatedBy); SELECT SCOPE_IDENTITY();";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                    cmd.Parameters.AddWithValue("@FromMail", objModel.FromMail);
                    cmd.Parameters.AddWithValue("@ClientMail", objModel.ClientMail);
                    cmd.Parameters.AddWithValue("@CC", objModel.CC);
                    cmd.Parameters.AddWithValue("@BCC", objModel.BCC);
                    cmd.Parameters.AddWithValue("@MailSubject", objModel.MailSubject);
                    cmd.Parameters.AddWithValue("@QuotationStatus", objModel.QuotationStatus);

                    cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    var id = cmd.ExecuteScalar();
                    


                    foreach (var data in objModel.ACRF_QuotationDetailsModelList)
                    {
                        data.QuotationId = Convert.ToInt32(id.ToString());
                        sqlstr = "insert into ACRF_QuotationDetails(QuotationId,AirlineId,TariffModeId,OCountryCode,OCityCode,OAirportName,DCountryCode,"
                        + " DCityCode,DAirportName,Slab1,Rate,Freight,FSC,WSC,Xray,Mcc,Ctc,AMS,TotalCost,IsRate1,IsRate2,IsRate3,Rate1, "
                        + " Rate2,Rate3,DisplayRateName1,DisplayRateName2,DisplayRateName3,CreatedBy,CreatedOn) values "
                        + " (@QuotationId,@AirlineId,@TariffModeId,@OCountryCode,@OCityCode,@OAirportName,@DCountryCode,"
                        + " @DCityCode,@DAirportName,@Slab1,@Rate,@Freight,@FSC,@WSC,@Xray,@Mcc,@Ctc,@AMS,@TotalCost,@IsRate1,@IsRate2,@IsRate3,@Rate1, "
                        + " @Rate2,@Rate3,@DisplayRateName1,@DisplayRateName2,@DisplayRateName3,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@QuotationId", data.QuotationId);
                        cmd.Parameters.AddWithValue("@AirlineId", data.AirlineId);
                        cmd.Parameters.AddWithValue("@TariffModeId", data.TariffModeId);
                        cmd.Parameters.AddWithValue("@OCountryCode", data.OCountryCode);
                        cmd.Parameters.AddWithValue("@OCityCode", data.OCityCode);
                        cmd.Parameters.AddWithValue("@OAirportName", data.OAirportName);
                        cmd.Parameters.AddWithValue("@DCountryCode", data.DCountryCode);
                        cmd.Parameters.AddWithValue("@DCityCode", data.DCityCode);
                        cmd.Parameters.AddWithValue("@DAirportName", data.DAirportName);
                        cmd.Parameters.AddWithValue("@Slab1", data.Slab1);
                        cmd.Parameters.AddWithValue("@Rate", data.Rate);
                        cmd.Parameters.AddWithValue("@Freight", data.Freight);
                        cmd.Parameters.AddWithValue("@FSC", data.FSC);
                        cmd.Parameters.AddWithValue("@WSC", data.WSC);
                        cmd.Parameters.AddWithValue("@Xray", data.Xray);
                        cmd.Parameters.AddWithValue("@Mcc", data.Mcc);
                        cmd.Parameters.AddWithValue("@Ctc", data.Ctc);
                        cmd.Parameters.AddWithValue("@AMS", data.AMS);
                        cmd.Parameters.AddWithValue("@TotalCost", data.TotalCost);
                        cmd.Parameters.AddWithValue("@IsRate1", data.IsRate1);
                        cmd.Parameters.AddWithValue("@IsRate2", data.IsRate2);
                        cmd.Parameters.AddWithValue("@IsRate3", data.IsRate3);
                        cmd.Parameters.AddWithValue("@Rate1", data.Rate1);
                        cmd.Parameters.AddWithValue("@Rate2", data.Rate2);
                        cmd.Parameters.AddWithValue("@Rate3", data.Rate3);
                        cmd.Parameters.AddWithValue("@DisplayRateName1", data.DisplayRateName1);
                        cmd.Parameters.AddWithValue("@DisplayRateName2", data.DisplayRateName2);
                        cmd.Parameters.AddWithValue("@DisplayRateName3", data.DisplayRateName3);

                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();

                    }



                    transaction.Commit();
                    connection.Close();
                    result = "Quotation Added Successfully!";
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

        

        #region List Quotation With Pagination

        public Paged_ACRF_QuotationDetailsModel ListQuotationByPagination(int max, int page, string search, int VendorId, string sort_col, string sort_dir)
        {
            Paged_ACRF_QuotationDetailsModel objPaged = new Paged_ACRF_QuotationDetailsModel();
            List<ACRF_QuotationModel> objList = new List<ACRF_QuotationModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetQuotationByPage";
                
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startRowIndex", startIndex);
                cmd.Parameters.AddWithValue("@pageSize", max);
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@search", search);
                cmd.Parameters.AddWithValue("@sort_col", sort_col);
                cmd.Parameters.AddWithValue("@sort_dir", sort_dir);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_QuotationModel tempobj = new ACRF_QuotationModel();
                    tempobj.QuotationId = Convert.ToInt32(sdr["QuotationId"].ToString());
                    tempobj.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    tempobj.FromMail = sdr["FromMail"].ToString();
                    tempobj.ClientMail = sdr["ClientMail"].ToString();
                    tempobj.CC = sdr["CC"].ToString();
                    tempobj.BCC = sdr["BCC"].ToString();
                    tempobj.MailSubject = sdr["MailSubject"].ToString();
                    tempobj.QuotationStatus = sdr["QuotationStatus"].ToString();

                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());

                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_QuotationDetailsModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_Quotation where VendorId = @VendorId and ClientMail like  @search ";
                cmd.Parameters.Clear();
                cmd.CommandText = sqlstr;
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@search", '%' + search + '%');
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
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




        #region Update Quotation Status

        public string UpdateQuotationStatus(QuotationStatusModel objModel)
        {
            string result = "Error on Updating Quotation Status!";
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
                    sqlstr = "update ACRF_Quotation set QuotationStatus=@QuotationStatus,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy";
                    sqlstr = sqlstr + "  where QuotationId=@QuotationId ";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@QuotationStatus", objModel.QuotationStatus);
                    cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                    cmd.Parameters.AddWithValue("@QuotationId", objModel.QuotationId);
                    cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();
                    result = "Quotation Status Updated Successfully!";
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




        #region Get One Quotation By QuotationId

        public ACRF_QuotationModel GetOneQuotation(int QuotationId)
        {
            ACRF_QuotationModel objModel = new ACRF_QuotationModel();
            List<ACRF_QuotationDetailsModel> objList = new List<ACRF_QuotationDetailsModel>();
            try
            {
                string sqlstr = "select QuotationId,VendorId,FromMail,ClientMail,Isnull(CC,'') as CC, Isnull(BCC,'') as BCC,"
                + " MailSubject,QuotationStatus,CreatedOn,CreatedBy,Isnull(UpdatedOn,'') as UpdatedOn, "
                + " Isnull(UpdatedBy,'') as UpdatedBy from ACRF_Quotation where QuotationId=@QuotationId";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@QuotationId", QuotationId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    objModel.QuotationId = Convert.ToInt32(sdr["QuotationId"].ToString());
                    objModel.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    objModel.FromMail = sdr["FromMail"].ToString();
                    objModel.ClientMail = sdr["ClientMail"].ToString();
                    objModel.CC = sdr["CC"].ToString();
                    objModel.BCC = sdr["BCC"].ToString();
                    objModel.MailSubject = sdr["MailSubject"].ToString();
                    objModel.QuotationStatus = sdr["QuotationStatus"].ToString();
                    objModel.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objModel.CreatedBy = sdr["CreatedBy"].ToString();
                    objModel.UpdatedOn = Convert.ToDateTime(sdr["UpdatedOn"].ToString());
                    objModel.UpdatedBy = sdr["UpdatedBy"].ToString();
                }
                sdr.Close();


                sqlstr = "select Id,QuotationId,AirlineId,TariffModeId,OCountryCode,Isnull(OCityCode,'') as OCityCode, Isnull(OAirportName,'') as OAirportName,"
                + " isnull(DCountryCode,'') as DCountryCode, isnull(DCityCode,'') as DCityCode, isnull(DAirportName,'') as DAirportName, "
                + " isnull(Slab1,0) as Slab1, isnull(Rate,0) as Rate, isnull(Freight,0) as Freight, isnull(FSC,0) as FSC, "
                + " isnull(WSC,0) as WSC, isnull(Xray,0) as Xray, isnull(Mcc,0) as Mcc, isnull(Ctc,0) as Ctc, isnull(AMS,0) as AMS, "
                + " isnull(TotalCost,0) as TotalCost, isnull(IsRate1,'') as IsRate1, isnull(IsRate2,'') as IsRate2, isnull(IsRate3,'') as IsRate3, "
                + " isnull(Rate1,0) as Rate1, isnull(Rate2,0) as Rate2, isnull(Rate3,0) as Rate3, isnull(DisplayRateName1,'') as DisplayRateName1, "
                + " isnull(DisplayRateName2,'') as DisplayRateName2, isnull(DisplayRateName3,'') as DisplayRateName3, isnull(CreatedBy,'') as CreatedBy, "
                + " isnull((Select top 1 AirlineName From ACRF_Airlines D where D.Id=Q.AirlineId),'') as Airline, "
                + " isnull((Select top 1 AirlinePhoto From ACRF_Airlines D where D.Id=Q.AirlineId),'') as AirlinePhoto, "
                + " isnull((Select top 1 CityName From ACRF_DestinationMaster D where D.CityCode= Q.DCityCode),'') as DCityName, "
                + " isnull((Select top 1 TariffMode From ACRF_TariffMode D where D.Id= Q.TariffModeId),'') as TariffMode, "
                + " isnull(CreatedOn,'') as CreatedOn from ACRF_QuotationDetails Q where QuotationId=@QuotationId";
                cmd.CommandText = sqlstr;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@QuotationId", QuotationId);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ACRF_QuotationDetailsModel tempobj = new ACRF_QuotationDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.QuotationId = Convert.ToInt32(sdr["QuotationId"].ToString());
                    tempobj.AirlineId = Convert.ToInt32(sdr["AirlineId"].ToString());
                    tempobj.TariffModeId = Convert.ToInt32(sdr["TariffModeId"].ToString());
                    tempobj.OCountryCode = sdr["OCountryCode"].ToString();
                    tempobj.OCityCode = sdr["OCityCode"].ToString();
                    tempobj.OAirportName = sdr["OAirportName"].ToString();
                    tempobj.DCountryCode = sdr["DCountryCode"].ToString();
                    tempobj.DCityCode = sdr["DCityCode"].ToString();
                    tempobj.DAirportName = sdr["DAirportName"].ToString();
                    tempobj.Slab1 = Convert.ToDecimal(sdr["Slab1"].ToString());
                    tempobj.Rate = Convert.ToDecimal(sdr["Rate"].ToString());
                    tempobj.Freight = Convert.ToDecimal(sdr["Freight"].ToString());
                    tempobj.FSC = Convert.ToDecimal(sdr["FSC"].ToString());
                    tempobj.WSC = Convert.ToDecimal(sdr["WSC"].ToString());
                    tempobj.Xray = Convert.ToDecimal(sdr["Xray"].ToString());
                    tempobj.Mcc = Convert.ToDecimal(sdr["Mcc"].ToString());
                    tempobj.Ctc = Convert.ToDecimal(sdr["Ctc"].ToString());
                    tempobj.AMS = Convert.ToDecimal(sdr["AMS"].ToString());
                    tempobj.TotalCost = Convert.ToDecimal(sdr["TotalCost"].ToString());
                    tempobj.IsRate1 = Convert.ToBoolean(sdr["IsRate1"].ToString());
                    tempobj.IsRate2 = Convert.ToBoolean(sdr["IsRate2"].ToString());
                    tempobj.IsRate3 = Convert.ToBoolean(sdr["IsRate3"].ToString());
                    tempobj.Rate1 = Convert.ToDecimal(sdr["Rate1"].ToString());
                    tempobj.Rate2 = Convert.ToDecimal(sdr["Rate2"].ToString());
                    tempobj.Rate3 = Convert.ToDecimal(sdr["Rate3"].ToString());
                    tempobj.DisplayRateName1 = sdr["DisplayRateName1"].ToString();
                    tempobj.DisplayRateName2 = sdr["DisplayRateName2"].ToString();
                    tempobj.DisplayRateName3 = sdr["DisplayRateName3"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();

                    tempobj.Airline = sdr["Airline"].ToString();

                    if (sdr["AirlinePhoto"].ToString() == "")
                    {
                        tempobj.AirlinePhoto = "";
                        tempobj.AirlineDemoPhoto = tempobj.Airline[0].ToString();
                    }
                    else
                    {
                        tempobj.AirlinePhoto = GlobalFunction.GetAPIUrl() + sdr["AirlinePhoto"].ToString();
                    }

                    objModel.DCityName = sdr["DCityName"].ToString();
                    objModel.TariffMode = sdr["TariffMode"].ToString();
                    objModel.Origin = sdr["OAirportName"].ToString();

                    objList.Add(tempobj);
                }
                
                connection.Close();
                objModel.ACRF_QuotationDetailsModelList = objList;
            }
            catch(Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }

        #endregion



        private ACRF_QuotationModel NullToBlank(ACRF_QuotationModel objModel)
        {
            if (objModel.BCC == null)
            {
                objModel.BCC = "";
            }
            if (objModel.CC == null)
            {
                objModel.CC = "";
            }
            if (objModel.MailSubject == null)
            {
                objModel.MailSubject = "";
            }

            return objModel;
        }

    }
}