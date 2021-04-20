using System;
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
    public partial class formThemVaThongTinDangVien : Form
    {
        public List<string> l = new List<string>();
        public bool dt1 = false;
        public bool dt2 = false;
        public bool dt3 = false;
        public bool dt4 = false;
        public bool dt5 = false;
        public bool dt6 = false;
        public bool dt7 = false;
        public bool dt8 = false;
        public bool dt9 = false;



        public static bool check = false;
        string gioitinh = "";
        string giadinh = "";

        private static formThemVaThongTinDangVien _instance;
        public static formThemVaThongTinDangVien Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new formThemVaThongTinDangVien();
                }
                return _instance;
            }
        }

        public formThemVaThongTinDangVien()
        {
            InitializeComponent();
        }



        private void formThemVaThongTinDangVien_Load(object sender, EventArgs e)
        {
            int x = (panel2.Size.Width - label1.Size.Width) / 2;
            label1.Location = new Point(x, label1.Location.Y);
            label1.Text = "I. THÔNG TIN CƠ BẢN";
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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            if (check == true)
            {
                getinfor();
                foreach (string text in giadinh.Split(','))
                {
                    if (text == checkBox1.Text)
                    {
                        checkBox1.Checked = true;
                    }
                    if (text == checkBox2.Text)
                    {
                        checkBox1.Checked = true;
                    }
                }

                if (gioitinh == "Nam")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
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


                setInfor();
                formThemVaThongTinDangVien2 f = (formThemVaThongTinDangVien2)Application.OpenForms["formThemVaThongTinDangVien2"];
                if (f == null)
                {
                    f = new formThemVaThongTinDangVien2();
                    f.AutoScroll = true;
                }
                f.Show();
                this.Hide();

            
            

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

            dangvien.taichibo = comboBox3.Text;
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

            dangvien.taichibo2 = comboBox4.Text;

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


        private void formThemVaThongTinDangVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            usercontrolForm.closeForm();
        }


        private void getinfor()
        {

            connectDb con = new connectDb();
            con.con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from ttcbDv where solylich='" + dangvien.solylich + "'", con.con))
                {
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            textBox1.Text = read.GetString(2);
                            gioitinh = read.GetString(3);
                            textBox2.Text = read.GetString(4);//


                            dateTimePicker1.Value = read.GetDateTime(5);

                        textBox3.Text = read.GetString(6);
                        textBox4.Text = read.GetString(7);
                        textBox5.Text = read.GetString(8);
                        textBox6.Text = read.GetString(9);
                        comboBox2.Text = read.GetString(10);
                        comboBox1.Text = read.GetString(11);
                        textBox7.Text = read.GetString(12);
                        textBox8.Text = read.GetString(13);

                            if (!read.IsDBNull(14))
                            {
                            dateTimePicker2.Value = read.GetDateTime(14);
                            }
                            else
                            {
                            dateTimePicker2.Value = DateTime.Today;
                            }

                        comboBox3.Text = read.GetString(15);
                        textBox10.Text = read.GetString(16);
                        textBox11.Text = read.GetString(17);
                        textBox12.Text = read.GetString(18);
                        textBox13.Text = read.GetString(19);

                            if (!read.IsDBNull(20))
                            {
                            dateTimePicker3.Value = read.GetDateTime(20);
                            }
                            else
                            {
                            dateTimePicker3.Value = DateTime.Today;
                            }

                            if (!read.IsDBNull(21))
                            {
                            dateTimePicker4.Value = read.GetDateTime(21);
                            }
                            else
                            {
                            dateTimePicker4.Value = DateTime.Today;
                            }


                        comboBox4.SelectedText = read.GetString(22);

                            if (!read.IsDBNull(23))
                            {
                            dateTimePicker5.Value = read.GetDateTime(23);
                            }
                            else
                            {
                            dateTimePicker5.Value = DateTime.Today;
                            }

                        textBox15.Text = read.GetString(24);

                            if (!read.IsDBNull(25))
                            {
                            dateTimePicker6.Value = read.GetDateTime(25);
                            }
                            else
                            {
                            dateTimePicker6.Value = DateTime.Today;
                            }

                        textBox16.Text = read.GetString(26);

                            if (!read.IsDBNull(27))
                            {
                            dateTimePicker7.Value = read.GetDateTime(27);
                            }
                            else
                            {
                            dateTimePicker7.Value = DateTime.Today;
                            }

                            if (!read.IsDBNull(28))
                            {
                            dateTimePicker8.Value = read.GetDateTime(28);
                            }
                            else
                            {
                            dateTimePicker8.Value = DateTime.Today;
                            }


                        textBox17.Text = read.GetString(29);
                        textBox18.Text = read.GetString(30);
                        textBox19.Text = read.GetString(31);
                        textBox20.Text = read.GetString(32);
                        textBox30.Text = read.GetString(33);
                        textBox21.Text = read.GetString(34);
                        textBox22.Text = read.GetString(35);
                        textBox23.Text = read.GetString(36);
                        textBox24.Text = read.GetString(37);
                        textBox25.Text = read.GetString(38);
                        textBox26.Text = read.GetString(39);
                        textBox27.Text = read.GetString(40);
                            giadinh = read.GetString(41);
                        textBox28.Text = read.GetString(42);
                            textBox29.Text = read.GetString(43);

                            if (!read.IsDBNull(44))
                            {
                                dateTimePicker9.Value = read.GetDateTime(44);
                            }
                            else
                            {
                                dateTimePicker9.Value = DateTime.Today;
                            }
                        
                    }
                }
            }
                

            con.con.Close();            
        }
    }
}
