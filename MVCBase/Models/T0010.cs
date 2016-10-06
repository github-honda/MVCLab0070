using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace MVCBase.Models
{
    public class T0010
    {
        public string ms1 { get; set; }  // 編號
        public string ms2 { get; set; }  // 姓名
        public int mi1 { get; set; }     // 國文
        public int mi2 { get; set; }     // 英文
        public T0010 Read1Record(string sFS01)
        {
            T0010 t1 = new T0010();
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString())) {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("select FS01, FS02, FI01, FI02 from T0010 where FS01=@FS01", cnn1)) {
                    cmd1.Parameters.Add("@FS01", SqlDbType.VarChar, 50).Value = sFS01; 
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        t1.ms1 = reader1.GetString(0);
                        t1.ms2 = reader1.GetString(1);
                        t1.mi1 = reader1.GetInt32(2);
                        t1.mi2 = reader1.GetInt32(3);
                    }
                }
            }
            return t1;
        }
        public List<T0010> ReadList()
        {
            List<T0010> oList = new List<T0010>();
            using (SqlConnection cnn1 = new SqlConnection(GetConnectionsString()))
            {
                cnn1.Open();
                using (SqlCommand cmd1 = new SqlCommand("select FS01, FS02, FI01, FI02 from T0010 order by FS01", cnn1))
                {
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        T0010 t1 = new T0010();
                        t1.ms1 = reader1.GetString(0);
                        t1.ms2 = reader1.GetString(1);
                        t1.mi1 = reader1.GetInt32(2);
                        t1.mi2 = reader1.GetInt32(3);
                        oList.Add(t1);
                    }
                }
            }
            return oList;
        }
        string GetConnectionsString()
        {
            string sConnectionString = "請在Web.Config中設定name=LocalDB01的ConnectionString";
            Configuration configuration1 = WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringSettings setting1;
            if (configuration1.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                setting1 = configuration1.ConnectionStrings.ConnectionStrings["LocalDB01"];
                if (setting1 != null)
                    sConnectionString = setting1.ConnectionString;
            }
            return sConnectionString;
        }

    }
}

