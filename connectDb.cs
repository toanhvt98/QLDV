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
using Microsoft.Win32;
using OfficeOpenXml;
using System.Diagnostics;

namespace QLDV
{
    class connectDb
    {
        
        
        
        public static string connect()
        {
            string a = "";
            if (File.Exists("connect.txt"))
            {
                a = File.ReadAllText("connect.txt");
            }
            return a;
        }

        public SqlConnection con = new SqlConnection(connect());
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

        public void selectDangVien(DataGridView dtg)
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();
            //DataTable dt = new DataTable();
            //con.Open();

            //using (SqlCommand cmd = new SqlCommand("select * from dangvien", con))
            //{
            //    dt = new DataTable();
            //    dt.Load(cmd.ExecuteReader());
            //}
            //con.Close();
            //dtg.DataSource = dt;

            try
            {
                Task.Run(() =>
                {
                    //Define DataTable
                    var table = new DataTable();
                    
                    table.Columns.Add("Column 1", typeof(int));
                    table.Columns.Add("Column 2", typeof(byte[]));
                    table.Columns.Add("Column 3", typeof(string));
                    table.Columns.Add("Column 4", typeof(string));
                    table.Columns.Add("Column 5", typeof(string));
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from dangvien", con))
                    {
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                table.Rows.Add(new object[] { Convert.ToInt32(read.GetValue(0)), (byte[]) read.GetValue(1), read.GetValue(2), read.GetValue(3), read.GetValue(4) });
                            }
                        }
                    }
                    con.Close();

                        //Set DataSource
                   dtg.Invoke(new Action(() => { dtg.DataSource = table; dtg.Columns[0].HeaderText = "STT";
                       dtg.Columns[1].HeaderText = "Ảnh Đảng viên";
                       dtg.Columns[2].HeaderText = "Số lý lịch";
                       dtg.Columns[3].HeaderText = "Số thẻ";
                       dtg.Columns[4].HeaderText = "Tên Đảng viên";
                       dtg.Columns[0].Visible = false;
                       
                       ((DataGridViewImageColumn)dtg.Columns[1]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                      dtg.Columns[0].Width = 70;
                       dtg.Columns[1].Width = 170;
                       dtg.Columns[2].Width = 120;
                       dtg.Columns[3].Width = 120;
                       dtg.Columns[4].Width = 150;
                   }));
                });
            }
            catch
            {
                MessageBox.Show("Vuot qua so luong ");
            }

            //stopwatch.Stop();
            //MessageBox.Show(stopwatch.ElapsedMilliseconds.ToString());

        }

        public void rfGridFormThemVaThongTin(DataGridView dtg, string tbl,string solylich, string sothe)
        {
            using (con)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select * from " + tbl + " where solylich='"+solylich+"' or sothe='"+sothe+"'", con))
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


        // phuong thuc them chibo vao comboxbox

        public void themChiBoCombo(ComboBox cb)
        {
            con.Open();
            Dictionary<string, string> test = new Dictionary<string, string>();
            using (SqlDataAdapter ada = new SqlDataAdapter("select ten,ma from chibo", con))
            {
                DataTable dtb = new DataTable();
                ada.Fill(dtb);
                
                foreach (DataRow dr in dtb.Rows)
                {
                    test.Add(dr[1].ToString(), dr[0].ToString());
                    
                }
            }
            cb.DataSource = new BindingSource(test, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
            con.Close();
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

        public bool checkSllVaSt(string opt,string text)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select "+opt+" from dangvien",con))
            {

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        if(read.GetString(0) == text)
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
        public void addSllVaSt(byte[] img,string solylich, string sothe)
        {
            using (SqlCommand cmd = new SqlCommand("insert into dangvien (anhdangvien,solylich,sothe) values (@img,@sll,@st)", con))
            {
                cmd.Parameters.AddWithValue("@img", img);
                cmd.Parameters.AddWithValue("@sll", SqlDbType.NVarChar).Value = solylich;
                cmd.Parameters.AddWithValue("@st", SqlDbType.NVarChar).Value = sothe;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void updateAnh(byte[] img, string solylich, string sothe)
        {
            using (SqlCommand cmd = new SqlCommand("update dangvien set anhdangvien=@anh where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {

                cmd.Parameters.AddWithValue("@anh", img);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void updateDangvien(string ten, string solylich, string sothe)
        {
            using (SqlCommand cmd = new SqlCommand("update dangvien set tendangvien=@ten where solylich='"+solylich+"' or sothe='"+sothe+"'", con))
            {

                cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void dellDV(string solylich, string sothe)
        {
            using (SqlCommand cmd = new SqlCommand("delete  from hcgiadinh  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {              
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from qhgiadinh  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from quatrinhhoatdong  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from qhnuocngoai  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from dacdiemlichsu  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from daotaochuyenmon  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from daotaochuyenmon2  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from ttcbDv  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("delete  from dangvien" +
                "" +
                "  where solylich='" + solylich + "' or sothe='" + sothe + "'", con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        // form uclTTCBDangVien
        public void TTCBDangVien(string optionQuerry,string solylich, string sothedangvien, string tendangdung,string gioitinh,
            string tenkhaisinh,DateTime ngaysinh,string noisinh,
            string quequan,string noithuongtru,string noitamtru,string dantoc,string tongiao,string thanhphangd,
            string nghenghiephiennay,DateTime? ngayvaodang,string taichibo,string nguoigt1,string chucvudonvi1,
            string nguoigt2,string chucvudonvi2,DateTime? ngaycap,DateTime? ngaychinhthuc,string taichibo2,
            DateTime? ngayduoctuyendung,string coquantuyendung,DateTime? ngayvaodoan,string thamgiatochucxh,
            DateTime? ngaynhapngu,DateTime? ngayxuatngu,string trinhdohiennay,string gdphothong,string gdNgheNghiep,
            string gddaihoc,string gdsaudaihoc,string hocvi,string hocham,string lyluanct,string ngoaingu,string tinhoc,
            string tinhtrangsuckhoe,string thuongbinhloai,string giadinh,string cmnd,string cancuoccdan,DateTime? mienCtac)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into ttcbDv (solylich,sothe,tendangvien,gioitinh,tenkhaisinh, ngaysinh, " +
                "noisinh, quequan, noithuongtru, noitamtru, dantoc,tongiao, tpgiadinh, " +
                "nghehientai, ngayvaodang, taichibo, nguoigt1, chucvudonvi1, nguoigt2, chucvudonvi2, " +
                "ngaycapthamquyen, ngaychinhthuc, taichibo1,ngaytuyendung, coquantuyendung, ngayvaodoan, " +
                "thamgiatochucxh, ngaynhapngu, ngayxuatngu,trinhdohientai, gdphothong, gdnghenghiep, gddaihoc, " +
                "gdsaudaihoc, hocvi, hocham, lyluanct,ngoaingu, tinhoc, tinhtrangsuckhoe, thuongbinh, giadinh, cmnd, " +
                "cancuoc, miencongtac) values(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22,@23,@24,@25," +
                "@26,@27,@28,@29,@30,@31,@32,@33,@34,@35,@36,@37,@38,@39,@40,@41,@42,@43,@44,@45)", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothedangvien;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = tendangdung;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = gioitinh;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = tenkhaisinh;

                    cmd.Parameters.AddWithValue("@6", SqlDbType.Date).Value = ngaysinh;

                    
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = noisinh;
                    cmd.Parameters.AddWithValue("@8", SqlDbType.NVarChar).Value = quequan;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.NVarChar).Value = noithuongtru;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = noitamtru;
                    cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = dantoc;
                    cmd.Parameters.AddWithValue("@12", SqlDbType.NVarChar).Value = tongiao;
                    cmd.Parameters.AddWithValue("@13", SqlDbType.NVarChar).Value = thanhphangd;
                    cmd.Parameters.AddWithValue("@14", SqlDbType.NVarChar).Value = nghenghiephiennay;

                    if (ngayvaodang != null)
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = ngayvaodang;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@16", SqlDbType.NVarChar).Value = taichibo;
                    cmd.Parameters.AddWithValue("@17", SqlDbType.NVarChar).Value = nguoigt1;
                    cmd.Parameters.AddWithValue("@18", SqlDbType.NVarChar).Value = chucvudonvi1;
                    cmd.Parameters.AddWithValue("@19", SqlDbType.NVarChar).Value = nguoigt2;
                    cmd.Parameters.AddWithValue("@20", SqlDbType.NVarChar).Value = chucvudonvi2;

                    if (ngaycap != null)
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = ngaycap;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = DBNull.Value;
                    }

                    if (ngaychinhthuc != null)
                    {
                        cmd.Parameters.AddWithValue("@22", SqlDbType.Date).Value = ngaychinhthuc;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@22", SqlDbType.Date).Value = DBNull.Value;
                    }

                    
                    cmd.Parameters.AddWithValue("@23", SqlDbType.NVarChar).Value = taichibo2;

                    if (ngayduoctuyendung != null)
                    {
                        cmd.Parameters.AddWithValue("@24", SqlDbType.Date).Value = ngayduoctuyendung;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@24", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@25", SqlDbType.NVarChar).Value = coquantuyendung;

                    if (ngayvaodoan != null)
                    {
                        cmd.Parameters.AddWithValue("@26", SqlDbType.Date).Value = ngayvaodoan;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@26", SqlDbType.Date).Value = DBNull.Value;
                    }

                    
                    cmd.Parameters.AddWithValue("@27", SqlDbType.NVarChar).Value = thamgiatochucxh;

                    if(ngaynhapngu != null)
                    {
                        cmd.Parameters.AddWithValue("@28", SqlDbType.Date).Value = ngaynhapngu;
                    } 
                    else
                    {
                        cmd.Parameters.AddWithValue("@28", SqlDbType.Date).Value = DBNull.Value;
                    }

                    if(ngayxuatngu != null)
                    {
                        cmd.Parameters.AddWithValue("@29", SqlDbType.Date).Value = ngayxuatngu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@29", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@30", SqlDbType.NVarChar).Value = trinhdohiennay;
                    cmd.Parameters.AddWithValue("@31", SqlDbType.NVarChar).Value = gdphothong;
                    cmd.Parameters.AddWithValue("@32", SqlDbType.NVarChar).Value = gdNgheNghiep;
                    cmd.Parameters.AddWithValue("@33", SqlDbType.NVarChar).Value = gddaihoc;
                    cmd.Parameters.AddWithValue("@34", SqlDbType.NVarChar).Value = gdsaudaihoc;
                    cmd.Parameters.AddWithValue("@35", SqlDbType.NVarChar).Value = hocvi;
                    cmd.Parameters.AddWithValue("@36", SqlDbType.NVarChar).Value = hocham;
                    cmd.Parameters.AddWithValue("@37", SqlDbType.NVarChar).Value = lyluanct;
                    cmd.Parameters.AddWithValue("@38", SqlDbType.NVarChar).Value = ngoaingu;
                    cmd.Parameters.AddWithValue("@39", SqlDbType.NVarChar).Value = tinhoc;
                    cmd.Parameters.AddWithValue("@40", SqlDbType.NVarChar).Value = tinhtrangsuckhoe;
                    cmd.Parameters.AddWithValue("@41", SqlDbType.NVarChar).Value = thuongbinhloai;
                    cmd.Parameters.AddWithValue("@42", SqlDbType.NVarChar).Value = giadinh;
                    cmd.Parameters.AddWithValue("@43", SqlDbType.NVarChar).Value = cmnd;
                    cmd.Parameters.AddWithValue("@44", SqlDbType.NVarChar).Value = cancuoccdan;

                    if (mienCtac != null)
                    {
                        cmd.Parameters.AddWithValue("@45", SqlDbType.Date).Value = mienCtac;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@45", SqlDbType.Date).Value = DBNull.Value;
                    }
                    

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update ttcbDv set tendangvien=@3,gioitinh=@4,tenkhaisinh=@5, ngaysinh=@6, " +
                "noisinh=@7, quequan=@8, noithuongtru=@9, noitamtru=@10, dantoc=@11,tongiao=@12, tpgiadinh=@13, " +
                "nghehientai=@14, ngayvaodang=@15, taichibo=@16, nguoigt1=@17, chucvudonvi1=@18, nguoigt2=@19, chucvudonvi2=@20, " +
                "ngaycapthamquyen=@21, ngaychinhthuc=@22, taichibo1=@23,ngaytuyendung=@24, coquantuyendung=@25, ngayvaodoan=@26, " +
                "thamgiatochucxh=@27, ngaynhapngu=@28, ngayxuatngu=@29,trinhdohientai=@30, gdphothong=@31, gdnghenghiep=@32, gddaihoc=@33, " +
                "gdsaudaihoc=@34, hocvi=@35, hocham=@36, lyluanct=@37,ngoaingu=@38, tinhoc=@39, tinhtrangsuckhoe=@40, thuongbinh=@41, giadinh=@42, cmnd=@43, " +
                "cancuoc=@44, miencongtac=@45" +
                " where solylich='"+solylich +"' or sothe='"+sothedangvien+"'", con))
                {
                    
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = tendangdung;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = gioitinh;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = tenkhaisinh;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.Date).Value = ngaysinh;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = noisinh;
                    cmd.Parameters.AddWithValue("@8", SqlDbType.NVarChar).Value = quequan;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.NVarChar).Value = noithuongtru;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = noitamtru;
                    cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = dantoc;
                    cmd.Parameters.AddWithValue("@12", SqlDbType.NVarChar).Value = tongiao;
                    cmd.Parameters.AddWithValue("@13", SqlDbType.NVarChar).Value = thanhphangd;
                    cmd.Parameters.AddWithValue("@14", SqlDbType.NVarChar).Value = nghenghiephiennay;
                    if (ngayvaodang != null)
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = ngayvaodang;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@16", SqlDbType.NVarChar).Value = taichibo;
                    cmd.Parameters.AddWithValue("@17", SqlDbType.NVarChar).Value = nguoigt1;
                    cmd.Parameters.AddWithValue("@18", SqlDbType.NVarChar).Value = chucvudonvi1;
                    cmd.Parameters.AddWithValue("@19", SqlDbType.NVarChar).Value = nguoigt2;
                    cmd.Parameters.AddWithValue("@20", SqlDbType.NVarChar).Value = chucvudonvi2;
                    if (ngaycap != null)
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = ngaycap;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = DBNull.Value;
                    }

                    if (ngaychinhthuc != null)
                    {
                        cmd.Parameters.AddWithValue("@22", SqlDbType.Date).Value = ngaychinhthuc;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@22", SqlDbType.Date).Value = DBNull.Value;
                    }


                    cmd.Parameters.AddWithValue("@23", SqlDbType.NVarChar).Value = taichibo2;

                    if (ngayduoctuyendung != null)
                    {
                        cmd.Parameters.AddWithValue("@24", SqlDbType.Date).Value = ngayduoctuyendung;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@24", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@25", SqlDbType.NVarChar).Value = coquantuyendung;
                    if (ngayvaodoan != null)
                    {
                        cmd.Parameters.AddWithValue("@26", SqlDbType.Date).Value = ngayvaodoan;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@26", SqlDbType.Date).Value = DBNull.Value;
                    }


                    cmd.Parameters.AddWithValue("@27", SqlDbType.NVarChar).Value = thamgiatochucxh;

                    if (ngaynhapngu != null)
                    {
                        cmd.Parameters.AddWithValue("@28", SqlDbType.Date).Value = ngaynhapngu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@28", SqlDbType.Date).Value = DBNull.Value;
                    }

                    if (ngayxuatngu != null)
                    {
                        cmd.Parameters.AddWithValue("@29", SqlDbType.Date).Value = ngayxuatngu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@29", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@30", SqlDbType.NVarChar).Value = trinhdohiennay;
                    cmd.Parameters.AddWithValue("@31", SqlDbType.NVarChar).Value = gdphothong;
                    cmd.Parameters.AddWithValue("@32", SqlDbType.NVarChar).Value = gdNgheNghiep;
                    cmd.Parameters.AddWithValue("@33", SqlDbType.NVarChar).Value = gddaihoc;
                    cmd.Parameters.AddWithValue("@34", SqlDbType.NVarChar).Value = gdsaudaihoc;
                    cmd.Parameters.AddWithValue("@35", SqlDbType.NVarChar).Value = hocvi;
                    cmd.Parameters.AddWithValue("@36", SqlDbType.NVarChar).Value = hocham;
                    cmd.Parameters.AddWithValue("@37", SqlDbType.NVarChar).Value = lyluanct;
                    cmd.Parameters.AddWithValue("@38", SqlDbType.NVarChar).Value = ngoaingu;
                    cmd.Parameters.AddWithValue("@39", SqlDbType.NVarChar).Value = tinhoc;
                    cmd.Parameters.AddWithValue("@40", SqlDbType.NVarChar).Value = tinhtrangsuckhoe;
                    cmd.Parameters.AddWithValue("@41", SqlDbType.NVarChar).Value = thuongbinhloai;
                    cmd.Parameters.AddWithValue("@42", SqlDbType.NVarChar).Value = giadinh;
                    cmd.Parameters.AddWithValue("@43", SqlDbType.NVarChar).Value = cmnd;
                    cmd.Parameters.AddWithValue("@44", SqlDbType.NVarChar).Value = cancuoccdan;
                    if (mienCtac != null)
                    {
                        cmd.Parameters.AddWithValue("@45", SqlDbType.Date).Value = mienCtac;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@45", SqlDbType.Date).Value = DBNull.Value;
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            
        }

        // form quatrinhhoatdong2

        public void quatrinhhoatdong(string optionQuerry,int id, string solylich, string sothe, DateTime tungay, DateTime denngay, string lamgi, string chucvu,string donvi)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into quatrinhhoatdong (solylich,sothe,tungay,denngay,lamgi,chucvu,dvcongtac)" +
                    " values (@1,@2,@3,@4,@5,@6,@7)", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothe;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = tungay;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = denngay;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = lamgi;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = chucvu;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = donvi;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update quatrinhhoatdong set tungay=@3,denngay=@4,lamgi=@5,chucvu=@6,dvcongtac=@7" +
                    " where id="+id, con))
                {
                    
                    cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = tungay;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = denngay;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = lamgi;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = chucvu;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = donvi;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        // form UCDaoTaoCHung3

        public void DaoTaoChung1(string optionQuerry,int id, string solylich, string sothe,string tentruong,string tennganh, DateTime tungay, DateTime denngay,string hinhthuc,string vanbang,string chungchi,string trinhdo)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into daotaochuyenmon (solylich,sothe,tentruong,nganhhoc,tungay,denngay," +
                    "hinhthuchoc,vanbang,chungchi,trinhdo) values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)",con))
                {
                    cmd.Parameters.AddWithValue("@1",SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothe;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = tentruong;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = tennganh;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.Date).Value = tungay;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.Date).Value = denngay;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = hinhthuc;
                    cmd.Parameters.AddWithValue("@8", SqlDbType.NVarChar).Value = vanbang;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.NVarChar).Value = chungchi;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = trinhdo;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update daotaochuyenmon set tentruong=@3,nganhhoc=@4,tungay=@5,denngay=@6," +
                    "hinhthuchoc=@7,vanbang=@8,chungchi=@9,trinhdo=@10 where id="+id, con))
                {
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = tentruong;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = tennganh;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.Date).Value = tungay;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.Date).Value = denngay;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = hinhthuc;
                    cmd.Parameters.AddWithValue("@8", SqlDbType.NVarChar).Value = vanbang;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.NVarChar).Value = chungchi;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = trinhdo;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void DaoTaoChung2(string optionQuerry,string solylich, string sothedangvien, string khenthuong, string huyhieudang ,string danhhieu, string kyluat)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into daotaochuyenmon2 (solylich,sothe,khenthuong,huyhieu,danhhieu,kyluat) values (@1,@2,@3,@4,@5,@6)", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothedangvien;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NText).Value = khenthuong;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = huyhieudang;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NText).Value = danhhieu;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NText).Value = kyluat;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update daotaochuyenmon2 set khenthuong=@1,huyhieu=@2,danhhieu=@3,kyluat=@4 where solylich='"+solylich +"' or sothe='"+sothedangvien+"'", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NText).Value = khenthuong;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = huyhieudang;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NText).Value = danhhieu;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NText).Value = kyluat;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            
        }

        // form DdlsVaQhng4

        public void Ddls(string optionQuerry,string solylich,string sothe,string bixoaten, DateTime? thoigian, 
                            string xoataichibo, string ketnaplai, DateTime? ngayvao, string vaochibo,
                            string vaonguoigt1, string vaochucvu1, string vaodonvi1, string vaonguoigt2, string vaochucvu2, string vaodonvi2,
                            DateTime? ngaychinhthuc2, string vaochibo2, DateTime? ngaykhoiphucdangtich, string vaochibo3, DateTime? ngaybikyluat,
                            string thongtinkyluat, DateTime? ngaylamviecchedocu, string thongtinchedocu)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into dacdiemlichsu (solylich,sothe,xoadanhsach,thoigian," +
                    "taichibo,ketnaplai,ngayvaolan2,taichibo2,nguoigt1,chucvu1,donvi1,nguoigt2,chucvu2, donvi2, " +
                    "ngaychinhthuc2, taichibo3, ngaykhoiphuc, taichibo4, ngayxulyphapluat, thongtinxulyphapluat, " +
                    "ngaylamchedocu,thongtinchedocu) values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15,@16,@17,@18,@19,@20,@21,@22)", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothe;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = bixoaten;
                    if (thoigian != null)
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = thoigian;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = xoataichibo;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = ketnaplai;
                    if(ngayvao != null)
                    {
                        cmd.Parameters.AddWithValue("@7", SqlDbType.Date).Value = ngayvao;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@7", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@8", SqlDbType.NVarChar).Value = vaochibo;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.NVarChar).Value = vaonguoigt1;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = vaochucvu1;
                    cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = vaodonvi1;
                    cmd.Parameters.AddWithValue("@12", SqlDbType.NVarChar).Value = vaonguoigt2;
                    cmd.Parameters.AddWithValue("@13", SqlDbType.NVarChar).Value = vaochucvu2;
                    cmd.Parameters.AddWithValue("@14", SqlDbType.NVarChar).Value = vaodonvi2;
                    if(ngaychinhthuc2 != null)
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = ngaychinhthuc2;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@16", SqlDbType.NVarChar).Value = vaochibo2;
                    if(ngaykhoiphucdangtich != null)
                    {
                        cmd.Parameters.AddWithValue("@17", SqlDbType.Date).Value = ngaykhoiphucdangtich;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@17", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@18", SqlDbType.NVarChar).Value = vaochibo3;
                    if(ngaybikyluat != null)
                    {
                        cmd.Parameters.AddWithValue("@19", SqlDbType.Date).Value = ngaybikyluat;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@19", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@20", SqlDbType.NText).Value = thongtinkyluat;
                    if(ngaylamviecchedocu != null)
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = ngaylamviecchedocu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    cmd.Parameters.AddWithValue("@22", SqlDbType.NText).Value = thongtinchedocu;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update dacdiemlichsu set xoadanhsach=@3,thoigian=@4," +
                    "taichibo=@5,ketnaplai=@6,ngayvaolan2=@7,taichibo2=@8,nguoigt1=@9,chucvu1=@10,donvi1=@11,nguoigt2=@12,chucvu2=@13, donvi2=@14, " +
                    "ngaychinhthuc2=@15, taichibo3=@16, ngaykhoiphuc=@17, taichibo4=@18, ngayxulyphapluat=@19, thongtinxulyphapluat=@20, " +
                    "ngaylamchedocu=@21,thongtinchedocu=@22 where solylich='"+solylich+"' or sothe='"+sothe+"'",con))
                {
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = bixoaten;
                    if (thoigian != null)
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = thoigian;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = xoataichibo;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = ketnaplai;
                    if (ngayvao != null)
                    {
                        cmd.Parameters.AddWithValue("@7", SqlDbType.Date).Value = ngayvao;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@7", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@8", SqlDbType.NVarChar).Value = vaochibo;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.NVarChar).Value = vaonguoigt1;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = vaochucvu1;
                    cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = vaodonvi1;
                    cmd.Parameters.AddWithValue("@12", SqlDbType.NVarChar).Value = vaonguoigt2;
                    cmd.Parameters.AddWithValue("@13", SqlDbType.NVarChar).Value = vaochucvu2;
                    cmd.Parameters.AddWithValue("@14", SqlDbType.NVarChar).Value = vaodonvi2;
                    if (ngaychinhthuc2 != null)
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = ngaychinhthuc2;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@16", SqlDbType.NVarChar).Value = vaochibo2;
                    if (ngaykhoiphucdangtich != null)
                    {
                        cmd.Parameters.AddWithValue("@17", SqlDbType.Date).Value = ngaykhoiphucdangtich;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@17", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@18", SqlDbType.NVarChar).Value = vaochibo3;
                    if (ngaybikyluat != null)
                    {
                        cmd.Parameters.AddWithValue("@19", SqlDbType.Date).Value = ngaybikyluat;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@19", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@20", SqlDbType.NText).Value = thongtinkyluat;
                    if (ngaylamviecchedocu != null)
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = ngaylamviecchedocu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@22", SqlDbType.NText).Value = thongtinchedocu;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        public void Qhng(string optionQuerry, string solylich, string sothe, DateTime? dinuocngoaitu,
         DateTime? dinuocngoaiden,string thongtindinuocngoai,string thamgiatochucnuocngoai,string nguoithannuocngoai)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into qhnuocngoai (solylich,sothe,tungay,denngay," +
                    "thongtinthem,thongtinquanhe,thongtinnguoithan) values (@1,@2,@3,@4,@5,@6,@7)", con))
                {
                    cmd.Parameters.AddWithValue("@1",SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothe;
                    if(dinuocngoaitu != null)
                    {
                        cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = dinuocngoaitu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = DBNull.Value;
                    }

                    if(dinuocngoaiden != null)
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = dinuocngoaiden;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = DBNull.Value;
                    }
                    
                    
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NText).Value = thongtindinuocngoai;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NText).Value = thamgiatochucnuocngoai;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NText).Value = nguoithannuocngoai;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update qhnuocngoai set tungay=@3,denngay=@4," +
                    "thongtinthem=@5,thongtinquanhe=@6,thongtinnguoithan=@7" +
                    " where solylich='"+solylich+"' or sothe='"+sothe+"'", con))
                {
                    if(dinuocngoaitu != null)
                    {
                        cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = dinuocngoaitu;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = DBNull.Value;
                    }
                    if(dinuocngoaiden != null) 
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = dinuocngoaiden;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = DBNull.Value;
                    }
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NText).Value = thongtindinuocngoai;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NText).Value = thamgiatochucnuocngoai;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NText).Value = nguoithannuocngoai;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            
        }

        //form QuanHeGD5
        public void quanheGD(string optionquerry,int id,string solylich,string sothe,string quanhe,string hoten,int namsinh,string thongtin)
        {
            if(optionquerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into qhgiadinh (solylich,sothe,quanhe,hoten,namsinh,thongtin) values (@1,@2,@3,@4,@5,@6)",con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothe;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = quanhe;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = hoten;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = namsinh;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = thongtin;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionquerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update qhgiadinh set quanhe=@3,hoten=@4,namsinh=@5,thongtin=@6 where id="+id, con))
                {
                    
                    cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = quanhe;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = hoten;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = namsinh;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = thongtin;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        // form HoanCanhGiaDinh 6
        public void hoancanhgd(string optionQuerry, string solylich,string sothe,string tongthunhap,string binhquandaunguoi,
         string nhaoduoccap,string dientichnhaoduoccap,string nhaotumua,string dientichnhaotumua,
         string datoduoccap,string datotumua,string hdkinhte,string dientichtrangtrai,string soldthue,
         string taisancogiatricao,string giatri)
        {
            if(optionQuerry == "insert")
            {
                using (SqlCommand cmd = new SqlCommand("insert into hcgiadinh (solylich,sothe,tongthunhapgd,binhquan," +
                    "nhaoduoccap,dientichnhao,nhatumua,dientichnhatumua,datoduoccap," +
                    "dattumua,hoatdongkinhte,dientichdatkinhdoanh,solaodongthue,taisan,giatri) values" +
                    " (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15)", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = solylich;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = sothe;
                    cmd.Parameters.AddWithValue("@3", SqlDbType.VarChar).Value = tongthunhap;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.VarChar).Value = binhquandaunguoi;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = nhaoduoccap;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.VarChar).Value = dientichnhaoduoccap;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = nhaotumua;
                    cmd.Parameters.AddWithValue("@8", SqlDbType.VarChar).Value = dientichnhaotumua;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.VarChar).Value = datoduoccap;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.VarChar).Value = datotumua;
                    cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = hdkinhte;
                    cmd.Parameters.AddWithValue("@12", SqlDbType.VarChar).Value = dientichtrangtrai;
                    cmd.Parameters.AddWithValue("@13", SqlDbType.VarChar).Value = soldthue;
                    cmd.Parameters.AddWithValue("@14", SqlDbType.NText).Value = taisancogiatricao;
                    cmd.Parameters.AddWithValue("@15", SqlDbType.VarChar).Value = giatri;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "update")
            {
                using (SqlCommand cmd = new SqlCommand("update hcgiadinh set tongthunhapgd=@3,binhquan=@4," +
                    "nhaoduoccap=@5,dientichnhao=@6,nhatumua=@7,dientichnhatumua=@8,datoduoccap=@9," +
                    "dattumua=@10,hoatdongkinhte=@11,dientichdatkinhdoanh=@12,solaodongthue=@13,taisan=@14,giatri=@15" +
                    " where solylich='"+solylich+"'", con))
                {
                    cmd.Parameters.AddWithValue("@3", SqlDbType.VarChar).Value = tongthunhap;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.VarChar).Value = binhquandaunguoi;
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = nhaoduoccap;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.VarChar).Value = dientichnhaoduoccap;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = nhaotumua;
                    cmd.Parameters.AddWithValue("@8", SqlDbType.VarChar).Value = dientichnhaotumua;
                    cmd.Parameters.AddWithValue("@9", SqlDbType.VarChar).Value = datoduoccap;
                    cmd.Parameters.AddWithValue("@10", SqlDbType.VarChar).Value = datotumua;
                    cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = hdkinhte;
                    cmd.Parameters.AddWithValue("@12", SqlDbType.VarChar).Value = dientichtrangtrai;
                    cmd.Parameters.AddWithValue("@13", SqlDbType.VarChar).Value = soldthue;
                    cmd.Parameters.AddWithValue("@14", SqlDbType.NText).Value = taisancogiatricao;
                    cmd.Parameters.AddWithValue("@15", SqlDbType.VarChar).Value = giatri;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }


        // phuong thuc them dattagrid view
        public void addDtgForm2(DataGridView dtg)
        {
            con.Open();

            for (int i = 0; i < dtg.Rows.Count; i++)
                {
                using (SqlCommand cmd = new SqlCommand("insert into quatrinhhoatdong (solylich,sothe,tungay,denngay,lamgi,chucvu,dvcongtac)" +
                    " values (@1,@2,@3,@4,@5,@6,@7)", con))
                {
                    cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = dtg.Rows[i].Cells[1].Value;
                    cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = dtg.Rows[i].Cells[2];
                    cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = Convert.ToDateTime(dtg.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = Convert.ToDateTime(dtg.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = dtg.Rows[i].Cells[5].Value;
                    cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = dtg.Rows[i].Cells[6].Value;
                    cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = dtg.Rows[i].Cells[7].Value;

                    
                    cmd.ExecuteNonQuery();
                    
                }
                    

                }
            con.Close();



        }

        public void thongke(Label lb, Label lb1, Label lb2, Label lb3, Label lb4, string combobox)
        {         
            if(combobox == "Tất cả")
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv", con))
                {
                    con.Open();
                    lb.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where  gioitinh=N'Nam'", con))
                {
                    con.Open();
                    lb1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where gioitinh=N'Nữ'", con))
                {
                    con.Open();
                    lb2.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where ngaychinhthuc is not null ", con))
                {
                    con.Open();
                    lb3.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where ngaychinhthuc is null", con))
                {
                    con.Open();
                    lb4.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo=N'"+combobox+"'", con))
                {
                    con.Open();
                    lb.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo = N'" + combobox + "' and gioitinh=N'Nam'", con))
                {
                    con.Open();
                    lb1.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo = N'" + combobox + "' and gioitinh=N'Nữ'", con))
                {
                    con.Open();
                    lb2.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where ngaychinhthuc is not null and taichibo = N'" + combobox + "'", con))
                {
                    con.Open();
                    lb3.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where ngaychinhthuc is null and taichibo = N'" + combobox + "'", con))
                {
                    con.Open();
                    lb4.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
        }

        public void thongkeTuoi(Label lb, Label lb1, Label lb2, Label lb3, Label lb4, string combobox)
        {
            string str = "";
            
            for(int i = 0; i < combobox.Length; i++)
            {
                if (char.IsDigit(combobox[i]))
                {
                    str += combobox[i];                   
                }
            }
            string start = str.Substring(0, 2);
            string end = str.Substring(2, 2);


            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >= "+ start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end, con))
            {
                con.Open();
                lb.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and gioitinh=N'Nam'", con))
            {
                con.Open();
                lb1.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and gioitinh=N'Nữ'", con))
            {
                con.Open();
                lb2.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }

            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and ngaychinhthuc is not null ", con))
            {
                con.Open();
                lb3.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and ngaychinhthuc is null", con))
            {
                con.Open();
                lb4.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }       

        }

        public void getNameChiBoOfAcc(string username,TextBox t)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select ten from chibo inner join taikhoan on ma = chibo where tentk='"+ username+"'", con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        t.Text = read.GetValue(0).ToString();
                    }
                }
            }
            con.Close();
        }
        public string getNameChiBoOfAcc1(string username)
        {
            con.Open();
            string t = "";
            using (SqlCommand cmd = new SqlCommand("select ten from chibo inner join taikhoan on ma = chibo where tentk='" + username + "'", con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        t += read.GetValue(0).ToString();
                    }
                }
            }
            con.Close();
            return t;
        }

        public string getQuyen4File(string username)
        {
            con.Open();
            string a = "";
            using (SqlCommand cmd = new SqlCommand("select quyentruycap from taikhoan where tentk='" + username + "'", con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        a = read.GetValue(0).ToString();
                    }
                }


            }
            con.Close();
            return a;
        }
        public void getQuyen(string username,TextBox t)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select quyentruycap from taikhoan where tentk='" + username + "'", con))
            {
                using (SqlDataReader read= cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        t.Text = read.GetValue(0).ToString();
                    }
                }

                
            }
            con.Close();
        }

        public void createAdminAcc(string acc, string pwd)
        {
            using (SqlCommand cmd = new SqlCommand("insert into taikhoan (tentk,matkhau,chibo,quyentruycap) values (@1,@2,@3,@4)", con))
            {
                cmd.Parameters.AddWithValue("@1",SqlDbType.NVarChar).Value = acc;
                cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = pwd;
                cmd.Parameters.AddWithValue("@3", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = "admin";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string getTextCheckBox(string tbl,string col,string sll)
        {
            string a ="";
            using (SqlCommand cmd = new SqlCommand("select "+col+" from "+tbl+" where solylich='"+sll+"'", con))
            {
                
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    a = read.GetString(0);
                }
                con.Close();
            }
            return a;
        }
        

        public void sendfile(string nguoigui,string chibo,string quyennguoigui,DateTime ngaygui,string quyennguoinhan, string nguoinhan,string tieude,string noidung,byte[] f,string tenfile,string guituchibo)
        {
            using (SqlCommand cmd = new SqlCommand("insert into guifile (tkgui,chibo,quyentkgui,tknhan,ngaygui,quyentknhan,tieude,noidung,teptin,tenfile,chibotkgui) values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11)", con))
            {
                cmd.Parameters.AddWithValue("@1", SqlDbType.NVarChar).Value = nguoigui;
                cmd.Parameters.AddWithValue("@2", SqlDbType.NVarChar).Value = chibo;
                cmd.Parameters.AddWithValue("@3", SqlDbType.VarChar).Value = quyennguoigui;
                cmd.Parameters.AddWithValue("@4", SqlDbType.NVarChar).Value = nguoinhan;
                cmd.Parameters.AddWithValue("@5", SqlDbType.NVarChar).Value = ngaygui;
                cmd.Parameters.AddWithValue("@6", SqlDbType.NVarChar).Value = quyennguoinhan;
                cmd.Parameters.AddWithValue("@7", SqlDbType.NVarChar).Value = tieude;
                cmd.Parameters.AddWithValue("@8", SqlDbType.NText).Value = noidung;
                cmd.Parameters.AddWithValue("@9", SqlDbType.VarBinary).Value = f;
                cmd.Parameters.AddWithValue("@10", SqlDbType.NVarChar).Value = tenfile;
                cmd.Parameters.AddWithValue("@11", SqlDbType.NVarChar).Value = guituchibo;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public void getallAccToRickTextBox(CheckedListBox t)
        {
            con.Open();
            List<string> ar = new List<string>();
            using (SqlCommand cmd = new SqlCommand("select tentk from taikhoan where quyentruycap !='admin'",con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        t.Items.Add(read.GetString(0));
                        
                    }
                }
            }
            
            con.Close();
        }
        public void getallCBToRickTextBox(CheckedListBox t)
        {
            con.Open();
            List<string> ar = new List<string>();
            using (SqlCommand cmd = new SqlCommand("select ten from chibo", con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        t.Items.Add(read.GetString(0));

                    }
                }
            }

            con.Close();
        }

        public List<string> getaccOfChiBo(string chibo)
        {
            con.Open();
            List<string> l = new List<string>();
            using (SqlCommand cmd = new SqlCommand("select tentk from taikhoan inner join chibo on chibo=ma where ten=N'"+chibo+"'", con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        l.Add(read.GetString(0));

                    }
                }
            }
            con.Close();
            return l;
        }

        public void rfAccDataGrid4File(DataGridView dtg, string tbl,string tkgui=null,string tknhan=null,string ft=null )
        {
            using (con)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select id,tkgui,quyentkgui,chibotkgui,tknhan,quyentknhan,chibo,tieude,noidung,tenfile,ngaygui from " + tbl + " where tkgui='"+tkgui+"' or tknhan='"+tknhan+"' "+ft+" order by ngaygui desc", con))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    dtg.DataSource = dtb;
                }
            }
        }

        public void downloadFileDtg1(int id,string opt=null)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select teptin,tenfile,tkgui,chibotkgui from guifile where id="+id, con))
                {
                    SqlDataReader read = cmd.ExecuteReader();
                   
                        if (read.Read())
                        {
                        byte[] buffer = (byte[])read[0];
                        string filename = read.GetString(1);
                        string tkgui = read.GetString(2);
                        string chibotkgui = read.GetString(3);
                        FileStream fs = new FileStream(GetDownloadFolderPath()+"\\"+id.ToString()+ "-" + tkgui+"-"+chibotkgui+ "-" + filename, FileMode.Create);
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Close();
                        }
                                      
                }
                con.Close();
            
        }
        public void downloadFileDtg2(int id, string opt = null)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);


            con.Open();
            using (SqlCommand cmd = new SqlCommand("select teptin,tenfile,tknhan,chibo from guifile where id=" + id, con))
            {
                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                {
                    byte[] buffer = (byte[])read[0];
                    string filename = read.GetString(1);
                    string tkgui = read.GetString(2);
                    string chibotkgui = read.GetString(3).Trim();
                    FileStream fs = new FileStream(GetDownloadFolderPath() + "\\" + id.ToString() + "-" + tkgui + "-" + chibotkgui + "-" + filename, FileMode.Create);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                }

            }
            con.Close();

        }
        string GetDownloadFolderPath()
        {
            return Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();
        }
        
        public void deleteFile(int id)
        {
            
            using (SqlCommand cmd = new SqlCommand("delete from guifile where id=" + id, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void dtgFilterForFile(DataGridView dtg,string col,string text,string tbl,string tkgui=null,string tknhan=null,string ft=null)
        {
            using (con)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select id,tkgui,quyentkgui,chibotkgui,tknhan,quyentknhan,chibo,tieude,noidung,tenfile,ngaygui from " + tbl + " where tkgui='" + tkgui + "' or tknhan='" + tknhan + "' order by ngaygui desc", con))
                {
                    DataTable dtb = new DataTable();
                    sda.Fill(dtb);
                    dtb.DefaultView.RowFilter =col + " like '%"+text+"%'" ;
                    dtg.DataSource = dtb;
                }
            }
        }
        public void exportExcel()
        {
            
            SaveFileDialog sdf = new SaveFileDialog();
            string filePath = "";
            sdf.Filter = "Excel Files| *.xls; *.xlsx; *.xlsm";
            
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    pck.Workbook.Properties.Author = "By ToAnh";
                    pck.Workbook.Properties.Title = "Báo cáo";
                    pck.Workbook.Worksheets.Add("Thông tin Đảng viên");
                    pck.Workbook.Worksheets.Add("Quá trình hoạt động");
                    pck.Workbook.Worksheets.Add("Đào tạo chuyên môn");
                    pck.Workbook.Worksheets.Add("Quan hệ gia đình");
                    ExcelWorksheet ws1 = pck.Workbook.Worksheets[0];
                    ExcelWorksheet ws2 = pck.Workbook.Worksheets[1];
                    ExcelWorksheet ws3 = pck.Workbook.Worksheets[2];
                    ExcelWorksheet ws4 = pck.Workbook.Worksheets[3];

                    ws1.Cells[1,1].Value = "Thông tin cơ bản";
                    using (SqlCommand cmd = new SqlCommand("", con))
                    {
                        con.Open();
                        using(SqlDataReader read = cmd.ExecuteReader())
                        {

                        }
                        con.Close();
                    }

                    byte[] bin = pck.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất file thành công. Đường dẫn file: " + filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }            
        }

        public void addCol(DataTable table)
        {
            table.Columns.Add("Số lý lịch");
            table.Columns.Add("Số thẻ");
            table.Columns.Add("Tên Đảng Viên");
            table.Columns.Add("Giới tính");
            table.Columns.Add("Tên khai sinh");
            table.Columns.Add("Ngày sinh");
            table.Columns.Add("Nơi sinh");
            table.Columns.Add("Quê quán");
            table.Columns.Add("Nơi thường trú");
            table.Columns.Add("Nơi tạm trú");
            table.Columns.Add("Dân tộc");
            table.Columns.Add("Tôn giáo");
            table.Columns.Add("Thành phần gia đình");
            table.Columns.Add("Nghề hiện tại");
            table.Columns.Add("Ngày vào Đảng");
            table.Columns.Add("Tại chi bộ");
            table.Columns.Add("Người giới thiệu 1");
            table.Columns.Add("Chức vụ, đơn vị 1");
            table.Columns.Add("Người giới thiệu 2");
            table.Columns.Add("Chức vụ, đơn vị 2");
            table.Columns.Add("Ngày cấp có thẩm quyền");
            table.Columns.Add("Ngày chính thức");
            table.Columns.Add("Tại chi bộ (Chính thức)");
            table.Columns.Add("Ngày tuyển dụng");
            table.Columns.Add("Cơ quan tuyển dụng");
            table.Columns.Add("Ngày vào Đoàn");
            table.Columns.Add("Tham gia tổ chức xã hội");
            table.Columns.Add("Ngày nhập ngũ");
            table.Columns.Add("Ngày xuất ngũ");
            table.Columns.Add("Trình độ hiện tại");
            table.Columns.Add("Giáo dục phổ thông");
            table.Columns.Add("Giáo dục nghề nghiệp");
            table.Columns.Add("Giáo dục Đại học");
            table.Columns.Add("Giáo dục sau Đại học");
            table.Columns.Add("Học vị");
            table.Columns.Add("Học hàm");
            table.Columns.Add("Lý luận chính trị");
            table.Columns.Add("Ngoại ngữ");
            table.Columns.Add("Tin học");
            table.Columns.Add("Tình trạng sức khỏe");
            table.Columns.Add("Thương binh");
            table.Columns.Add("Gia đình");
            table.Columns.Add("Chứng minh nhân dân");
            table.Columns.Add("Căn cước");
            table.Columns.Add("Miễn công tác ngày");
            table.Columns.Add("Khen thưởng");
            table.Columns.Add("Huy hiệu");
            table.Columns.Add("Danh hiệu");
            table.Columns.Add("Kỷ luật");
            table.Columns.Add("Bị xóa khỏi danh sách Đảng viên");
            table.Columns.Add("Thời gian");
            table.Columns.Add("Tại chi bộ (Bị xóa)");
            table.Columns.Add("Được kết nạp lại");
            table.Columns.Add("Ngày vào");
            table.Columns.Add("Tại chi bộ (Kết nạp lại)");
            table.Columns.Add("Người giới thiệu thứ 1");
            table.Columns.Add("Chức vụ người giới thiệu 1");
            table.Columns.Add("Đơn vị người giới thiệu 1");
            table.Columns.Add("Người giới thiệu thứ 2");
            table.Columns.Add("Chức vụ người giới thiệu 2");
            table.Columns.Add("Đơn vị người giới thiệu 2");
            table.Columns.Add("Ngày chính thức lần 2");
            table.Columns.Add("Tại chi bộ (Chính thức lần 2)");
            table.Columns.Add("Ngày khôi phục Đảng tịch");
            table.Columns.Add("Tại chi bộ (Khôi phục)");
            table.Columns.Add("Ngày xử lý theo pháp luật");
            table.Columns.Add("Thông tin xử lý theo pháp luật");
            table.Columns.Add("Ngày làm việc trong chế độ cũ");
            table.Columns.Add("Thông tin chế độ cũ");
            table.Columns.Add("Từ ngày");
            table.Columns.Add("Đến ngày");
            table.Columns.Add("Thông tin thêm");
            table.Columns.Add("Thông tin quan hệ");
            table.Columns.Add("Thông tin người thân");

            table.Columns.Add("Tổng thu nhập gia đình");
            table.Columns.Add("Bình quân");
            table.Columns.Add("Nhà ở (được cấp,được thuê,loại nhà)");
            table.Columns.Add("Diện tích(m²)");
            table.Columns.Add("Nhà ở (tự mua,tự xây,loại nhà)");
            table.Columns.Add("Diện tích( m²)");
            table.Columns.Add("Đất được cấp(m²)");
            table.Columns.Add("Đất tự mua(m²)");
            table.Columns.Add("Hoạt động kinh tế");
            table.Columns.Add("Diện tích đất kinh doanh (ha)");
            table.Columns.Add("Số lao động thuê mướn");
            table.Columns.Add("Tài sản trên 50tr");
            table.Columns.Add("Giá trị");
        }

        public void alterDatatable(DataTable table,string chibo,string q=null,string q1=null,string q2=null)
        {
            
            addCol(table);
            if (chibo != "Tất cả")
            {
                try
                {
                    Task.Run(() =>
                    {
                        string querry = "select * from ttcbDv inner join daotaochuyenmon2 on ttcbDv.solylich = daotaochuyenmon2.solylich inner join dacdiemlichsu on ttcbDv.solylich = dacdiemlichsu.solylich " +
                            "inner join qhnuocngoai on ttcbDv.solylich = qhnuocngoai.solylich inner join hcgiadinh on ttcbDv.solylich = hcgiadinh.solylich where taichibo1=N'"+chibo+"'"+q+q1+q2;
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(querry, con))
                        {
                            using (SqlDataReader read = cmd.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    table.Rows.Add(new object[] {
                                    read.GetValue(0),
                                    read.GetValue(1),
                                    read.GetValue(2),
                                    read.GetValue(3),
                                    read.GetValue(4),
                                    read.GetValue(5),
                                    read.GetValue(6),
                                    read.GetValue(7),
                                    read.GetValue(8),
                                    read.GetValue(9),
                                    read.GetValue(10),
                                    read.GetValue(11),
                                    read.GetValue(12),
                                    read.GetValue(13),
                                    read.GetValue(14),
                                    read.GetValue(15),
                                    read.GetValue(16),
                                    read.GetValue(17),
                                    read.GetValue(18),
                                    read.GetValue(19),
                                    read.GetValue(20),
                                    read.GetValue(21),
                                    read.GetValue(22),
                                    read.GetValue(23),
                                    read.GetValue(24),
                                    read.GetValue(25),
                                    read.GetValue(26),
                                    read.GetValue(27),
                                    read.GetValue(28),
                                    read.GetValue(29),
                                    read.GetValue(30),
                                    read.GetValue(31),
                                    read.GetValue(32),
                                    read.GetValue(33),
                                    read.GetValue(34),
                                    read.GetValue(35),
                                    read.GetValue(36),
                                    read.GetValue(37),
                                    read.GetValue(38),
                                    read.GetValue(39),
                                    read.GetValue(40),
                                    read.GetValue(41),
                                    read.GetValue(42),
                                    read.GetValue(43),
                                    read.GetValue(44),
                                    read.GetValue(47),
                                    read.GetValue(48),
                                    read.GetValue(49),
                                    read.GetValue(50),
                                    read.GetValue(53),
                                    read.GetValue(54),
                                    read.GetValue(55),
                                    read.GetValue(56),
                                    read.GetValue(57),
                                    read.GetValue(58),
                                    read.GetValue(59),
                                    read.GetValue(60),
                                    read.GetValue(61),
                                    read.GetValue(62),
                                    read.GetValue(63),
                                    read.GetValue(64),
                                    read.GetValue(65),
                                    read.GetValue(66),
                                    read.GetValue(67),
                                    read.GetValue(68),
                                    read.GetValue(69),
                                    read.GetValue(70),
                                    read.GetValue(71),
                                    read.GetValue(72),
                                    read.GetValue(75),
                                    read.GetValue(76),
                                    read.GetValue(77),
                                    read.GetValue(78),
                                    read.GetValue(79),
                                    read.GetValue(82),
                                    read.GetValue(83),
                                    read.GetValue(84),
                                    read.GetValue(85),
                                    read.GetValue(86),
                                    read.GetValue(87),
                                    read.GetValue(88),
                                    read.GetValue(89),
                                    read.GetValue(90),
                                    read.GetValue(91),
                                    read.GetValue(92),
                                    read.GetValue(93),
                                    read.GetValue(94),
                                    });
                                }

                            }
                        }
                        con.Close();
                        MessageBox.Show("done");
                    });

                }
                catch
                {
                    MessageBox.Show("Vuot qua so luong ");
                }
            }
            else
            {
                
                try
                {
                    Task.Run(() =>
                    {
                        string querry = "select * from ttcbDv inner join daotaochuyenmon2 on ttcbDv.solylich = daotaochuyenmon2.solylich inner join dacdiemlichsu on ttcbDv.solylich = dacdiemlichsu.solylich " +
                            "inner join qhnuocngoai on ttcbDv.solylich = qhnuocngoai.solylich inner join hcgiadinh on ttcbDv.solylich = hcgiadinh.solylich  "+q+q1+q2;
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(querry, con))
                        {
                            using (SqlDataReader read = cmd.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    table.Rows.Add(new object[] {
                                    read.GetValue(0),
                                    read.GetValue(1),
                                    read.GetValue(2),
                                    read.GetValue(3),
                                    read.GetValue(4),
                                    read.GetValue(5),
                                    read.GetValue(6),
                                    read.GetValue(7),
                                    read.GetValue(8),
                                    read.GetValue(9),
                                    read.GetValue(10),
                                    read.GetValue(11),
                                    read.GetValue(12),
                                    read.GetValue(13),
                                    read.GetValue(14),
                                    read.GetValue(15),
                                    read.GetValue(16),
                                    read.GetValue(17),
                                    read.GetValue(18),
                                    read.GetValue(19),
                                    read.GetValue(20),
                                    read.GetValue(21),
                                    read.GetValue(22),
                                    read.GetValue(23),
                                    read.GetValue(24),
                                    read.GetValue(25),
                                    read.GetValue(26),
                                    read.GetValue(27),
                                    read.GetValue(28),
                                    read.GetValue(29),
                                    read.GetValue(30),
                                    read.GetValue(31),
                                    read.GetValue(32),
                                    read.GetValue(33),
                                    read.GetValue(34),
                                    read.GetValue(35),
                                    read.GetValue(36),
                                    read.GetValue(37),
                                    read.GetValue(38),
                                    read.GetValue(39),
                                    read.GetValue(40),
                                    read.GetValue(41),
                                    read.GetValue(42),
                                    read.GetValue(43),
                                    read.GetValue(44),
                                    read.GetValue(47),
                                    read.GetValue(48),
                                    read.GetValue(49),
                                    read.GetValue(50),
                                    read.GetValue(53),
                                    read.GetValue(54),
                                    read.GetValue(55),
                                    read.GetValue(56),
                                    read.GetValue(57),
                                    read.GetValue(58),
                                    read.GetValue(59),
                                    read.GetValue(60),
                                    read.GetValue(61),
                                    read.GetValue(62),
                                    read.GetValue(63),
                                    read.GetValue(64),
                                    read.GetValue(65),
                                    read.GetValue(66),
                                    read.GetValue(67),
                                    read.GetValue(68),
                                    read.GetValue(69),
                                    read.GetValue(70),
                                    read.GetValue(71),
                                    read.GetValue(72),
                                    read.GetValue(75),
                                    read.GetValue(76),
                                    read.GetValue(77),
                                    read.GetValue(78),
                                    read.GetValue(79),
                                    read.GetValue(82),
                                    read.GetValue(83),
                                    read.GetValue(84),
                                    read.GetValue(85),
                                    read.GetValue(86),
                                    read.GetValue(87),
                                    read.GetValue(88),
                                    read.GetValue(89),
                                    read.GetValue(90),
                                    read.GetValue(91),
                                    read.GetValue(92),
                                    read.GetValue(93),
                                    read.GetValue(94),

                                    });
                                }

                            }
                        }
                        con.Close();
                        MessageBox.Show("done");
                    });

                }
                catch
                {
                    MessageBox.Show("Vuot qua so luong ");
                }
            }
        }

        public void alterDatatable1(DataTable table, string combobox, string q = null, string q1 = null, string q2 = null)
        {
            addCol(table);
            string str = "";
            for (int i = 0; i < combobox.Length; i++)
            {
                if (char.IsDigit(combobox[i]))
                {
                    str += combobox[i];
                }
            }
            string start = str.Substring(0, 2);
            string end = str.Substring(2, 2);
            
                try
                {
                    Task.Run(() =>
                    {
                        string querry = "select * from ttcbDv inner join daotaochuyenmon2 on ttcbDv.solylich = daotaochuyenmon2.solylich inner join dacdiemlichsu on ttcbDv.solylich = dacdiemlichsu.solylich " +
                            "inner join qhnuocngoai on ttcbDv.solylich = qhnuocngoai.solylich inner join hcgiadinh on ttcbDv.solylich = hcgiadinh.solylich where DATEDIFF(YY,ngaysinh,GETDATE()) >= " + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end +q+q1+q2;
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(querry, con))
                        {
                            using (SqlDataReader read = cmd.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    table.Rows.Add(new object[] {
                                    read.GetValue(0),
                                    read.GetValue(1),
                                    read.GetValue(2),
                                    read.GetValue(3),
                                    read.GetValue(4),
                                    read.GetValue(5),
                                    read.GetValue(6),
                                    read.GetValue(7),
                                    read.GetValue(8),
                                    read.GetValue(9),
                                    read.GetValue(10),
                                    read.GetValue(11),
                                    read.GetValue(12),
                                    read.GetValue(13),
                                    read.GetValue(14),
                                    read.GetValue(15),
                                    read.GetValue(16),
                                    read.GetValue(17),
                                    read.GetValue(18),
                                    read.GetValue(19),
                                    read.GetValue(20),
                                    read.GetValue(21),
                                    read.GetValue(22),
                                    read.GetValue(23),
                                    read.GetValue(24),
                                    read.GetValue(25),
                                    read.GetValue(26),
                                    read.GetValue(27),
                                    read.GetValue(28),
                                    read.GetValue(29),
                                    read.GetValue(30),
                                    read.GetValue(31),
                                    read.GetValue(32),
                                    read.GetValue(33),
                                    read.GetValue(34),
                                    read.GetValue(35),
                                    read.GetValue(36),
                                    read.GetValue(37),
                                    read.GetValue(38),
                                    read.GetValue(39),
                                    read.GetValue(40),
                                    read.GetValue(41),
                                    read.GetValue(42),
                                    read.GetValue(43),
                                    read.GetValue(44),
                                    read.GetValue(47),
                                    read.GetValue(48),
                                    read.GetValue(49),
                                    read.GetValue(50),
                                    read.GetValue(53),
                                    read.GetValue(54),
                                    read.GetValue(55),
                                    read.GetValue(56),
                                    read.GetValue(57),
                                    read.GetValue(58),
                                    read.GetValue(59),
                                    read.GetValue(60),
                                    read.GetValue(61),
                                    read.GetValue(62),
                                    read.GetValue(63),
                                    read.GetValue(64),
                                    read.GetValue(65),
                                    read.GetValue(66),
                                    read.GetValue(67),
                                    read.GetValue(68),
                                    read.GetValue(69),
                                    read.GetValue(70),
                                    read.GetValue(71),
                                    read.GetValue(72),
                                    read.GetValue(75),
                                    read.GetValue(76),
                                    read.GetValue(77),
                                    read.GetValue(78),
                                    read.GetValue(79),
                                    read.GetValue(82),
                                    read.GetValue(83),
                                    read.GetValue(84),
                                    read.GetValue(85),
                                    read.GetValue(86),
                                    read.GetValue(87),
                                    read.GetValue(88),
                                    read.GetValue(89),
                                    read.GetValue(90),
                                    read.GetValue(91),
                                    read.GetValue(92),
                                    read.GetValue(93),
                                    read.GetValue(94),

                                    });
                                }

                            }
                        }
                        con.Close();
                        MessageBox.Show("done");
                    });

                }
                catch
                {
                    MessageBox.Show("Vuot qua so luong ");
                }
            
            
            
        }

        public void rfDtg4ThongKeChibo(DataGridView dtg,string chibo)
        {
            Task.Run(() =>
            {
                con.Open();
                if(chibo != "Tất cả")
                using (SqlCommand cmd = new SqlCommand("Select solylich,sothe,tendangvien,gioitinh,ngaychinhthuc,taichibo1 from ttcbDv where taichibo1=N'" + chibo + "'", con))
                {
                    DataTable dt = new DataTable();
                    for (int i = 0; i <6; i++)
                    {
                        dt.Columns.Add(i.ToString());
                    }
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dt.Rows.Add(new object[] {
                                read.GetValue(0),
                                read.GetValue(1),
                                read.GetValue(2),
                                read.GetValue(3),read.GetValue(4),read.GetValue(5)
                            });
                        }
                        dtg.Invoke(new Action(() =>
                        {
                            dtg.DataSource = dt;
                            dtg.Columns[0].HeaderText = "Số lý lịch";
                            dtg.Columns[1].HeaderText = "Số thẻ";
                            dtg.Columns[2].HeaderText = "Tên Đảng Viên";
                            dtg.Columns[3].HeaderText = "Giới tính";
                            dtg.Columns[4].HeaderText = "Ngày chính thức";
                            dtg.Columns[5].HeaderText = "Chi bộ";
                        }));
                    }
                }
                
                else
                {
                    using (SqlCommand cmd = new SqlCommand("Select solylich,sothe,tendangvien,gioitinh,ngaychinhthuc,taichibo1 from ttcbDv", con))
                    {
                        DataTable dt1 = new DataTable();
                        for (int i = 0; i < 6; i++)
                        {
                            dt1.Columns.Add(i.ToString());
                        }
                        using (SqlDataReader read = cmd.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                dt1.Rows.Add(new object[] {
                                read.GetValue(0),
                                read.GetValue(1),
                                read.GetValue(2),
                                read.GetValue(3),read.GetValue(4),read.GetValue(5)
                            });
                            }
                            dtg.Invoke(new Action(() =>
                            {
                                dtg.DataSource = dt1;
                                dtg.Columns[0].HeaderText = "Số lý lịch";
                                dtg.Columns[1].HeaderText = "Số thẻ";
                                dtg.Columns[2].HeaderText = "Tên Đảng Viên";
                                dtg.Columns[3].HeaderText = "Giới tính";
                                dtg.Columns[4].HeaderText = "Ngày chính thức";
                                dtg.Columns[5].HeaderText = "Chi bộ";
                            }));
                        }
                    }
                    con.Close();
                }
                });
            
        }
        public void rfDtg4ThongKeDoTuoi(DataGridView dtg, string combobox)
        {
            string str = "";

            for (int i = 0; i < combobox.Length; i++)
            {
                if (char.IsDigit(combobox[i]))
                {
                    str += combobox[i];
                }
            }
            string start = str.Substring(0, 2);
            string end = str.Substring(2, 2);
            
            Task.Run(() => {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select solylich,sothe,tendangvien,gioitinh,ngaysinh from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >= " + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end, con))
                {
                    DataTable dt = new DataTable();
                    for(int i = 0; i < 5; i++)
                    {
                        dt.Columns.Add(i.ToString());
                    }
                    using(SqlDataReader read= cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dt.Rows.Add(new object[]
                            {
                                read.GetValue(0),
                                read.GetValue(1),
                                read.GetValue(2),
                                read.GetValue(3),read.GetValue(4)
                            });
                        }
                        dtg.Invoke(new Action(() =>
                        {
                            dtg.DataSource = dt;
                            dtg.Columns[0].HeaderText = "Số lý lịch";
                            dtg.Columns[1].HeaderText = "Số thẻ";
                            dtg.Columns[2].HeaderText = "Tên Đảng Viên";
                            dtg.Columns[3].HeaderText = "Giới tính";

                            dtg.Columns[4].HeaderText = "Ngày sinh";
                        }));
                    }
                }
                con.Close();
            });
            
        }
    }
}
