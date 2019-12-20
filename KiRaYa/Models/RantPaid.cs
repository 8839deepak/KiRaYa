using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class RantPaid
    {
        [Display(Name = "Rental ID")]
        public int RPID  { get; set; }
        [Display(Name = "Rantal Name")]
        public int RantalID { get; set; }
        [Display(Name = "Room Number")]
        public int RID { get; set; }
        [Display(Name = "Last Reading")]
        public int LastReading { get; set; }
        [Display(Name = "Current Reading")]
        public int CurrentReading { get; set; }
        [Display(Name = "Electricity Used")]
        public int UsedElectricity { get; set; }
        [Display(Name = "Total Rant")]
        public int TotalRant { get; set; }

        [Display(Name = "Total Pay")]
        public int TotalPay { get; set; }
        [Display(Name = "Pay Amount")]
        public int Amt { get; set; }
        [Display(Name = "Remaining Peyment")]
        public int Remaining { get; set; }
        public int Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public int Update_By { get; set; }
        public DateTime Update_Date { get; set; }
        public RantPaid()
        {
            Remaining = 0;
        }
        public int Save()
        {
            int Row = 0;
            SqlCommand cmd = null;
            string Query = "";
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();
                 
                if (this.RPID  == 0)
                {
                    Query = "Insert into  RentPaid  values( @RantalID,@RID,@LastReading,@CurrentReading,@UsedElectricity,@TotalRant,@TotalPay,@Amt,@Remaining,@Create_By,@Create_Date,@Update_By,@Update_Date);";
                    cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@Create_By", HttpContext.Current.Session["ID"]);
                    cmd.Parameters.AddWithValue("@Create_Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Update_By", 0);
                    cmd.Parameters.AddWithValue("@Update_Date", DateTime.Now);
                }

                    
                else
                { 
                     Query = "update  RentPaid set RantalID =@RantalID,RID=@RID,LastReading =@LastReading,CurrentReading=@CurrentReading,UsedElectricity=@UsedElectricity,TotalRant=@TotalRant,TotalPay=@TotalPay,Amt=@Amt,Remaining=@Remaining,Update_By=@Update_By,Update_Date=@Update_Date where RPID=@RPID ";
                    cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@RPID", this.RPID);
                    cmd.Parameters.AddWithValue("@Update_By", HttpContext.Current.Session["ID"]);
                    cmd.Parameters.AddWithValue("@Update_Date", DateTime.Now);
                }
               
                cmd.Parameters.AddWithValue("@RantalID", this.RantalID);
                cmd.Parameters.AddWithValue("RID", this.RID);
                cmd.Parameters.AddWithValue("LastReading", this.LastReading);
                cmd.Parameters.AddWithValue("CurrentReading", this.CurrentReading);
                cmd.Parameters.AddWithValue("UsedElectricity", this.UsedElectricity);
                cmd.Parameters.AddWithValue("TotalRant", this.TotalRant);
                cmd.Parameters.AddWithValue("TotalPay", this.TotalPay);
                cmd.Parameters.AddWithValue("@Amt", this.Amt);
                cmd.Parameters.AddWithValue("@Remaining", this.Remaining);
                Row = cmd.ExecuteNonQuery();
            
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<RantPaid> GetAll()
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<RantPaid> ListTmp = new List<RantPaid>();

            try
            {
                string Query = "SELECT * FROM  RentPaid ORDER BY RPID  DESC";
                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    RantPaid ObjTmp = new RantPaid();
                    ObjTmp.RPID = SDR.GetInt32(0);
                    ObjTmp.RantalID = SDR.GetInt32(1);
                    ObjTmp.RID = SDR.GetInt32(2);
                    ObjTmp.LastReading = SDR.GetInt32(3);
                    ObjTmp.CurrentReading = SDR.GetInt32(4);
                    ObjTmp.UsedElectricity = SDR.GetInt32(5);
                    ObjTmp.TotalRant = SDR.GetInt32(6);
                    ObjTmp.TotalPay = SDR.GetInt32(7);
                    ObjTmp.Amt = SDR.GetInt32(8);
                    ObjTmp.Remaining = SDR.GetInt32(9);

                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }

            return (ListTmp);
        }
        public RantPaid GetOne(int RPID )
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            RantPaid ObjTmp = new RantPaid();

            try
            {
                string Query = "SELECT * FROM  RentPaid where RPID =@RPID ";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@RPID ", RPID );
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.RPID = SDR.GetInt32(0);
                    ObjTmp.RantalID = SDR.GetInt32(1);
                    ObjTmp.RID = SDR.GetInt32(2);
                    ObjTmp.LastReading = SDR.GetInt32(3);
                    ObjTmp.CurrentReading = SDR.GetInt32(4);
                    ObjTmp.UsedElectricity = SDR.GetInt32(5);
                    ObjTmp.TotalRant = SDR.GetInt32(6);
                    ObjTmp.TotalPay = SDR.GetInt32(7);
                    ObjTmp.Amt = SDR.GetInt32(8);
                    ObjTmp.Remaining = SDR.GetInt32(9);
                }
            }
            catch (System.Exception e)
            { e.ToString(); }

            finally { Con.Close(); }

            return (ObjTmp);
        }

        public int Dell(int  ID)
        {
            int R = 0;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            try
            {
                string Query = "Delete FROM  RentPaid where RPID =" +  ID;
                cmd = new SqlCommand(Query, Con);
                R = cmd.ExecuteNonQuery();
            }
            catch (System.Exception e)
            { e.ToString(); }

            finally
            {
                Con.Close();
            }
            return R;

        }
    }
}