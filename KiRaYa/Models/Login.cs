using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class Login
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string EmailID { get; set; }
        public int UserID { get; set; }
        public Login()
            {
            UserID = 0;

            }


        public Login addloginuser(int ID)
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            Login objtemp = new Login();
            try
            {
                string Query = "SELECT * FROM Login where ID=@ID";
                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while(SDR.Read())
                {
                    Login ObjTmp = new Login();
                    ObjTmp.ID = SDR.GetInt32(0);
                    ObjTmp.UserName = SDR.GetString(1);
                    ObjTmp.Password = SDR.GetString(2);
                    ObjTmp.UserType = SDR.GetString(3);
                    ObjTmp.EmailID = SDR.GetString(4);
                    ObjTmp.UserID = SDR.GetInt32(5);
                    
                }
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); cmd.Dispose(); }
            return objtemp;
        }
        public int Save()
        {
            int Row = 0;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();
                string Query = "";
                if (this.ID == 0)
                    Query = "Insert into Login values (@UserName,@Password,@UserType,@EmailID,@UserID);";
                else
                    Query = "Update Login set Username=@UserName,Password=@Password,UserType=@UserType,EmailID=@EmailID,UserID=@UserID where ID=@ID;";

                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@ID", this.ID);
                cmd.Parameters.AddWithValue("@UserName", this.UserName);
                cmd.Parameters.AddWithValue("@Password", this.Password);
                cmd.Parameters.AddWithValue("@UserType", this.UserType);
                cmd.Parameters.AddWithValue("@EmailID", this.EmailID);
                cmd.Parameters.AddWithValue("@UserID", this.UserID);
               
                Row = cmd.ExecuteNonQuery();

            }
            catch (System.Exception e)
            { e.ToString(); }

            finally
            {
                Con.Close(); Con.Dispose(); Con = null;
            }
            return Row;
        }
        //Get All Function
        public Login GetOne(int ID)
        {
            Login ObjTmp = new Login();
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();
                string Query = "Select * from Login where ID=@ID";
                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@ID", ID);
                SqlDataReader SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.ID = SDR.GetInt32(0);
                    ObjTmp.UserName = SDR.GetString(1);
                    ObjTmp.Password = SDR.GetString(2);
                    ObjTmp.UserType = SDR.GetString(3);
                    ObjTmp.EmailID = SDR.GetString(4);
                    ObjTmp.UserID = SDR.GetInt32(5);
                     
                }
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return ObjTmp;
        }
        //Get All Function
        public List<Login> GetAll()
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            List<Login> ListTmp = new List<Login>();
            string Query = "Select * from Login order by ID desc";
            SqlCommand cmd = new SqlCommand(Query, Con);
            try
            {
                SqlDataReader SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    Login ObjTmp = new Login();
                    ObjTmp.ID = SDR.GetInt32(0);
                    ObjTmp.UserName = SDR.GetString(1);
                    ObjTmp.Password = SDR.GetString(2);
                    ObjTmp.UserType = SDR.GetString(3);
                    ObjTmp.EmailID = SDR.GetString(4);
                    ObjTmp.UserID = SDR.GetInt32(5);
                    
                    ListTmp.Add(ObjTmp);
                }
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); cmd.Dispose(); }
            return ListTmp;
        }
        public int Dell(int ID)
        {
            int R = 0;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            try
            {
                string Query = "Delete FROM  Login where ID=" + ID;
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