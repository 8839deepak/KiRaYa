using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class BlockC
    {
        public int CCID { get; set; }
        public string Name { get; set; }
        public int DDID { get; set; }
        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlCommand cmd = null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this.CCID == 0)

                    Query = "Insert into  BlockC  values(@Name,@DDID);";
                else
                    Query = "update  BlockC set Name=@Name,DDID=@DDID where CCID=@CCID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@CCID", this.CCID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@DDID", this.DDID);
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<BlockC> GetAll()
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<BlockC> ListTmp = new List<BlockC>();

            try
            {
                string Query = "SELECT * FROM  BlockC ORDER BY CCID DESC";

                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    BlockC ObjTmp = new BlockC();
                    ObjTmp.CCID = SDR.GetInt32(0);
                    ObjTmp.Name = SDR.GetString(1);
                    ObjTmp.DDID = SDR.GetInt32(2);


                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return (ListTmp);
        }
        public BlockC GetOne(int CCID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            BlockC ObjTmp = new BlockC();

            try
            {
                string Query = "SELECT * FROM  BlockC where CCID=@CCID";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@CCID", CCID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.CCID = SDR.GetInt32(0);
                    ObjTmp.Name = SDR.GetString(1);
                    ObjTmp.DDID = SDR.GetInt32(2);
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
                string Query = "Delete FROM  BlockC where CCID=" + ID;
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