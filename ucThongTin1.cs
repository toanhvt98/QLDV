using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLDV
{
    public partial class ucThongTin1 : UserControl
    {
        public static List<string> l = new List<string>();
        public bool dt1 = false;
        public bool dt2 = false;
        public bool dt3 = false;
        public bool dt4 = false;
        public bool dt5 = false;
        public bool dt6 = false;
        public bool dt7 = false;
        public bool dt8 = false;
        public bool dt9 = false;

        public ucThongTin1(formThemVaThongTinDangVien f)
        {
            InitializeComponent();
        }

        public static bool check = false;
        public static bool check1 = false;
        string gioitinh = "";
        string giadinh = "";
        public ucThongTin1()
        {
            InitializeComponent();
            
        }

        public static ucThongTin1 _instance;
        public static ucThongTin1 Instance
        {
            get
            {
                if(_instance == null || _instance.IsDisposed == true)
                {
                    _instance = new ucThongTin1();
                }
                return _instance;
            }
        }

        private void ucThongTin1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            dateTimePicker3.MaxDate = DateTime.Now;
            dateTimePicker4.MaxDate = DateTime.Now;
            dateTimePicker5.MaxDate = DateTime.Now;
            dateTimePicker6.MaxDate = DateTime.Now;
            dateTimePicker7.MaxDate = DateTime.Now;
            dateTimePicker8.MaxDate = DateTime.Now;
            dateTimePicker9.MaxDate = DateTime.Now;
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker3.ValueChanged += new EventHandler(dateTimePicker3_ValueChanged);
            dateTimePicker4.ValueChanged += new EventHandler(dateTimePicker4_ValueChanged);
            dateTimePicker5.ValueChanged += new EventHandler(dateTimePicker5_ValueChanged);
            dateTimePicker6.ValueChanged += new EventHandler(dateTimePicker6_ValueChanged);
            dateTimePicker7.ValueChanged += new EventHandler(dateTimePicker7_ValueChanged);
            dateTimePicker8.ValueChanged += new EventHandler(dateTimePicker8_ValueChanged);
            dateTimePicker9.ValueChanged += new EventHandler(dateTimePicker9_ValueChanged);
            

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
                string a = String.Concat(con.getTextCheckBox("ttcbDv", "giadinh", dangvien.solylich).Where(c => !Char.IsWhiteSpace(c)));
                List<string> l = a.Split(',').ToList();
                for(int i = 0; i < l.Count; i++)
                {
                    if(l[i] == String.Concat(checkBox1.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox1.Checked = true;
                    }
                    if (l[i] == String.Concat(checkBox2.Text.Where(c => !Char.IsWhiteSpace(c))))
                    {
                        checkBox2.Checked = true;
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
            else
            {
                radioButton1.Checked = true;
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
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


        public void setInfor()
        {
            dangvien.tendangdung = ucThongTin1.Instance.textBox1.Text;
            if (radioButton1.Checked)
            {
                dangvien.gioitinh = ucThongTin1.Instance.radioButton1.Text;
            }
            else
                dangvien.gioitinh = ucThongTin1.Instance.radioButton2.Text;

            dangvien.tenkhaisinh = ucThongTin1.Instance.textBox2.Text;

            dangvien.ngaysinh = ucThongTin1.Instance.dateTimePicker1.Value;


            dangvien.noisinh = ucThongTin1.Instance.textBox3.Text;
            dangvien.quequan = ucThongTin1.Instance.textBox4.Text;
            dangvien.noithuongtru = ucThongTin1.Instance.textBox5.Text;
            dangvien.noitamtru = ucThongTin1.Instance.textBox6.Text;
            dangvien.dantoc = ucThongTin1.Instance.comboBox2.Text;
            dangvien.tongiao = ucThongTin1.Instance.comboBox1.Text;
            dangvien.thanhphangd = ucThongTin1.Instance.textBox7.Text;
            dangvien.nghenghiephiennay = ucThongTin1.Instance.textBox8.Text;

            if (dt2 == true)
            {
                dangvien.ngayvaodang = ucThongTin1.Instance.dateTimePicker2.Value;
            }
            else
            {
                dangvien.ngayvaodang = null;
            }

            dangvien.taichibo = ucThongTin1.Instance.comboBox3.Text;
            dangvien.nguoigt1 = ucThongTin1.Instance.textBox10.Text;
            dangvien.chucvudonvi1 = ucThongTin1.Instance.textBox11.Text;
            dangvien.nguoigt2 = ucThongTin1.Instance.textBox12.Text;
            dangvien.chucvudonvi2 = ucThongTin1.Instance.textBox13.Text;

            if (dt3 == true)
            {
                dangvien.ngaycap = ucThongTin1.Instance.dateTimePicker3.Value;
            }
            else
            {
                dangvien.ngaycap = null;
            }

            if (dt4 == true)
            {
                dangvien.ngaychinhthuc = ucThongTin1.Instance.dateTimePicker4.Value;
            }
            else
            {
                dangvien.ngaychinhthuc = null;
            }

            dangvien.taichibo2 = ucThongTin1.Instance.comboBox4.Text;

            if (dt5 == true)
            {
                dangvien.ngayduoctuyendung = ucThongTin1.Instance.dateTimePicker5.Value;
            }
            else
            {
                dangvien.ngayduoctuyendung = null;
            }

            dangvien.coquantuyendung = ucThongTin1.Instance.textBox15.Text;

            if (dt6 == true)
            {
                dangvien.ngayvaodoan = ucThongTin1.Instance.dateTimePicker6.Value;
            }
            else
            {
                dangvien.ngayvaodoan = null;
            }


            dangvien.thamgiatochucxh = ucThongTin1.Instance.textBox16.Text;

            if (dt7 == true)
            {
                dangvien.ngaynhapngu = ucThongTin1.Instance.dateTimePicker7.Value;
            }
            else
            {
                dangvien.ngaynhapngu = null;
            }

            if (dt8 == true)
            {
                dangvien.ngayxuatngu = ucThongTin1.Instance.dateTimePicker8.Value;
            }
            else
            {
                dangvien.ngayxuatngu = null;
            }

            dangvien.trinhdohiennay = ucThongTin1.Instance.textBox17.Text;
            dangvien.gdphothong = ucThongTin1.Instance.textBox18.Text;
            dangvien.gdNgheNghiep = ucThongTin1.Instance.textBox19.Text;
            dangvien.gddaihoc = ucThongTin1.Instance.textBox20.Text;
            dangvien.gdsaudaihoc = ucThongTin1.Instance.textBox30.Text;
            dangvien.hocvi = ucThongTin1.Instance.textBox21.Text;
            dangvien.hocham = ucThongTin1.Instance.textBox22.Text;
            dangvien.lyluanct = ucThongTin1.Instance.textBox23.Text;
            dangvien.ngoaingu = ucThongTin1.Instance.textBox24.Text;
            dangvien.tinhoc = ucThongTin1.Instance.textBox25.Text;
            dangvien.tinhtrangsuckhoe = ucThongTin1.Instance.textBox26.Text;
            dangvien.thuongbinhloai = ucThongTin1.Instance.textBox27.Text;

            List<string> checkedItems = (from Control c in Controls where c is CheckBox && ((CheckBox)c).Checked select c.Text).ToList();

            dangvien.giadinh = string.Join(", ", checkedItems.ToArray());


            dangvien.cmnd = ucThongTin1.Instance.textBox28.Text; ;
            dangvien.cancuoccdan = ucThongTin1.Instance.textBox29.Text;

            if (dt9 == true)
            {
                dangvien.mienCtac = ucThongTin1.Instance.dateTimePicker9.Value;
            }
            else
            {
                dangvien.mienCtac = null;
            }
        }





        public void getinfor()
        {

            connectDb con = new connectDb();
            con.con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from ttcbDv where solylich='" + dangvien.solylich + "'", con.con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        ucThongTin1.Instance.textBox1.Text = read.GetString(2);
                        ucThongTin1.Instance.gioitinh = read.GetString(3);
                        ucThongTin1.Instance.textBox2.Text = read.GetString(4);//


                        ucThongTin1.Instance.dateTimePicker1.Value = read.GetDateTime(5);

                        ucThongTin1.Instance.textBox3.Text = read.GetString(6);
                        ucThongTin1.Instance.textBox4.Text = read.GetString(7);
                        ucThongTin1.Instance.textBox5.Text = read.GetString(8);
                        ucThongTin1.Instance.textBox6.Text = read.GetString(9);
                        ucThongTin1.Instance.comboBox2.Text = read.GetString(10);
                        ucThongTin1.Instance.comboBox1.Text = read.GetString(11);
                        ucThongTin1.Instance.textBox7.Text = read.GetString(12);
                        ucThongTin1.Instance.textBox8.Text = read.GetString(13);

                        if (!read.IsDBNull(14))
                        {
                            ucThongTin1.Instance.dateTimePicker2.Value = read.GetDateTime(14);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker2.Value = DateTime.Today;
                        }

                        ucThongTin1.Instance.comboBox3.Text = read.GetString(15);
                        ucThongTin1.Instance.textBox10.Text = read.GetString(16);
                        ucThongTin1.Instance.textBox11.Text = read.GetString(17);
                        ucThongTin1.Instance.textBox12.Text = read.GetString(18);
                        ucThongTin1.Instance.textBox13.Text = read.GetString(19);

                        if (!read.IsDBNull(20))
                        {
                            ucThongTin1.Instance.dateTimePicker3.Value = read.GetDateTime(20);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker3.Value = DateTime.Today;
                        }

                        if (!read.IsDBNull(21))
                        {
                            ucThongTin1.Instance.dateTimePicker4.Value = read.GetDateTime(21);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker4.Value = DateTime.Today;
                        }


                        ucThongTin1.Instance.comboBox4.SelectedText = read.GetString(22);

                        if (!read.IsDBNull(23))
                        {
                            ucThongTin1.Instance.dateTimePicker5.Value = read.GetDateTime(23);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker5.Value = DateTime.Today;
                        }

                        ucThongTin1.Instance.textBox15.Text = read.GetString(24);

                        if (!read.IsDBNull(25))
                        {
                            ucThongTin1.Instance.dateTimePicker6.Value = read.GetDateTime(25);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker6.Value = DateTime.Today;
                        }

                        ucThongTin1.Instance.textBox16.Text = read.GetString(26);

                        if (!read.IsDBNull(27))
                        {
                            ucThongTin1.Instance.dateTimePicker7.Value = read.GetDateTime(27);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker7.Value = DateTime.Today;
                        }

                        if (!read.IsDBNull(28))
                        {
                            ucThongTin1.Instance.dateTimePicker8.Value = read.GetDateTime(28);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker8.Value = DateTime.Today;
                        }


                        ucThongTin1.Instance.textBox17.Text = read.GetString(29);
                        ucThongTin1.Instance.textBox18.Text = read.GetString(30);
                        ucThongTin1.Instance.textBox19.Text = read.GetString(31);
                        ucThongTin1.Instance.textBox20.Text = read.GetString(32);
                        ucThongTin1.Instance.textBox30.Text = read.GetString(33);
                        ucThongTin1.Instance.textBox21.Text = read.GetString(34);
                        ucThongTin1.Instance.textBox22.Text = read.GetString(35);
                        ucThongTin1.Instance.textBox23.Text = read.GetString(36);
                        ucThongTin1.Instance.textBox24.Text = read.GetString(37);
                        ucThongTin1.Instance.textBox25.Text = read.GetString(38);
                        ucThongTin1.Instance.textBox26.Text = read.GetString(39);
                        ucThongTin1.Instance.textBox27.Text = read.GetString(40);

                        ucThongTin1.Instance.giadinh = read.GetString(41);


                        ucThongTin1.Instance.textBox28.Text = read.GetString(42);
                        ucThongTin1.Instance.textBox29.Text = read.GetString(43);

                        if (!read.IsDBNull(44))
                        {
                            ucThongTin1.Instance.dateTimePicker9.Value = read.GetDateTime(44);
                        }
                        else
                        {
                            ucThongTin1.Instance.dateTimePicker9.Value = DateTime.Today;
                        }

                    }
                }
            }


            con.con.Close();
        }


    }

}
