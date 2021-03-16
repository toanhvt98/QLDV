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
    public partial class ucQuanHeGD5 : UserControl
    {

        public static bool check = false;
        public ucQuanHeGD5()
        {
            InitializeComponent();
        }
        private static ucQuanHeGD5 _instance;
        public static ucQuanHeGD5 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucQuanHeGD5();
                }
                return _instance;
            }
        }
        private void ucQuanHeGD5_Load(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(DateTime.Now.Year.ToString());
            while (i >= 1900)
            {
                comboBox1.Items.Add(i);
                i--;
            }
            comboBox1.SelectedIndex = 0;

            if(check == true)
            {
                foreach(Control c in groupBox1.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).Text = "";
                    }
                    else if (c is ComboBox)
                    {
                        ((ComboBox)c).SelectedIndex = 0;
                    }
                    else if (c is RichTextBox)
                    {
                        ((RichTextBox)c).Text = "";
                    }
                }

                foreach(Control c in this.Controls)
                {
                    if(c is DataGridView)
                    {
                        ((DataGridView)c).DataSource = null;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "IV. ĐẶC ĐIỂM LỊCH SỬ VÀ QUAN HỆ VỚI NƯỚC NGOÀI";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucDdlsVaQhng4.Instance);
            ucDdlsVaQhng4.Instance.Dock = DockStyle.Fill;
            ucDdlsVaQhng4.Instance.BringToFront();

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

        private void button1_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "VI. HOÀN CẢNH GIA KINH TẾ CỦA BẢN THÂN VÀ GIA ĐÌNH";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucHoanCanhGiaDinh6.Instance);
            ucHoanCanhGiaDinh6.Instance.Dock = DockStyle.Fill;
            ucHoanCanhGiaDinh6.Instance.BringToFront();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
