using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class ucHoanCanhGiaDinh6 : UserControl
    {



        public ucHoanCanhGiaDinh6()
        {
            InitializeComponent();
  
        }

        public static bool check = false;

        //thong tin can thiet 0
        private string solylich;
        private string sothedangvien;
        private byte[] anh;

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


        private static ucHoanCanhGiaDinh6 _instance;
        public static ucHoanCanhGiaDinh6 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucHoanCanhGiaDinh6();
                }
                return _instance;
            }
        }
        
        private void ucHoanCanhGiaDinh6_Load(object sender, EventArgs e)
        {
            if(check == true)
            {
                foreach(Control c in groupBox1.Controls)
                {
                    if(c is TextBox)
                    {
                        ((TextBox)c).Text = "";
                    }
                    else if(c is RichTextBox)
                    {
                        ((RichTextBox)c).Text = "";
                    }
                }
            }
        }

        public static formThemVaThongTinDangVien d = new formThemVaThongTinDangVien();
        private void button1_Click(object sender, EventArgs e) // insert sql
        {
            setAllInfor();
            connectDb con = new connectDb();
            DialogResult dr = MessageBox.Show("Bạn có muốn thêm người này vào danh sách Đảng viên không?\n Tên Đảng viên:  "+ tendangdung +"\n" +
                "Số lý lịch: "+solylich+"\n" +
                "Số thẻ Đảng viên: "+sothedangvien,"Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                con.addSllVaSt(anh, tendangdung, solylich, sothedangvien);
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
                    "Số thẻ Đảng viên: " + sothedangvien,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);


                formThemVaThongTinDangVien f = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
                
                f.Close();
                

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "V. QUAN HỆ GIA ĐÌNH";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucQuanHeGD5.Instance);
            ucQuanHeGD5.Instance.Dock = DockStyle.Fill;
            ucQuanHeGD5.Instance.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                dtvttdv.Close();
            }
        }

        public void setAllInfor()
        {

            
            solylich = dangvien.solylich;
            sothedangvien = dangvien.sothedangvien;
            anh = dangvien.anh;

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
            tongiao = dangvien.danhhieu;
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
            

            cmnd = dangvien.cmnd ;
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
            if (!char.IsDigit(e.KeyChar) &&  (e.KeyChar != ',') && (e.KeyChar != (char)Keys.Back) )
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') &&  ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1)))
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


        
    }
}
