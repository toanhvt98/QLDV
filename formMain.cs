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
    public partial class formMain : Form
    {

        private static formMain _instance;
        public static formMain Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new formMain();
                }
                return _instance;

            }
        }
        public formMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucQLTK uc = new ucQLTK();
            usercontrolForm.showcontrol(uc, panel1);
        }

        private void chiBộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucQLCB uc = new ucQLCB();
            usercontrolForm.showcontrol(uc, panel1);
        }

        private void thêmĐảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uclDangVien uc = new uclDangVien();
            usercontrolForm.showcontrol(uc, panel1);
        }

        private void thôngTinĐảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCThongTin uc = new UCThongTin();
            usercontrolForm.showcontrol(uc, panel1);
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            uclDangVien uc = new uclDangVien();
            usercontrolForm.showcontrol(uc, panel1);
            
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucThongKe uc = new ucThongKe();
            usercontrolForm.showcontrol(uc, panel1);
        }

        private void tàiKhoảnCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucTaiKhoanCaNhan uc = new ucTaiKhoanCaNhan();
            usercontrolForm.showcontrol(uc, panel1);
        }

        private void đăngXuấtToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đăng xuất khỏi tài khoản không?","Đăng xuất",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                this.Hide();
                formLogin f = new formLogin();
                f.ShowDialog();
                this.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ucQuanLyFilecs uc = new ucQuanLyFilecs();
            usercontrolForm.showcontrol(uc, panel1);
        }
    }
}
