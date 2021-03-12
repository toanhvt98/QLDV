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
    public partial class ucDdlsVaQhng : UserControl
    {
        public ucDdlsVaQhng()
        {
            InitializeComponent();
        }
        private static ucDdlsVaQhng _instance;
        public static ucDdlsVaQhng Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucDdlsVaQhng();
                }
                return _instance;
            }
        }
        private void ucDdlsVaQhng_Load(object sender, EventArgs e)
        {

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
            dtvttdv.panel1.Controls.Add(UCDaoTaoChung.Instance);
            UCDaoTaoChung.Instance.Dock = DockStyle.Fill;
            UCDaoTaoChung.Instance.BringToFront();
        }
    }
}
