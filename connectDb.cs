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
        public void addSllVaSt(byte[] img,string ten,string solylich, string sothe)
        {
            using (SqlCommand cmd = new SqlCommand("insert into dangvien (anhdangvien,tendangvien,solylich,sothe) values (@img,@ten,@sll,@st)", con))
            {
                cmd.Parameters.AddWithValue("@img", SqlDbType.Image).Value = img;
                cmd.Parameters.AddWithValue("@sll", SqlDbType.NVarChar).Value = solylich;
                cmd.Parameters.AddWithValue("@ten", SqlDbType.NVarChar).Value = ten;
                cmd.Parameters.AddWithValue("@st", SqlDbType.NVarChar).Value = sothe;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // form uclTTCBDangVien
        public void TTCBDangVien(string optionQuerry,string solylich, string sothedangvien, string tendangdung,string gioitinh,
            string tenkhaisinh,DateTime? ngaysinh,string noisinh,
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
                "@26,@27,@28,@29,@30,@31,@32,@33,@34,@35,@36,@37,@38,@39,@40,@41,@42,@43,@44,@45,)", con))
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
                    cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = ngayvaodang;
                    cmd.Parameters.AddWithValue("@16", SqlDbType.NVarChar).Value = taichibo;
                    cmd.Parameters.AddWithValue("@17", SqlDbType.NVarChar).Value = nguoigt1;
                    cmd.Parameters.AddWithValue("@18", SqlDbType.NVarChar).Value = chucvudonvi1;
                    cmd.Parameters.AddWithValue("@19", SqlDbType.NVarChar).Value = nguoigt2;
                    cmd.Parameters.AddWithValue("@20", SqlDbType.NVarChar).Value = chucvudonvi2;
                    cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = ngaycap;
                    cmd.Parameters.AddWithValue("@22", SqlDbType.Date).Value = ngaychinhthuc;
                    cmd.Parameters.AddWithValue("@23", SqlDbType.NVarChar).Value = taichibo2;
                    cmd.Parameters.AddWithValue("@24", SqlDbType.Date).Value = ngayduoctuyendung;
                    cmd.Parameters.AddWithValue("@25", SqlDbType.NVarChar).Value = coquantuyendung;
                    cmd.Parameters.AddWithValue("@26", SqlDbType.Date).Value = ngayvaodoan;
                    cmd.Parameters.AddWithValue("@27", SqlDbType.NVarChar).Value = thamgiatochucxh;
                    cmd.Parameters.AddWithValue("@28", SqlDbType.Date).Value = ngaynhapngu;
                    cmd.Parameters.AddWithValue("@29", SqlDbType.Date).Value = ngayxuatngu;
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
                    cmd.Parameters.AddWithValue("@45", SqlDbType.Date).Value = mienCtac;

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
                    cmd.Parameters.AddWithValue("@15", SqlDbType.Date).Value = ngayvaodang;
                    cmd.Parameters.AddWithValue("@16", SqlDbType.NVarChar).Value = taichibo;
                    cmd.Parameters.AddWithValue("@17", SqlDbType.NVarChar).Value = nguoigt1;
                    cmd.Parameters.AddWithValue("@18", SqlDbType.NVarChar).Value = chucvudonvi1;
                    cmd.Parameters.AddWithValue("@19", SqlDbType.NVarChar).Value = nguoigt2;
                    cmd.Parameters.AddWithValue("@20", SqlDbType.NVarChar).Value = chucvudonvi2;
                    cmd.Parameters.AddWithValue("@21", SqlDbType.Date).Value = ngaycap;
                    cmd.Parameters.AddWithValue("@22", SqlDbType.Date).Value = ngaychinhthuc;
                    cmd.Parameters.AddWithValue("@23", SqlDbType.NVarChar).Value = taichibo2;
                    cmd.Parameters.AddWithValue("@24", SqlDbType.Date).Value = ngayduoctuyendung;
                    cmd.Parameters.AddWithValue("@25", SqlDbType.NVarChar).Value = coquantuyendung;
                    cmd.Parameters.AddWithValue("@26", SqlDbType.Date).Value = ngayvaodoan;
                    cmd.Parameters.AddWithValue("@27", SqlDbType.NVarChar).Value = thamgiatochucxh;
                    cmd.Parameters.AddWithValue("@28", SqlDbType.Date).Value = ngaynhapngu;
                    cmd.Parameters.AddWithValue("@29", SqlDbType.Date).Value = ngayxuatngu;
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
                    cmd.Parameters.AddWithValue("@45", SqlDbType.Date).Value = mienCtac;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else if(optionQuerry == "select")
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from ttcbDv where solylich='"+solylich+"' or sothe='"+sothedangvien+"'", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        tendangdung = read.GetString(2);
                        gioitinh = read.GetString(3);
                        tenkhaisinh = read.GetString(4);

                        if (read.GetDateTime(5) != null)
                        {
                            ngaysinh = read.GetDateTime(5);
                        }
                        else
                        {
                            ngaysinh = DateTime.Now;
                        }
                        noisinh = read.GetString(6);
                        quequan = read.GetString(7);
                        noithuongtru = read.GetString(8);
                        noitamtru = read.GetString(9);
                        dantoc = read.GetString(10);
                        tongiao = read.GetString(11);
                        thanhphangd = read.GetString(12);
                        nghenghiephiennay = read.GetString(13);

                        if(read.GetDateTime(14) != null)
                        {
                            ngayvaodang = read.GetDateTime(14);
                        }
                        else
                        {
                            ngayvaodang = DateTime.Now;
                        }

                        taichibo = read.GetString(15);
                        nguoigt1 = read.GetString(16);
                        chucvudonvi1 = read.GetString(17);
                        nguoigt2 = read.GetString(18);
                        chucvudonvi2 = read.GetString(19);

                        if(read.GetDateTime(20) != null)
                        {
                            ngaycap = read.GetDateTime(20);
                        }
                        else
                        {
                            ngaycap = DateTime.Now;
                        }

                        if (read.GetDateTime(21) != null)
                        {
                            ngaychinhthuc = read.GetDateTime(21);
                        }
                        else
                        {
                            ngaychinhthuc = DateTime.Now;
                        }


                        taichibo2 = read.GetString(22);
                        
                        if(read.GetDateTime(23) != null)
                        {
                            ngayduoctuyendung = read.GetDateTime(23);
                        }
                        else
                        {
                            ngayduoctuyendung = DateTime.Now;
                        }

                        coquantuyendung = read.GetString(24);

                        if (read.GetDateTime(25) != null)
                        {
                            ngayvaodoan = read.GetDateTime(25);
                        }
                        else
                        {
                            ngayvaodoan = DateTime.Now;
                        }

                        thamgiatochucxh = read.GetString(26);

                        if (read.GetDateTime(27) != null)
                        {
                            ngaynhapngu = read.GetDateTime(27);
                        }
                        else
                        {
                            ngaynhapngu = DateTime.Now;
                        }

                        if (read.GetDateTime(28) != null)
                        {
                            ngayxuatngu = read.GetDateTime(28);
                        }
                        else
                        {
                            ngayxuatngu = DateTime.Now;
                        }


                        trinhdohiennay = read.GetString(29);
                        gdphothong = read.GetString(30);
                        gdNgheNghiep = read.GetString(31);
                        gddaihoc = read.GetString(32);
                        gdsaudaihoc = read.GetString(33);
                        hocvi = read.GetString(34);
                        hocham = read.GetString(35);
                        lyluanct = read.GetString(36);
                        ngoaingu = read.GetString(37);
                        tinhoc = read.GetString(38);
                        tinhtrangsuckhoe = read.GetString(39);
                        thuongbinhloai = read.GetString(40);
                        giadinh = read.GetString(41);
                        cmnd = read.GetString(42);
                        cancuoccdan = read.GetString(43);
                        
                        if(read.GetDateTime(44) != null)
                        {
                            mienCtac = read.GetDateTime(44);
                        }
                        else
                        {
                            mienCtac = DateTime.Now;
                        }
                        
                    }
                }
                con.Close();
            }
            
        }

        // form quatrinhhoatdong2

        public void quatrinhhoatdong()
        {

        }

        // form UCDaoTaoCHung3

        public void DaoTaoChung1()
        {
            
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
            else if(optionQuerry == "select")
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from daotaochuyenmon2 where solylich='" + solylich + "' or sothe='" + sothedangvien + "'", con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        khenthuong = read.GetString(2);
                        huyhieudang = read.GetString(3);
                        danhhieu = read.GetString(4);
                        kyluat = read.GetString(5);
                    }
                }
                con.Close();
            }
            
        }

        // form DdlsVaQhng4

        public void addDdls()
        {

        }

        public void addQhng()
        {

        }

    }
}
