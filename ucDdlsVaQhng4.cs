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
    public partial class ucDdlsVaQhng4 : UserControl
    {

        public static bool check = false;

        public bool dt1 = false;
        public bool dt2 = false;
        public bool dt3 = false;
        public bool dt4 = false;
        public bool dt5 = false;
        public bool dt6 = false;
        public bool dt7 = false;
        public bool dt8 = false;
        public ucDdlsVaQhng4()
        {
            InitializeComponent();
        }



        private static ucDdlsVaQhng4 _instance;
        public static ucDdlsVaQhng4 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucDdlsVaQhng4();
                }
                return _instance;
            }
        }
        private void ucDdlsVaQhng_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker3.ValueChanged += new EventHandler(dateTimePicker3_ValueChanged);
            dateTimePicker4.ValueChanged += new EventHandler(dateTimePicker4_ValueChanged);
            dateTimePicker5.ValueChanged += new EventHandler(dateTimePicker5_ValueChanged);
            dateTimePicker6.ValueChanged += new EventHandler(dateTimePicker6_ValueChanged);
            dateTimePicker7.ValueChanged += new EventHandler(dateTimePicker7_ValueChanged);
            dateTimePicker8.ValueChanged += new EventHandler(dateTimePicker8_ValueChanged);
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            dateTimePicker3.MaxDate = DateTime.Now;
            dateTimePicker4.MaxDate = DateTime.Now;
            dateTimePicker5.MaxDate = DateTime.Now;
            dateTimePicker6.MaxDate = DateTime.Now;
            dateTimePicker7.MaxDate = DateTime.Now;
            dateTimePicker8.MaxDate = DateTime.Now;
            connectDb con = new connectDb();
            con.themChiBoCombo(comboBox1);
            con.themChiBoCombo(comboBox2);
            con.themChiBoCombo(comboBox3);
            con.themChiBoCombo(comboBox4);

            enableGrb(checkBox1, groupBox1);
            enableGrb(checkBox2, groupBox2);

            if(check == true)
            {
                foreach (Control c in this.Controls)
                {
                    if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }
                }

                foreach(Control c in groupBox1.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)(c)).Text = "";
                    }
                    else if (c is ComboBox)
                    {
                        ((ComboBox)c).SelectedIndex = 0;
                    }
                    else if (c is DateTimePicker)
                    {
                        ((DateTimePicker)c).Value = DateTime.Today;
                    }
                    else if( c is RichTextBox)
                    {
                        ((RichTextBox)c).Text = "";
                    }
                }

                foreach(Control c in groupBox2.Controls)
                {
                    if (c is DateTimePicker)
                    {
                        ((DateTimePicker)c).Value = DateTime.Today;
                    }
                    else if (c is RichTextBox)
                    {
                        ((RichTextBox)c).Text = "";
                    }
                }

            }
        }

        private void enableGrb(CheckBox cb, GroupBox grb)
        {
            grb.Enabled = cb.Checked;
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

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "III. ĐÀO TẠO, BỒI DƯỠNG VỀ CHUYÊN MÔN, NGHIỆP VỤ, LÝ LUẬN CHÍNH TRỊ, NGOẠI NGỮ";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(UCDaoTaoChung3.Instance);
            UCDaoTaoChung3.Instance.Dock = DockStyle.Fill;
            UCDaoTaoChung3.Instance.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "V. QUAN HỆ GIA ĐÌNH";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucQuanHeGD5.Instance);
            ucQuanHeGD5.Instance.Dock = DockStyle.Fill;
            ucQuanHeGD5.Instance.BringToFront();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            enableGrb(checkBox1, groupBox1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            enableGrb(checkBox2, groupBox2);
        }

        private void setInfor()
        {
            dangvien.bixoaten = textBox1.Text;
            if (dt1 == true)
            {
                dangvien.thoigian = dateTimePicker1.Value;
            }
            else
            {
                dangvien.thoigian = null;
            }

            dangvien.xoataichibo = comboBox1.SelectedText.ToString();
            dangvien.ketnaplai = textBox3.Text;

            if (dt2 == true)
            {
                dangvien.ngayvao = dateTimePicker2.Value;
            }
            else
            {
                dangvien.ngayvao = null;
            }


            dangvien.vaochibo = comboBox2.SelectedText.ToString();
            dangvien.vaonguoigt1 = textBox5.Text;
            dangvien.vaochucvu1 = textBox6.Text;
            dangvien.vaodonvi1 = textBox7.Text;
            dangvien.vaonguoigt2 = textBox8.Text;
            dangvien.vaochucvu2 = textBox10.Text;
            dangvien.vaodonvi2 = textBox9.Text;

            if (dt3 == true)
            {
                dangvien.ngaychinhthuc2 = dateTimePicker3.Value;
            }
            else
            {
                dangvien.ngaychinhthuc2 = null;
            }


            dangvien.vaochibo2 = comboBox3.SelectedText.ToString();

            if (dt4 == true)
            {
                dangvien.ngaykhoiphucdangtich = dateTimePicker4.Value;
            }
            else
            {
                dangvien.ngaykhoiphucdangtich = null;
            }

            dangvien.vaochibo3 = comboBox4.SelectedText.ToString();

            if (dt5 == true)
            {
                dangvien.ngaybikyluat = dateTimePicker5.Value;
            }
            else
            {
                dangvien.ngaybikyluat = null;
            }

            dangvien.thongtinkyluat = richTextBox5.Text;

            if (dt6 == true)
            {
                dangvien.ngaylamviecchedocu = dateTimePicker6.Value;
            }
            else
            {
                dangvien.ngaylamviecchedocu = null;
            }

            dangvien.thongtinchedocu = richTextBox2.Text;

            if (dt7 == true)
            {
                dangvien.dinuocngoaitu = dateTimePicker7.Value;
            }
            else
            {
                dangvien.dinuocngoaitu = null;
            }

            if (dt8 == true)
            {
                dangvien.dinuocngoaiden = dateTimePicker8.Value;
            }
            else
            {
                dangvien.dinuocngoaiden = null;
            }

            dangvien.thongtindinuocngoai = richTextBox1.Text;
            dangvien.thamgiatochucnuocngoai = richTextBox3.Text;
            dangvien.nguoithannuocngoai = richTextBox4.Text;
        }
    }
}
