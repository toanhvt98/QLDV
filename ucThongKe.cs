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
    public partial class ucThongKe : UserControl
    {
        string nam = null;
        string nu = null;
        string congCm = null;
        string huyhieu = null;
        DataTable dt = new DataTable();
        public ucThongKe()
        {
            InitializeComponent();
            for(int i = 0; i < 5; i++)
            {
                dt.Columns.Add(i.ToString());
            }
        }

        private void ucThongKe_Load(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            var table = new DataTable();
            for (int i = 0; i < 87; i++)
            {
                table.Columns.Add("Columns" + i.ToString());
            }
            //con.rfDtg4excel(table,dataGridView1);
            
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            radioButton1.Checked = true;

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
                con.thongke(label4, label6, label8,
                    label10, label12, comboBox1.Text);
            con.rfDtg4ThongKeChibo(dataGridView1,comboBox1.Text);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            comboBox2.Visible = true;
            Dictionary<string, string> test = new Dictionary<string, string>();
            
            test.Add("1", "Từ 25 đến 29");
            test.Add("2", "Từ 30 đến 34");
            test.Add("3", "Từ 35 đến 39");
            test.Add("4", "Từ 40 đến 44");
            test.Add("5", "Từ 45 đến 49");
            test.Add("6", "Từ 50 đến 55");
            test.Add("7", "Từ 55 đến 59");
            test.Add("8", "Từ 60 đến 69");
            test.Add("9", "Từ 70 đến 80");
            comboBox2.DataSource = new BindingSource(test, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            comboBox2.Visible = false;
            connectDb con = new connectDb();
            Dictionary<string, string> test = new Dictionary<string, string>();
            con.con.Open();
            test.Add("all", "Tất cả");
            using (SqlDataAdapter ada = new SqlDataAdapter("select ten,ma from chibo", con.con))
            {
                DataTable dtb = new DataTable();
                ada.Fill(dtb);

                foreach (DataRow dr in dtb.Rows)
                {
                    test.Add(dr[1].ToString(), dr[0].ToString());

                }
            }
            comboBox1.DataSource = new BindingSource(test, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            con.con.Close();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            connectDb con = new connectDb();
            con.thongkeTuoi(label4, label6, label8,
                    label10, label12, comboBox2.Text);
            con.rfDtg4ThongKeDoTuoi(dataGridView1, comboBox2.Text);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
