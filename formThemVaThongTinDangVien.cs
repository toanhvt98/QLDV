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
    public partial class formThemVaThongTinDangVien : Form
    {
        public formThemVaThongTinDangVien()
        {
            InitializeComponent();
        }

        private void formThemVaThongTinDangVien_Load(object sender, EventArgs e)
        {
            int x = (panel2.Size.Width - label1.Size.Width) / 2;
            label1.Location = new Point(x, label1.Location.Y);
            panel1.Controls.Add(ucTTCBDangVien.Instance);
            ucTTCBDangVien.Instance.Dock = DockStyle.Fill;
            ucTTCBDangVien.Instance.BringToFront();

            panel1.Controls.Add(ucQuaTrinhHoatDongVaCongTac.Instance);
            label1.Text = "I. THÔNG TIN CƠ BẢN";
        }

    }
}
