using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class RoomTable
    {
        [Display(Name = "Room ID")]
        public int RID{ get; set; }
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }
        [Display(Name = "Room Size")]
        public string RoomSIZE { get; set; }
        [Display(Name = "Electri City Cost")]
        public string ElectriCityCost { get; set; }
        [Display(Name = "Rant Amount")]
        public int RantAmt { get; set; }
        public int BBID { get; set; }
        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this.RID== 0)

                    Query = "Insert into  RoomTable  values(@RoomNumber,@RoomSIZE,@ElectriCityCost,@RantAmt,@BBID);";
                else
                    Query = "update  RoomTable set RoomNumber=@RoomNumber,RoomSIZE=@RoomSIZE,ElectriCityCost=@ElectriCityCost,RantAmt=@RantAmt,BBID=@BBID where RID=@RID";


                SqlCommand cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@RID", this.RID);
                cmd.Parameters.AddWithValue("@RoomNumber", this.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomSIZE", this.RoomSIZE);
                cmd.Parameters.AddWithValue("@ElectriCityCost", this.ElectriCityCost);
                cmd.Parameters.AddWithValue("@RantAmt", this.RantAmt);
                cmd.Parameters.AddWithValue("@BBID", this.BBID);
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<RoomTable> GetAll()
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<RoomTable> ListTmp = new List<RoomTable>();

            try
            {
                string Query = "SELECT * FROM  RoomTable ORDER BY RID DESC";
                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    RoomTable ObjTmp = new RoomTable();
                    ObjTmp.RID= SDR.GetInt32(0);
                    ObjTmp.RoomNumber = SDR.GetString(1);
                    ObjTmp.RoomSIZE = SDR.GetString(2);
                    ObjTmp.ElectriCityCost = SDR.GetString(3);
                    ObjTmp.RantAmt = SDR.GetInt32(4);
                    ObjTmp.BBID = SDR.GetInt32(5);

                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }

            return (ListTmp);
        }
        public RoomTable GetOne(int RID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            RoomTable ObjTmp = new RoomTable();

            try
            {
                string Query = "SELECT * FROM  RoomTable where RID=@RID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@RID", RID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.RID= SDR.GetInt32(0);
                    ObjTmp.RoomNumber = SDR.GetString(1);
                    ObjTmp.RoomSIZE = SDR.GetString(2);
                    ObjTmp.ElectriCityCost = SDR.GetString(3);
                    ObjTmp.RantAmt = SDR.GetInt32(4);
                    ObjTmp.BBID = SDR.GetInt32(5);
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
                string Query = "Delete FROM  RoomTable where RID=" + ID;
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