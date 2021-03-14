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

        //thong tin can thiet 0
        private string solylich;
        private string sothedangvien;
        private byte[] anh;

        // thong tin co ban 1
        private string tendangdung;
        private string gioitinh;
        private string tenkhaisinh;
        private DateTime? ngaysinh;
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
            
        }

        private void button1_Click(object sender, EventArgs e) // insert sql
        {
            setInfor();
            connectDb con = new connectDb();       
            con.addSllVaSt(anh, tendangdung, solylich, sothedangvien);
            con.TTCBDangVien("insert",solylich, sothedangvien, tendangdung, gioitinh, tenkhaisinh, ngaysinh, noisinh,
             quequan, noithuongtru, noitamtru, dantoc, tongiao, thanhphangd,
             nghenghiephiennay, ngayvaodang, taichibo, nguoigt1, chucvudonvi1,
             nguoigt2, chucvudonvi2, ngaycap, ngaychinhthuc, taichibo2,
             ngayduoctuyendung, coquantuyendung, ngayvaodoan, thamgiatochucxh,
             ngaynhapngu, ngayxuatngu, trinhdohiennay, gdphothong, gdNgheNghiep,
             gddaihoc, gdsaudaihoc, hocvi, hocham, lyluanct, ngoaingu, tinhoc,
             tinhtrangsuckhoe, thuongbinhloai, giadinh, cmnd, cancuoccdan, mienCtac);

            con.DaoTaoChung2("insert",solylich, sothedangvien, khenthuong, huyhieudang, danhhieu, kyluat);
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

        public void setInfor()
        {
            uclDangVien dv = new uclDangVien();
            solylich = dv.textBox1.Text;
            sothedangvien = dv.textBox2.Text;
            ImageConverter converter = new ImageConverter();
            anh = (byte[])converter.ConvertTo(dv.pictureBox1.Image,typeof(byte[]));

            // thong tin co ban 1
            ucTTCBDangVien ttcb = new ucTTCBDangVien();
            tendangdung = ttcb.textBox1.Text;
            if (ttcb.radioButton1.Checked)
            {
                gioitinh = ttcb.radioButton1.Text;
            }
            else
                gioitinh = ttcb.radioButton2.Text;
            
            tenkhaisinh = ttcb.textBox2.Text;

            if (ttcb.dt1 == true)
            {
                ngaysinh = ttcb.dateTimePicker1.Value;
            }
            else
            {
                ngaysinh = null;
            }
                
            noisinh = ttcb.textBox3.Text;
            quequan = ttcb.textBox4.Text;
            noithuongtru = ttcb.textBox5.Text;
            noitamtru = ttcb.textBox6.Text;
            dantoc = ttcb.comboBox2.Text;
            tongiao = ttcb.comboBox1.Text;
            thanhphangd = ttcb.textBox7.Text;
            nghenghiephiennay = ttcb.textBox8.Text;

            if (ttcb.dt2 == true)
            {
                ngayvaodang = ttcb.dateTimePicker2.Value;
            }
            else
            {
                ngayvaodang = null;
            }
            
            taichibo = ttcb.textBox9.Text;
            nguoigt1 = ttcb.textBox10.Text;
            chucvudonvi1 = ttcb.textBox11.Text;
            nguoigt2 = ttcb.textBox12.Text;
            chucvudonvi2 = ttcb.textBox13.Text;

            if (ttcb.dt3 == true)
            {
                ngaycap = ttcb.dateTimePicker3.Value;
            }
            else
            {
                ngaycap = null;
            }

            if (ttcb.dt4 == true)
            {
                ngaychinhthuc = ttcb.dateTimePicker4.Value;
            }
            else
            {
                ngaychinhthuc = null;
            }
            
            taichibo2 = ttcb.textBox14.Text;

            if (ttcb.dt5 == true)
            {
                ngayduoctuyendung = ttcb.dateTimePicker5.Value;
            }
            else
            {
                ngayduoctuyendung = null;
            }
            
            coquantuyendung = ttcb.textBox15.Text;

            if (ttcb.dt6 == true)
            {
                ngayvaodoan = ttcb.dateTimePicker6.Value;
            }
            else
            {
                ngayvaodoan = null;
            }
            

            thamgiatochucxh = ttcb.textBox16.Text;

            if (ttcb.dt7 == true)
            {
                ngaynhapngu = ttcb.dateTimePicker7.Value;
            }
            else
            {
                ngaynhapngu = null;
            }

            if (ttcb.dt8 == true)
            {
                ngayxuatngu = ttcb.dateTimePicker8.Value;
            }
            else
            {
                ngayxuatngu = null;
            }

            trinhdohiennay = ttcb.textBox17.Text;
            gdphothong = ttcb.textBox18.Text;
            gdNgheNghiep = ttcb.textBox19.Text;
            gddaihoc = ttcb.textBox20.Text;
            gdsaudaihoc = ttcb.textBox30.Text;
            hocvi = ttcb.textBox21.Text;
            hocham = ttcb.textBox22.Text;
            lyluanct = ttcb.textBox23.Text;
            ngoaingu = ttcb.textBox24.Text;
            tinhoc = ttcb.textBox25.Text;
            tinhtrangsuckhoe = ttcb.textBox26.Text;
            thuongbinhloai = ttcb.textBox27.Text;

            giadinh = string.Join(", ",ttcb.l.ToArray());
            

            cmnd = ttcb.textBox28.Text; ;
            cancuoccdan = ttcb.textBox29.Text;

            if (ttcb.dt9 == true)
            {
                mienCtac = ttcb.dateTimePicker9.Value;
            }
            else
            {
                mienCtac = null;
            }

            //// dao tao chung 3
            UCDaoTaoChung3 dtc = new UCDaoTaoChung3();
            khenthuong = dtc.richTextBox1.Text;
            huyhieudang = string.Join(", ",dtc.l.ToArray());
            
            danhhieu = dtc.richTextBox2.Text;
            kyluat = dtc.richTextBox3.Text;


            //// dac dien lich su va quan he nuoc ngoai 4
            ///
            ucDdlsVaQhng4 lsqh = new ucDdlsVaQhng4();
            bixoaten = lsqh.textBox1.Text;
            if (lsqh.dt1 == true)
            {
                thoigian = lsqh.dateTimePicker1.Value;
            }
            else
            {
                thoigian = null;
            }               

            xoataichibo = lsqh.textBox2.Text;
            ketnaplai = lsqh.textBox3.Text;

            if (lsqh.dt2 == true)
            {
                ngayvao = lsqh.dateTimePicker2.Value;
            }
            else
            {
                ngayvao = null;
            }


            vaochibo = lsqh.textBox4.Text;
            vaonguoigt1 = lsqh.textBox5.Text;
            vaochucvu1 = lsqh.textBox6.Text;
            vaochucvu1 = lsqh.textBox7.Text;
            vaonguoigt2 = lsqh.textBox8.Text;
            vaochucvu2 = lsqh.textBox10.Text;
            vaochucvu2 = lsqh.textBox9.Text;

            if (lsqh.dt3 == true)
            {
                ngaychinhthuc2 = lsqh.dateTimePicker3.Value;
            }
            else
            {
                ngaychinhthuc2 = null;
            }


            vaochibo2 = lsqh.textBox11.Text;

            if (lsqh.dt4 == true)
            {
                ngaykhoiphucdangtich = lsqh.dateTimePicker4.Value;
            }
            else
            {
                ngaykhoiphucdangtich = null;
            }

            vaochibo3 = lsqh.textBox12.Text;

            if (lsqh.dt5 == true)
            {
                ngaybikyluat = lsqh.dateTimePicker5.Value;
            }
            else
            {
                ngaybikyluat = null;
            }

            thongtinkyluat = lsqh.richTextBox5.Text;

            if (lsqh.dt6 == true)
            {
                ngaylamviecchedocu = lsqh.dateTimePicker6.Value;
            }
            else
            {
                ngaylamviecchedocu = null;
            }

            thongtinchedocu = lsqh.richTextBox2.Text;

            if(lsqh.dt7 == true)
            {
                dinuocngoaitu = lsqh.dateTimePicker7.Value;
            }
            else
            {
                dinuocngoaitu = null;
            }

            if (lsqh.dt8 == true)
            {
                dinuocngoaiden = lsqh.dateTimePicker8.Value;
            }
            else
            {
                dinuocngoaiden = null;
            }

            thongtindinuocngoai = lsqh.richTextBox1.Text;
            thamgiatochucnuocngoai = lsqh.richTextBox3.Text;
            nguoithannuocngoai = lsqh.richTextBox4.Text;

            // hoan canh gia dinh 6
            tongthunhap = this.textBox1.Text + " Đồng";
            binhquandaunguoi = this.textBox2.Text + " Đồng";
            nhaoduoccap = this.textBox3.Text;
            dientichnhaoduoccap = this.textBox4.Text + " m2";
            nhaotumua = this.textBox5.Text;
            dientichnhaotumua = this.textBox6.Text + " m2";
            datoduoccap = this.textBox7.Text + " m2";
            datotumua = this.textBox8.Text + " m2";
            hdkinhte = this.textBox9.Text;
            dientichtrangtrai = this.textBox10.Text + " ha";
            soldthue = this.textBox11.Text;
            taisancogiatricao = this.richTextBox1.Text;
            giatri = this.textBox12.Text +" Đồng";
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


    }
}
