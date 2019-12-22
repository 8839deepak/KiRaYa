using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class BlockBRoom
    {
        public int BBID { get; set; }
        public string Name{get; set; }
        public int CCID { get; set; }
        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlCommand cmd = null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this.BBID == 0)

                    Query = "Insert into  BlockBRoom  values(@Name,@CCID);";
                else
                    Query = "update  BlockBRoom set Name=@Name,CCID=@CCID where BBID=@BBID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@CCID", this.CCID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@CCID", this.CCID);
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<BlockBRoom> GetAll()
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<BlockBRoom> ListTmp = new List<BlockBRoom>();

            try
            {
                string Query = "SELECT * FROM  BlockBRoom ORDER BY BBID DESC";

                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    BlockBRoom ObjTmp = new BlockBRoom();
                    ObjTmp.BBID = SDR.GetInt32(0);
                    ObjTmp.Name = SDR.GetString(1);
                    ObjTmp.CCID = SDR.GetInt32(2);


                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return (ListTmp);
        }
        public BlockBRoom GetOne(int CCID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            BlockBRoom ObjTmp = new BlockBRoom();

            try
            {
                string Query = "SELECT * FROM  BlockBRoom where BBID=@BBID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@CCID", CCID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.BBID = SDR.GetInt32(0);
                    ObjTmp.Name = SDR.GetString(1);
                    ObjTmp.CCID = SDR.GetInt32(2);
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
                string Query = "Delete FROM  BlockBRoom where BBID=" + ID;
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