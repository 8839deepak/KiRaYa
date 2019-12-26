using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KiRaYa.Models
{
    public class BlockD
    {
        public int DDID { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Save()
        {
            int Row = 0;
            string Query = "";
            SqlCommand cmd = null;
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            try
            {
                Con.Open();

                if (this. DDID  == 0)

                    Query = "Insert into  BlockD  values(@Name,@Size );";
                else
                    Query = "update  BlockD set Name=@Name,Size=@Size  where  DDID =@ DDID ";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@DDID", this.DDID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Size", this.Size);
               
                Row = cmd.ExecuteNonQuery();
            }
            catch (Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return Row;
        }

        public List<BlockD> GetAll()
        {

            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            List<BlockD> ListTmp = new List<BlockD>();

            try
            {
                string Query = "SELECT * FROM  BlockD ORDER BY  DDID  DESC";

                cmd = new SqlCommand(Query, Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    BlockD ObjTmp = new BlockD();
                    ObjTmp.DDID  = SDR.GetInt32(0);
                    ObjTmp.Name = SDR.GetString(1);
                    ObjTmp.Size = SDR.GetInt32(2);
                    


                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { Con.Close(); }
            return (ListTmp);
        }
        public BlockD GetOne(int CCID)
        {
            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ToString());
            Con.Open();
            SqlCommand cmd = null;
            SqlDataReader SDR = null;
            BlockD ObjTmp = new BlockD();

            try
            {
                string Query = "SELECT * FROM  BlockD where  DDID =@ DDID ";
                cmd = new SqlCommand(Query, Con);
                cmd.Parameters.AddWithValue("@DDID", DDID);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp.DDID  = SDR.GetInt32(0);
                    ObjTmp.Name = SDR.GetString(1);
                    ObjTmp.Size = SDR.GetInt32(2);

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
                string Query = "Delete FROM  BlockD where  DDID =" + ID;
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