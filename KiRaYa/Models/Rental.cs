using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class Rental
    {
        [Display(Name = "Rental ID")]
        public int RantalID { get; set; }
        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile{ get; set; }
        [Display(Name = "Address")]
        public string Address{ get; set; }
        public string VoterID { get; set; }
        public string AdharcardPhoto { get; set; }
        public string AdharNumber { get; set; }
        public string PanPhoto { get; set; }
        public string PanNumber { get; set; }
        public string RentAgreementFor { get; set; }
        [Display(Name = "Room ID")]
        public int RID { get; set; }
        public int createBy { get; set; }
        public DateTime Updatedate { get; set; }
        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlCommand cmd = null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this.RantalID == 0)
                {
                    Query = "Insert into  Rental  values(@PhotoPath, @Name,@Date,@Mobile,@Address,@VoterID,@AdharcardPhoto,@AdharNumber,@PanPhoto,@PanNumber,@RentAgreementFor,@RID,@v,@Updatedate);";
                    cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@createBy", HttpContext.Current.Session["ID"]);
                    cmd.Parameters.AddWithValue("@Updatedate", DateTime.Now);
                }
                    
                else
                { 
                    Query = "update  Rental set  PhotoPath=@PhotoPath, Name=@Name,Date=@Date,Mobile=@Mobile,Address=@Address,VoterID=@VoterID,AdharcardPhoto=@AdharcardPhoto,AdharNumber=@AdharNumber,PanPhoto=@PanPhoto,PanNumber=@PanNumber,RentAgreementFor=@RentAgreementFor,RID=@RID,createBy=@createBy,Updatedate=@Updatedate where RantalID=@RantalID";
                 cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@RantalID", this.RantalID);
                    cmd.Parameters.AddWithValue("@createBy", HttpContext.Current.Session["ID"]);
                    cmd.Parameters.AddWithValue("@Updatedate", DateTime.Now);
                   
                }
                
                cmd.Parameters.AddWithValue("@PhotoPath", this.PhotoPath);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Mobile", this.Mobile);
                cmd.Parameters.AddWithValue("@Address", this.Address);
                cmd.Parameters.AddWithValue("@VoterID", this.VoterID);
                cmd.Parameters.AddWithValue("@AdharcardPhoto", this.AdharcardPhoto);
                cmd.Parameters.AddWithValue("@AdharNumber", this.AdharNumber);
                cmd.Parameters.AddWithValue("@PanPhoto", this.PanPhoto);
                cmd.Parameters.AddWithValue("@PanNumber", this.PanNumber);
                cmd.Parameters.AddWithValue("@RentAgreementFor", this.RentAgreementFor);
                cmd.Parameters.AddWithValue("@RID", this.RID);
                cmd.Parameters.AddWithValue("@createBy", HttpContext.Current.Session["ID"]);
                cmd.Parameters.AddWithValue("@Updatedate", DateTime.Now);
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<Rental> GetAll()
        {
            
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<Rental> ListTmp = new List<Rental>();

            try
            {
                string Query = "SELECT * FROM  Rental ORDER BY RantalID DESC";
               
                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    Rental ObjTmp = new Rental();
                    ObjTmp.RantalID = SDR.GetInt32(0);
                    ObjTmp.PhotoPath = SDR.GetString(1);
                    ObjTmp.Name = SDR.GetString(2);
                    ObjTmp.Date = SDR.GetDateTime(3);
                    ObjTmp.Mobile = SDR.GetString(4);
                    ObjTmp.Address = SDR.GetString(5);
                    ObjTmp.VoterID = SDR.GetString(6);
                    ObjTmp.AdharcardPhoto = SDR.GetString(7);
                    ObjTmp.AdharNumber = SDR.GetString(8);
                    ObjTmp.PanPhoto = SDR.GetString(9);
                    ObjTmp.PanNumber = SDR.GetString(10);
                    ObjTmp.RentAgreementFor = SDR.GetString(11);
                    ObjTmp.RID = SDR.GetInt32(12);
                    ObjTmp.createBy = SDR.GetInt32(13);
                    ObjTmp.Updatedate = SDR.GetDateTime(14);

                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return (ListTmp);
        }
        public Rental GetOne(int RantalID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            Rental ObjTmp = new Rental();

            try
            {
                string Query = "SELECT * FROM  Rental where RantalID=@RantalID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@RantalID", RantalID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.RantalID = SDR.GetInt32(0);
                    ObjTmp.PhotoPath = SDR.GetString(1);
                    ObjTmp.Name = SDR.GetString(2);
                    ObjTmp.Date = SDR.GetDateTime(3);
                    ObjTmp.Mobile = SDR.GetString(4);
                    ObjTmp.Address = SDR.GetString(5);
                    ObjTmp.VoterID = SDR.GetString(6);
                    ObjTmp.AdharcardPhoto = SDR.GetString(7);
                    ObjTmp.AdharNumber = SDR.GetString(8);
                    ObjTmp.PanPhoto = SDR.GetString(9);
                    ObjTmp.PanNumber = SDR.GetString(10);
                    ObjTmp.RentAgreementFor = SDR.GetString(11);
                    ObjTmp.RID = SDR.GetInt32(12);
                    ObjTmp.createBy = SDR.GetInt32(13);
                    ObjTmp.Updatedate = SDR.GetDateTime(14);
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
                string Query = "Delete FROM  Rental where RantalID=" + ID;
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