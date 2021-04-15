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
    public partial class ucTaiKhoanCaNhan : UserControl
    {
        public ucTaiKhoanCaNhan()
        {
            InitializeComponent();
        }

        private void ucTaiKhoanCaNhan_Load(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            textBox1.Text = userInfor.username;
            textBox2.Text = userInfor.pwd;
            textBox3.Text = con.getNameChiBoOfAcc(textBox1.Text);
        }
    }
}
