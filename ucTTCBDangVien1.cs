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
    public partial class ucTTCBDangVien : UserControl
    {
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "II. TÓM TẮT QUÁ TRÌNH HOẠT ĐỘNG VÀ CÔNG TÁC";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucQuaTrinhHoatDongVaCongTac2.Instance);
            ucQuaTrinhHoatDongVaCongTac2.Instance.Dock = DockStyle.Fill;
            ucQuaTrinhHoatDongVaCongTac2.Instance.BringToFront();
        }
    }
}
