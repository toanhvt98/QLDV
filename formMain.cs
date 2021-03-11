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
        public formMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panel1.Controls.Add(uclDangVien.Instance);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(ucQLTK.Instance);
            ucQLTK.Instance.Dock = DockStyle.Fill;
            ucQLTK.Instance.BringToFront();
        }

        private void chiBộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(ucQLCB.Instance);
            ucQLCB.Instance.Dock = DockStyle.Fill;
            ucQLCB.Instance.BringToFront();
        }

        private void thêmĐảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uclDangVien.Instance.Dock = DockStyle.Fill;
            uclDangVien.Instance.BringToFront();
        }
    }
}
