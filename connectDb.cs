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
            DataTable dt = new DataTable();
            con.Open();
            
            using (SqlCommand cmd = new SqlCommand("select * from dangvien", con))
            {
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
            }
            con.Close();
            dtg.DataSource = dt;
            
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
                    
                    cmd.Parameters.AddWithValue("@3", SqlDbType.Date).Value = dinuocngoaitu;
                    cmd.Parameters.AddWithValue("@4", SqlDbType.Date).Value = dinuocngoaiden;
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

        public void thongke(DataGridViewCell lb,DataGridViewCell lb1,DataGridViewCell lb2, DataGridViewCell lb3, DataGridViewCell lb4, string combobox)
        {         
            if(combobox == "Tất cả")
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv", con))
                {
                    con.Open();
                    lb.Value = cmd.ExecuteScalar();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where  gioitinh=N'Nam'", con))
                {
                    con.Open();
                    lb1.Value = cmd.ExecuteScalar();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where gioitinh=N'Nữ'", con))
                {
                    con.Open();
                    lb2.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo1!='' ", con))
                {
                    con.Open();
                    lb3.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo1 = ''", con))
                {
                    con.Open();
                    lb4.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo=N'"+combobox+"'", con))
                {
                    con.Open();
                    lb.Value = cmd.ExecuteScalar();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo = N'" + combobox + "' and gioitinh=N'Nam'", con))
                {
                    con.Open();
                    lb1.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo = N'" + combobox + "' and gioitinh=N'Nữ'", con))
                {
                    con.Open();
                    lb2.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo1 != '' and taichibo = N'" + combobox + "'", con))
                {
                    con.Open();
                    lb3.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where taichibo1 = '' and taichibo = N'" + combobox + "'", con))
                {
                    con.Open();
                    lb4.Value = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
        }

        public void thongkeTuoi(DataGridViewCell lb, DataGridViewCell lb1, DataGridViewCell lb2, DataGridViewCell lb3, DataGridViewCell lb4, string combobox)
        {

            string start = "";
            string end = "";
            for(int i = 0; i < combobox.Length; i++)
            {
                if (char.IsDigit(combobox[i]))
                {
                    start += combobox[i];
                    if(start.Length == 2)
                    {
                        end += combobox[i];
                    }
                }
            }


            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >= "+ start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end, con))
            {
                con.Open();
                lb.Value = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and gioitinh=N'Nam'", con))
            {
                con.Open();
                lb1.Value = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and gioitinh=N'Nữ'", con))
            {
                con.Open();
                lb2.Value = cmd.ExecuteScalar().ToString();
                con.Close();
            }

            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and taichibo1!='' ", con))
            {
                con.Open();
                lb3.Value = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            using (SqlCommand cmd = new SqlCommand("select count(*) from ttcbDv where DATEDIFF(YY,ngaysinh,GETDATE()) >=" + start + " and DATEDIFF(YY,ngaysinh,GETDATE()) <= " + end + " and taichibo1 = ''", con))
            {
                con.Open();
                lb4.Value = cmd.ExecuteScalar().ToString();
                con.Close();
            }

        }

        public string getNameChiBoOfAcc(string username)
        {
            string a = "";
            using (SqlCommand cmd = new SqlCommand("select ten from chibo inner join taikhoan on ma = chibo where tentk='"+ username+"'", con))
            {
                con.Open();
                a = cmd.ExecuteScalar().ToString();
             
                con.Close();
            }
            return a;
        }
        public void exportExcel(string url)
        {

        }
    }
}
