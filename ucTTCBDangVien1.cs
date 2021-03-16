using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class ucTTCBDangVien : UserControl
    {


        public List<string> l = new List<string>();

        public static bool check = false;

        public bool dt1 = false;
        public bool dt2 = false;
        public bool dt3 = false;
        public bool dt4 = false;
        public bool dt5 = false;
        public bool dt6 = false;
        public bool dt7 = false;
        public bool dt8 = false;
        public bool dt9 = false;

        public ucTTCBDangVien()
        {
            InitializeComponent();
        }



        private static ucTTCBDangVien _instance;
        public static ucTTCBDangVien Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucTTCBDangVien();
                }
                return _instance;
            }
        }
        private void ucTTCBDangVien_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker3.ValueChanged += new EventHandler(dateTimePicker3_ValueChanged);
            dateTimePicker4.ValueChanged += new EventHandler(dateTimePicker4_ValueChanged);
            dateTimePicker5.ValueChanged += new EventHandler(dateTimePicker5_ValueChanged);
            dateTimePicker6.ValueChanged += new EventHandler(dateTimePicker6_ValueChanged);
            dateTimePicker7.ValueChanged += new EventHandler(dateTimePicker7_ValueChanged);
            dateTimePicker8.ValueChanged += new EventHandler(dateTimePicker8_ValueChanged);
            dateTimePicker9.ValueChanged += new EventHandler(dateTimePicker9_ValueChanged);
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            dateTimePicker3.MaxDate = DateTime.Now;
            dateTimePicker4.MaxDate = DateTime.Now;
            dateTimePicker5.MaxDate = DateTime.Now;
            dateTimePicker6.MaxDate = DateTime.Now;
            dateTimePicker7.MaxDate = DateTime.Now;
            dateTimePicker8.MaxDate = DateTime.Now;
            dateTimePicker9.MaxDate = DateTime.Now;

            connectDb con = new connectDb();
            con.themChiBoCombo(comboBox3);
            con.themChiBoCombo(comboBox4);


            //important
            if(check == true)
            {
                foreach (Control c in panel1.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)(c)).Text = "";
                    }
                    else if(c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }
                    else if(c is ComboBox)
                    {
                        ((ComboBox)c).SelectedIndex = 0;
                    }
                    else if(c is DateTimePicker)
                    {
                        ((DateTimePicker)c).Value = DateTime.Today;
                    }

                }
            }
            

        }
        private void button2_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                dtvttdv.Close();
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            List<string> l = new List<string>();

            if (checkBox1.Checked)
            {
                l.Add(checkBox1.Text);
            }
            if (checkBox2.Checked)
            {
                l.Add(checkBox2.Text);
            }
            
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "II. TÓM TẮT QUÁ TRÌNH HOẠT ĐỘNG VÀ CÔNG TÁC";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucQuaTrinhHoatDongVaCongTac2.Instance);
            ucQuaTrinhHoatDongVaCongTac2.Instance.Dock = DockStyle.Fill;
            ucQuaTrinhHoatDongVaCongTac2.Instance.BringToFront();

            setInfor();
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dt1 = true;
        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dt2 = true;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            dt3 = true;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            dt4 = true;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            dt5 = true;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            dt6 = true;
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            dt7 = true;
        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            dt8 = true;
        }

        private void dateTimePicker9_ValueChanged(object sender, EventArgs e)
        {
            dt9 = true;
        }


        private void setInfor()
        {
            dangvien.tendangdung = textBox1.Text;
            if (radioButton1.Checked)
            {
                dangvien.gioitinh = radioButton1.Text;
            }
            else
                dangvien.gioitinh = radioButton2.Text;

            dangvien.tenkhaisinh = textBox2.Text;

            dangvien.ngaysinh = dateTimePicker1.Value;


            dangvien.noisinh = textBox3.Text;
            dangvien.quequan = textBox4.Text;
            dangvien.noithuongtru = textBox5.Text;
            dangvien.noitamtru = textBox6.Text;
            dangvien.dantoc = comboBox2.Text;
            dangvien.tongiao = comboBox1.Text;
            dangvien.thanhphangd = textBox7.Text;
            dangvien.nghenghiephiennay = textBox8.Text;

            if (dt2 == true)
            {
                dangvien.ngayvaodang = dateTimePicker2.Value;
            }
            else
            {
                dangvien.ngayvaodang = null;
            }

            dangvien.taichibo = comboBox3.SelectedText.ToString();
            dangvien.nguoigt1 = textBox10.Text;
            dangvien.chucvudonvi1 = textBox11.Text;
            dangvien.nguoigt2 = textBox12.Text;
            dangvien.chucvudonvi2 = textBox13.Text;

            if (dt3 == true)
            {
                dangvien.ngaycap = dateTimePicker3.Value;
            }
            else
            {
                dangvien.ngaycap = null;
            }

            if (dt4 == true)
            {
                dangvien.ngaychinhthuc = dateTimePicker4.Value;
            }
            else
            {
                dangvien.ngaychinhthuc = null;
            }

            dangvien.taichibo2 = comboBox4.SelectedText.ToString();

            if (dt5 == true)
            {
                dangvien.ngayduoctuyendung = dateTimePicker5.Value;
            }
            else
            {
                dangvien.ngayduoctuyendung = null;
            }

            dangvien.coquantuyendung = textBox15.Text;

            if (dt6 == true)
            {
                dangvien.ngayvaodoan = dateTimePicker6.Value;
            }
            else
            {
                dangvien.ngayvaodoan = null;
            }


            dangvien.thamgiatochucxh = textBox16.Text;

            if (dt7 == true)
            {
                dangvien.ngaynhapngu = dateTimePicker7.Value;
            }
            else
            {
                dangvien.ngaynhapngu = null;
            }

            if (dt8 == true)
            {
                dangvien.ngayxuatngu = dateTimePicker8.Value;
            }
            else
            {
                dangvien.ngayxuatngu = null;
            }

            dangvien.trinhdohiennay = textBox17.Text;
            dangvien.gdphothong = textBox18.Text;
            dangvien.gdNgheNghiep = textBox19.Text;
            dangvien.gddaihoc = textBox20.Text;
            dangvien.gdsaudaihoc = textBox30.Text;
            dangvien.hocvi = textBox21.Text;
            dangvien.hocham = textBox22.Text;
            dangvien.lyluanct = textBox23.Text;
            dangvien.ngoaingu = textBox24.Text;
            dangvien.tinhoc = textBox25.Text;
            dangvien.tinhtrangsuckhoe = textBox26.Text;
            dangvien.thuongbinhloai = textBox27.Text;

            dangvien.giadinh = string.Join(", ", l.ToArray());


            dangvien.cmnd = textBox28.Text; ;
            dangvien.cancuoccdan = textBox29.Text;

            if (dt9 == true)
            {
                dangvien.mienCtac = dateTimePicker9.Value;
            }
            else
            {
                dangvien.mienCtac = null;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }


    }
}
