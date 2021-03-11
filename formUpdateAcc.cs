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
    public partial class formUpdateAcc : Form
    {
        private readonly ucQLTK qltk;
        public formUpdateAcc(ucQLTK qltk1)
        {
            InitializeComponent();
            qltk = qltk1;
        }

        public int id { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            con.updateAcc(id, textBox2.Text);
            MessageBox.Show("Cập nhật tài khoản: "+textBox1.Text+" thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
            qltk.loaddata();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formUpdateAcc_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
        }
    }
}
