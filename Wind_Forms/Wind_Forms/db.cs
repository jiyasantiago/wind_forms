using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;


namespace Wind_Forms
{
    class db
    {
        public SqlConnection conn = new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=Jea;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public void load_data(string q, ListView lv)
        {
            lv.Items.Clear();
            try
            {
                SqlDataReader dr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = q;
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ListViewItem item = new ListViewItem(dr["Id"].ToString());
                        item.SubItems.Add(dr["email"].ToString());
                        item.SubItems.Add(dr["password"].ToString());
                        lv.Items.Add(item);
                    }
                }
                dr.Close();
                conn.Close();
            }
            catch(Exception e)
            {
                //cn.Close();
                MessageBox.Show(e.Message.ToString());
            }
         
        }

        public void saveData(string sqlCreate, string sqlUpdate, TextBox txtid) {
            cmd.Connection = conn;

            SqlCommand cmdCheckId = new SqlCommand("select id from tblInfo where id = '" + txtid.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdCheckId);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //update
                conn.Open();
                SqlCommand cmd1 = conn.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = sqlUpdate;

                cmd1.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Updated Successfully!");

            }
            else {
                //Insert
                conn.Open();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = sqlCreate;

                cmd2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Added Successfully!");
            }
            conn.Close();
        }
    }
}
