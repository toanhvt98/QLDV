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
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            if (con.checkLogin(textBox1.Text, textBox2.Text, "admin"))
            {
                this.Hide();
                formMain fMain = new formMain();
                
                fMain.ShowDialog();
                this.Close();
            }
            else if(con.checkLogin(textBox1.Text, textBox2.Text, "user"))
            {
                this.Hide();
                formMain fMain = new formMain();
                fMain.quảnLýToolStripMenuItem.Visible = false;
                fMain.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Tên đăng nhập hoặc tài khoản không đúng!");
        }
    }
}
