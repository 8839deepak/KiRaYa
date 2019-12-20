using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class ForgetPassword
    {
        public int FID { get; set; }
        public string EmailID { get; set; }
        public string NewPassword { get; set; }

        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlCommand cmd = null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this.FID == 0)

                    Query = "Insert into  ForgetPassword  values(@EmailID,@NewPassword);";
                else
                    Query = "update  ForgetPassword set EmailID=@EmailID,NewPassword=@NewPassword where FID=@FID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@FID", this.FID);
                cmd.Parameters.AddWithValue("@EmailID", this.EmailID);
                cmd.Parameters.AddWithValue("@NewPassword", this.NewPassword);
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<ForgetPassword> GetAll()
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<ForgetPassword> ListTmp = new List<ForgetPassword>();

            try
            {
                string Query = "SELECT * FROM  ForgetPassword ORDER BY FID DESC";

                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ForgetPassword ObjTmp = new ForgetPassword();
                    ObjTmp.FID = SDR.GetInt32(0);
                    ObjTmp.EmailID = SDR.GetString(1);


                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return (ListTmp);
        }
        public ForgetPassword GetOne(int FID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            ForgetPassword ObjTmp = new ForgetPassword();

            try
            {
                string Query = "SELECT * FROM  ForgetPassword where FID=@FID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@FID", FID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.FID = SDR.GetInt32(0);
                    ObjTmp.EmailID = SDR.GetString(1);
                }
            }
            catch (System.Exception e)
            { e.ToString(); }

            finally { Con.Close(); }

            return (ObjTmp);
        }

        public int Dell(int ID)
        {
            int R = 0;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            try
            {
                string Query = "Delete FROM  ForgetPassword where FID=" + ID;
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