using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDV
{
    public partial class ucThongTin6 : UserControl
    {
        public static bool check = false;
        private static ucThongTin6 _instance;
        public static ucThongTin6 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucThongTin6();
                }
                return _instance;
            }
        }

        public ucThongTin6()
        {
            InitializeComponent();
        }

        private void ucThongTin6_Load(object sender, EventArgs e)
        {
            if (check == true)
            {
                getInfor();
            }
        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1)))
            {
                e.Handled = true;
            }
        }

        public void setInfor()
        {
            dangvien.tongthunhap = this.textBox1.Text;
            dangvien.binhquandaunguoi = this.textBox2.Text;
            dangvien.nhaoduoccap = this.textBox3.Text;
            dangvien.dientichnhaoduoccap = this.textBox4.Text;
            dangvien.nhaotumua = this.textBox5.Text;
            dangvien.dientichnhaotumua = this.textBox6.Text;
            dangvien.datoduoccap = this.textBox7.Text;
            dangvien.datotumua = this.textBox8.Text;
            dangvien.hdkinhte = this.textBox9.Text;
            dangvien.dientichtrangtrai = this.textBox10.Text;
            dangvien.soldthue = this.textBox11.Text;
            dangvien.taisancogiatricao = this.richTextBox1.Text;
            dangvien.giatri = this.textBox12.Text;
        }

        public void getInfor()
        {
            connectDb con = new connectDb();
            con.con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from hcgiadinh where solylich='" + dangvien.solylich + "'", con.con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        textBox1.Text = read.GetString(2);
                        textBox2.Text = read.GetString(3);
                        textBox3.Text = read.GetString(4);
                        textBox4.Text = read.GetString(5);
                        textBox5.Text = read.GetString(6);
                        textBox6.Text = read.GetString(7);
                        textBox7.Text = read.GetString(8);
                        textBox8.Text = read.GetString(9);
                        textBox9.Text = read.GetString(10);
                        textBox10.Text = read.GetString(11);
                        textBox11.Text = read.GetString(12);
                        richTextBox1.Text = read.GetString(13);
                        textBox12.Text = read.GetString(14);
                    }
                }
            }
            con.con.Close();
        }

    }
}
