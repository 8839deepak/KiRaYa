using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public string UserType { get; set; }

        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlCommand cmd = null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this.UserID == 0)
                {
                    Query = "Insert into  UserDetails  values(@UserType);";
                    cmd = new SqlCommand(Query, Con);
                    
                }

                else
                {
                    Query = "update  UserDetails set UserType=@UserType where UserID=@UserID";
                    cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@UserID", this.UserID);
                     

                }

              
                cmd.Parameters.AddWithValue("@UserType", this.UserType);
               
              
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<UserDetails> GetAll()
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<UserDetails> ListTmp = new List<UserDetails>();

            try
            {
                string Query = "SELECT * FROM  UserDetails ORDER BY UserID DESC";

                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    UserDetails ObjTmp = new UserDetails();
                    ObjTmp.UserID = SDR.GetInt32(0);
                    ObjTmp.UserType = SDR.GetString(1);
                   

                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return (ListTmp);
        }
        public UserDetails GetOne(int UserID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            UserDetails ObjTmp = new UserDetails();

            try
            {
                string Query = "SELECT * FROM  UserDetails where UserID=@UserID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.UserID = SDR.GetInt32(0);
                    ObjTmp.UserType = SDR.GetString(1);
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
                string Query = "Delete FROM  UserDetails where UserID=" + ID;
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