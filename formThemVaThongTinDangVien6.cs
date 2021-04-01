﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class formThemVaThongTinDangVien6 : Form
    {
        private string solylich;
        private string sothedangvien;
        public static bool check = false;

        // thong tin co ban 1
        private string tendangdung;
        private string gioitinh;
        private string tenkhaisinh;
        private DateTime ngaysinh;
        private string noisinh;
        private string quequan;
        private string noithuongtru;
        private string noitamtru;
        private string dantoc;
        private string tongiao;
        private string thanhphangd;
        private string nghenghiephiennay;
        private DateTime? ngayvaodang;
        private string taichibo;
        private string nguoigt1;
        private string chucvudonvi1;
        private string nguoigt2;
        private string chucvudonvi2;
        private DateTime? ngaycap;
        private DateTime? ngaychinhthuc;
        private string taichibo2;
        private DateTime? ngayduoctuyendung;
        private string coquantuyendung;
        private DateTime? ngayvaodoan;
        private string thamgiatochucxh;
        private DateTime? ngaynhapngu;
        private DateTime? ngayxuatngu;
        private string trinhdohiennay;
        private string gdphothong;
        private string gdNgheNghiep;
        private string gddaihoc;
        private string gdsaudaihoc;
        private string hocvi;
        private string hocham;
        private string lyluanct;
        private string ngoaingu;
        private string tinhoc;
        private string tinhtrangsuckhoe;
        private string thuongbinhloai;
        private string giadinh;
        private string cmnd;
        private string cancuoccdan;
        private DateTime? mienCtac;

        // dao tao chung 3
        private string khenthuong;
        private string huyhieudang;
        private string danhhieu;
        private string kyluat;


        // dac dien lich su va quan he nuoc ngoai 4
        private string bixoaten;
        private DateTime? thoigian;
        private string xoataichibo;
        private string ketnaplai;
        private DateTime? ngayvao;
        private string vaochibo;
        private string vaonguoigt1;
        private string vaochucvu1;
        private string vaodonvi1;
        private string vaonguoigt2;
        private string vaochucvu2;
        private string vaodonvi2;
        private DateTime? ngaychinhthuc2;
        private string vaochibo2;
        private DateTime? ngaykhoiphucdangtich;
        private string vaochibo3;
        private DateTime? ngaybikyluat;
        private string thongtinkyluat;
        private DateTime? ngaylamviecchedocu;
        private string thongtinchedocu;

        private DateTime? dinuocngoaitu;
        private DateTime? dinuocngoaiden;
        private string thongtindinuocngoai;
        private string thamgiatochucnuocngoai;
        private string nguoithannuocngoai;

        //hoan canh kinh te 6

        private string tongthunhap;
        private string binhquandaunguoi;
        private string nhaoduoccap;
        private string dientichnhaoduoccap;
        private string nhaotumua;
        private string dientichnhaotumua;
        private string datoduoccap;
        private string datotumua;
        private string hdkinhte;
        private string dientichtrangtrai;
        private string soldthue;
        private string taisancogiatricao;
        private string giatri;
        public formThemVaThongTinDangVien6()
        {
            InitializeComponent();
        }

        private void formThemVaThongTinDangVien6_Load(object sender, EventArgs e)
        {
            if(check == true)
            {
                getInfor();
            }
        }
        private void button1_Click(object sender, EventArgs e) // insert sql
        {
            setAllInfor();
            connectDb con = new connectDb();
            if(check == false) {
            DialogResult dr = MessageBox.Show("Bạn có muốn thêm người này vào danh sách Đảng viên không?\n Tên Đảng viên:  " + tendangdung + "\n" +
                "Số lý lịch: " + solylich + "\n" +
                "Số thẻ Đảng viên: " + sothedangvien, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    con.addSllVaSt(dangvien.anh, dangvien.solylich, dangvien.sothedangvien);
                    con.updateDangvien(tendangdung, solylich, sothedangvien);
                    con.TTCBDangVien("insert", solylich, sothedangvien, tendangdung, gioitinh, tenkhaisinh, ngaysinh, noisinh,
                     quequan, noithuongtru, noitamtru, dantoc, tongiao, thanhphangd,
                     nghenghiephiennay, ngayvaodang, taichibo, nguoigt1, chucvudonvi1,
                     nguoigt2, chucvudonvi2, ngaycap, ngaychinhthuc, taichibo2,
                     ngayduoctuyendung, coquantuyendung, ngayvaodoan, thamgiatochucxh,
                     ngaynhapngu, ngayxuatngu, trinhdohiennay, gdphothong, gdNgheNghiep,
                     gddaihoc, gdsaudaihoc, hocvi, hocham, lyluanct, ngoaingu, tinhoc,
                     tinhtrangsuckhoe, thuongbinhloai, giadinh, cmnd, cancuoccdan, mienCtac);

                    con.DaoTaoChung2("insert", solylich, sothedangvien, khenthuong, huyhieudang, danhhieu, kyluat);

                    con.Ddls("insert", solylich, sothedangvien, bixoaten, thoigian,
                                    xoataichibo, ketnaplai, ngayvao, vaochibo,
                                    vaonguoigt1, vaochucvu1, vaodonvi1, vaonguoigt2, vaochucvu2, vaodonvi2,
                                    ngaychinhthuc2, vaochibo2, ngaykhoiphucdangtich, vaochibo3, ngaybikyluat,
                                    thongtinkyluat, ngaylamviecchedocu, thongtinchedocu);
                    con.Qhng("insert", solylich, sothedangvien, dinuocngoaitu,
                        dinuocngoaiden, thongtindinuocngoai, thamgiatochucnuocngoai, nguoithannuocngoai);

                    con.hoancanhgd("insert", solylich, sothedangvien, tongthunhap, binhquandaunguoi,
                                   nhaoduoccap, dientichnhaoduoccap, nhaotumua, dientichnhaotumua,
                                    datoduoccap, datotumua, hdkinhte, dientichtrangtrai, soldthue,
                                    taisancogiatricao, giatri);
                    MessageBox.Show("Thêm đảng viên thành công.\n" +
                        "Tên Đảng viên: " + tendangdung + "\n" +
                        "Số lý lịch: " + solylich + "\n" +
                        "Số thẻ Đảng viên: " + sothedangvien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    uclDangVien uc = new uclDangVien();
                    uc.textBox1.Text = "";
                    uc.textBox2.Text = "";
                    uc.pictureBox1.Image = uc.pictureBox1.InitialImage;

                    this.Close();
                }
            }
            else if(check == true)
            {
                con.addSllVaSt(dangvien.anh, dangvien.solylich, dangvien.sothedangvien);
                con.updateDangvien(tendangdung, solylich, sothedangvien);
                con.TTCBDangVien("update", solylich, sothedangvien, tendangdung, gioitinh, tenkhaisinh, ngaysinh, noisinh,
                 quequan, noithuongtru, noitamtru, dantoc, tongiao, thanhphangd,
                 nghenghiephiennay, ngayvaodang, taichibo, nguoigt1, chucvudonvi1,
                 nguoigt2, chucvudonvi2, ngaycap, ngaychinhthuc, taichibo2,
                 ngayduoctuyendung, coquantuyendung, ngayvaodoan, thamgiatochucxh,
                 ngaynhapngu, ngayxuatngu, trinhdohiennay, gdphothong, gdNgheNghiep,
                 gddaihoc, gdsaudaihoc, hocvi, hocham, lyluanct, ngoaingu, tinhoc,
                 tinhtrangsuckhoe, thuongbinhloai, giadinh, cmnd, cancuoccdan, mienCtac);

                con.DaoTaoChung2("update", solylich, sothedangvien, khenthuong, huyhieudang, danhhieu, kyluat);

                con.Ddls("update", solylich, sothedangvien, bixoaten, thoigian,
                                xoataichibo, ketnaplai, ngayvao, vaochibo,
                                vaonguoigt1, vaochucvu1, vaodonvi1, vaonguoigt2, vaochucvu2, vaodonvi2,
                                ngaychinhthuc2, vaochibo2, ngaykhoiphucdangtich, vaochibo3, ngaybikyluat,
                                thongtinkyluat, ngaylamviecchedocu, thongtinchedocu);
                con.Qhng("update", solylich, sothedangvien, dinuocngoaitu,
                    dinuocngoaiden, thongtindinuocngoai, thamgiatochucnuocngoai, nguoithannuocngoai);

                con.hoancanhgd("update", solylich, sothedangvien, tongthunhap, binhquandaunguoi,
                               nhaoduoccap, dientichnhaoduoccap, nhaotumua, dientichnhaotumua,
                                datoduoccap, datotumua, hdkinhte, dientichtrangtrai, soldthue,
                                taisancogiatricao, giatri);
                MessageBox.Show("Cập nhật đảng viên thành công.\n" +
                    "Tên Đảng viên: " + tendangdung + "\n" +
                    "Số lý lịch: " + solylich + "\n" +
                    "Số thẻ Đảng viên: " + sothedangvien, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                uclDangVien uc = new uclDangVien();
                uc.textBox1.Text = "";
                uc.textBox2.Text = "";
                uc.pictureBox1.Image = uc.pictureBox1.InitialImage;

                this.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void setAllInfor()
        {


            solylich = dangvien.solylich;
            sothedangvien = dangvien.sothedangvien;


            // thong tin co ban 1
            tendangdung = dangvien.tendangdung;
            gioitinh = dangvien.gioitinh;

            tenkhaisinh = dangvien.tenkhaisinh;
            ngaysinh = dangvien.ngaysinh;

            noisinh = dangvien.noisinh;
            quequan = dangvien.quequan;
            noithuongtru = dangvien.noithuongtru;
            noitamtru = dangvien.noitamtru;
            dantoc = dangvien.dantoc;
            tongiao = dangvien.tongiao;
            thanhphangd = dangvien.thanhphangd;
            nghenghiephiennay = dangvien.nghenghiephiennay;

            ngayvaodang = dangvien.ngayvaodang;


            taichibo = dangvien.taichibo;
            nguoigt1 = dangvien.nguoigt1;
            chucvudonvi1 = dangvien.chucvudonvi1;
            nguoigt2 = dangvien.nguoigt2;
            chucvudonvi2 = dangvien.chucvudonvi2;

            ngaycap = dangvien.ngaycap;
            ngaychinhthuc = dangvien.ngaychinhthuc;


            taichibo2 = dangvien.taichibo2;

            ngayduoctuyendung = dangvien.ngayduoctuyendung;


            coquantuyendung = dangvien.coquantuyendung;
            ngayvaodoan = dangvien.ngayvaodoan;



            thamgiatochucxh = dangvien.thamgiatochucxh;

            ngaynhapngu = dangvien.ngaynhapngu;

            ngayxuatngu = dangvien.ngayxuatngu;


            trinhdohiennay = dangvien.trinhdohiennay;
            gdphothong = dangvien.gdphothong;
            gdNgheNghiep = dangvien.gdNgheNghiep;
            gddaihoc = dangvien.gddaihoc;
            gdsaudaihoc = dangvien.gdsaudaihoc;
            hocvi = dangvien.hocvi;
            hocham = dangvien.hocham;
            lyluanct = dangvien.lyluanct;
            ngoaingu = dangvien.ngoaingu;
            tinhoc = dangvien.tinhoc;
            tinhtrangsuckhoe = dangvien.tinhtrangsuckhoe;
            thuongbinhloai = dangvien.thuongbinhloai;

            giadinh = dangvien.giadinh;


            cmnd = dangvien.cmnd;
            cancuoccdan = dangvien.cancuoccdan;

            mienCtac = dangvien.mienCtac;


            //// dao tao chung 3

            khenthuong = dangvien.khenthuong;
            huyhieudang = dangvien.huyhieudang;

            danhhieu = dangvien.danhhieu;
            kyluat = dangvien.kyluat;


            //// dac dien lich su va quan he nuoc ngoai 4
            ///

            bixoaten = dangvien.bixoaten;
            thoigian = dangvien.thoigian;


            xoataichibo = dangvien.xoataichibo;
            ketnaplai = dangvien.ketnaplai;
            ngayvao = dangvien.ngayvao;



            vaochibo = dangvien.vaochibo;
            vaonguoigt1 = dangvien.vaonguoigt1;
            vaochucvu1 = dangvien.vaochucvu1;
            vaodonvi1 = dangvien.vaodonvi1;
            vaonguoigt2 = dangvien.vaonguoigt2;
            vaochucvu2 = dangvien.vaochucvu2;
            vaodonvi2 = dangvien.vaodonvi2;


            ngaychinhthuc2 = dangvien.ngaychinhthuc2;



            vaochibo2 = dangvien.vaochibo2;


            ngaykhoiphucdangtich = dangvien.ngaykhoiphucdangtich;


            vaochibo3 = dangvien.vaochibo3;

            ngaybikyluat = dangvien.ngaybikyluat;


            thongtinkyluat = dangvien.thongtinkyluat;

            ngaylamviecchedocu = dangvien.ngaylamviecchedocu;


            thongtinchedocu = dangvien.thongtinchedocu;

            dinuocngoaitu = dangvien.dinuocngoaitu;


            dinuocngoaiden = dangvien.dinuocngoaiden;


            thongtindinuocngoai = dangvien.thongtindinuocngoai;
            thamgiatochucnuocngoai = dangvien.thamgiatochucnuocngoai;
            nguoithannuocngoai = dangvien.nguoithannuocngoai;

            setInfor();

            // hoan canh gia dinh 6
            tongthunhap = dangvien.tongthunhap;
            binhquandaunguoi = dangvien.binhquandaunguoi;
            nhaoduoccap = dangvien.nhaoduoccap;
            dientichnhaoduoccap = dangvien.dientichnhaoduoccap;
            nhaotumua = dangvien.nhaotumua;
            dientichnhaotumua = dangvien.dientichnhaotumua;
            datoduoccap = dangvien.datoduoccap;
            datotumua = dangvien.datotumua;
            hdkinhte = dangvien.hdkinhte;
            dientichtrangtrai = dangvien.dientichtrangtrai;
            soldthue = dangvien.soldthue;
            taisancogiatricao = dangvien.taisancogiatricao;
            giatri = dangvien.giatri;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1)))
            {
                e.Handled = true;
            }
        }

        private void setInfor()
        {
            dangvien.tongthunhap = this.textBox1.Text;
            dangvien.binhquandaunguoi = this.textBox2.Text;
            dangvien.nhaoduoccap = this.textBox3.Text;
            dangvien.dientichnhaoduoccap = this.textBox4.Text;
            dangvien.nhaotumua = this.textBox5.Text;
            dangvien.dientichnhaotumua = this.textBox6.Text;
            dangvien.datoduoccap = this.textBox7.Text;
            dangvien.datotumua = this.textBox8.Text;
            dangvien.hdkinhte = this.textBox9.Text;
            dangvien.dientichtrangtrai = this.textBox10.Text;
            dangvien.soldthue = this.textBox11.Text;
            dangvien.taisancogiatricao = this.richTextBox1.Text;
            dangvien.giatri = this.textBox12.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien5 f = (formThemVaThongTinDangVien5)Application.OpenForms["formThemVaThongTinDangVien5"];
            if (f == null)
            {
                f = new formThemVaThongTinDangVien5();
                f.AutoScroll = true;
            }
            f.Show();
            this.Hide();
        }

        private void formThemVaThongTinDangVien6_FormClosed(object sender, FormClosedEventArgs e)
        {
            formThemVaThongTinDangVien f1 = new formThemVaThongTinDangVien();
            if(f1 != null)
            {
                f1.Close();
            }
            formThemVaThongTinDangVien2 f2 = new formThemVaThongTinDangVien2();
            if (f2 != null)
            {
                f2.Close();
            }

            formThemVaThongTinDangVien3 f3 = new formThemVaThongTinDangVien3();
            if (f3 != null)
            {
                f3.Close();
            }
            formThemVaThongTinDangVien4 f4 = new formThemVaThongTinDangVien4();
            if (f4 != null)
            {
                f4.Close();
            }
            formThemVaThongTinDangVien5 f5 = new formThemVaThongTinDangVien5(); ;
            if (f5 != null)
            {
                f5.Close();
            }
            formThemVaThongTinDangVien5 f6 = new formThemVaThongTinDangVien5(); ;
            if (f6 != null)
            {
                f6.Close();
            }

        }

        private void getInfor()
        {
            connectDb con = new connectDb();
            con.con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from hcgiadinh where solylich='" + dangvien.solylich + "'", con.con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        textBox1.Text = read.GetString(2);
                        textBox2.Text = read.GetString(3);
                        textBox3.Text = read.GetString(4);
                        textBox4.Text = read.GetString(5);
                        textBox5.Text = read.GetString(6);
                        textBox6.Text = read.GetString(7);
                        textBox7.Text = read.GetString(8);
                        textBox8.Text = read.GetString(9);
                        textBox9.Text = read.GetString(10);
                        textBox10.Text = read.GetString(11);
                        textBox11.Text = read.GetString(12);
                        richTextBox1.Text = read.GetString(13);
                        textBox12.Text = read.GetString(14);
                    }
                }
            }
            con.con.Close();
        }
    }
}