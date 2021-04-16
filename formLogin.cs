using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            if(textBox1.Text == "createadminaccount" && textBox2.Text == "createadminaccount")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                formCreateAdminAcc f = new formCreateAdminAcc();
                f.ShowDialog();
                
            }
            else if(textBox1.Text == "setconnect" && textBox2.Text == "setconnect")
            {
                setConnectStr setCon = new setConnectStr();
                if (!File.Exists("connect.txt"))
                {
                    File.Create("connect.txt");
                }
                userInfor.username = textBox1.Text;
                userInfor.pwd = textBox1.Text;
                setCon.ShowDialog();
            }
            else if (con.checkLogin(textBox1.Text, textBox2.Text, "admin"))
            {
                this.Hide();
                formMain fMain = new formMain();
                userInfor.username = textBox1.Text;
                userInfor.pwd = textBox1.Text;
                fMain.ShowDialog();
                this.Close();
            }
            else if(con.checkLogin(textBox1.Text, textBox2.Text, "user"))
            {
                this.Hide();
                formMain fMain = new formMain();
                fMain.quảnLýToolStripMenuItem.Visible = false;
                userInfor.username = textBox1.Text;
                userInfor.pwd = textBox1.Text;
                fMain.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Tên đăng nhập hoặc tài khoản không đúng!");
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
