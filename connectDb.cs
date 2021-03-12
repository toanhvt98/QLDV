using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace QLDV
{
    class connectDb
    {
        SqlConnection con = new SqlConnection(Properties.Settings.Default.sqlStr.ToString());
        
        public bool checkLogin(string tentk, string matkhau, string quyentruycap)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select tentk,matkhau,quyentruycap from taikhoan",con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader.GetString(0) == tentk && reader.GetString(1) == matkhau && reader.GetString(2) == quyentruycap)
                        {
                            con.Close();
                            return true;
                        }
                    }
                }
            }
            con.Close();
            return false;
        }
        public void rfAccDataGrid(DataGridView dtg, string tbl)
        {
            using (con)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select * from " + tbl, con))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    dtg.DataSource = dtb;
                }
            }
        }

        public void dtgFilter(DataGridView dtg,string col,string text, string tbl)
        {
            using (con)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select * from " + tbl, con))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    dtb.DefaultView.RowFilter = col + " like '%" + text + "%'";
                    dtg.DataSource = dtb;
                }
            }
        }


        // phuong thuc su dung quan ly tai khoan
        public bool checkAccAlready(string acc)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select tentk from taikhoan", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader.GetString(0) == acc)
                        {
                            con.Close();
                            return false;
                        }
                    }
                }
            }
            con.Close();
            return true;
        }
        

        public void addAcc(string acc, string pass, string codeCb,string per)
        {
            using (SqlCommand cmd = new SqlCommand("insert into taikhoan (tentk,matkhau,chibo,quyentruycap) values(@acc,@pass,@codeCb,@per)", con))
            {
                cmd.Parameters.AddWithValue("@acc",SqlDbType.NVarChar).Value = acc;
                cmd.Parameters.AddWithValue("@pass", SqlDbType.NVarChar).Value = pass;
                cmd.Parameters.AddWithValue("@codeCb", SqlDbType.NVarChar).Value = codeCb;
                cmd.Parameters.AddWithValue("@per", SqlDbType.VarChar).Value = per;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void updateAcc(int id,string pass)
        {
            using (SqlCommand cmd = new SqlCommand("update taikhoan set matkhau=@pass where id=" + id, con))
            {
                
                cmd.Parameters.AddWithValue("@pass", SqlDbType.NVarChar).Value = pass;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void del(int id,string table) // dung cho chibo va taikhoan
        {
            using (SqlCommand cmd = new SqlCommand("delete from "+table+" where id=" + id, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // phuong thuc su dung quan ly chi bo

        public void addChiBo(string ma, string ten)
        {
            using (SqlCommand cmd = new SqlCommand("insert into chibo (ma,ten) values (@ma,@ten)", con))
            {
                cmd.Parameters.AddWithValue("@ma", SqlDbType.NVarChar).Value = ma;
                cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public bool checkChiBoAlready(string col,string txt)
        {
            int a = 0;
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select "+col+" from chibo", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(0) == txt)
                        {
                            a = 1;
                            con.Close();
                            break;
                        }
                    }
                }
            }
            con.Close();
            if (a == 1)
                return false;
            else
                return true;
        }

        public void updateChiBo(int id, string ten)
        {
            using (SqlCommand cmd = new SqlCommand("update chibo set ten=@ten where id=" + id, con))
            {
                cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // phuong thuc quan ly dang vien

        public bool checkSllVaSt(string opt)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select "+opt+" from dangvien",con))
            {

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        if(read.GetString(0) == opt)
                        {
                            con.Close();
                            return false;
                        }
                    }
                }
            }
            con.Close();
            return true;
        }
        public void addSllVaSt(byte[] img,string sll, string st)
        {
            using (SqlCommand cmd = new SqlCommand("insert into dangvien (anhdangvien,solylich,sothe) values (@img,@sll,@st)", con))
            {
                cmd.Parameters.AddWithValue("@img", SqlDbType.Image).Value = img;
                cmd.Parameters.AddWithValue("@sll", SqlDbType.NVarChar).Value = sll;
                cmd.Parameters.AddWithValue("@st", SqlDbType.NVarChar).Value = st;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        

    }
}
