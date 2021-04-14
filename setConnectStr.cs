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
using System.Data.SqlClient;

namespace QLDV
{
    public partial class setConnectStr : Form
    {
        public setConnectStr()
        {
            InitializeComponent();
        }

        private void setConnectStr_Load(object sender, EventArgs e)
        {
            if (File.Exists("connect.txt"))
                textBox1.Text = File.ReadAllText("connect.txt");
            else
                textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty)
            {
                if (File.Exists("connect.txt"))
                {
                    File.WriteAllText("connect.txt", string.Empty);
                    File.WriteAllText("connect.txt", textBox1.Text);
                }
                else
                {
                    File.Create("connect.txt");
                    File.WriteAllText("connect.txt", textBox1.Text);
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(File.ReadAllText("connect.txt"));
                SqlCommand cmd = new SqlCommand("select count(*) from INFORMATION_SCHEMA.tables", con);
                con.Open();

                int countTbl = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (countTbl == 11)
                {
                    MessageBox.Show("Kết nối thành công. Đã đủ table trong cơ sở dữ liệu");
                }
                else
                {
                    MessageBox.Show("Kết nối thành công. Còn thiếu " + (countTbl - 11).ToString() + " table trong cơ sở dữ liệu");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối!");
            }
        }
    }
}
