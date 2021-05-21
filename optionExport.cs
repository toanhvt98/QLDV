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
    public partial class optionExport : Form
    {
        DataSet ds = new DataSet();
        
        public optionExport()
        {
            InitializeComponent();
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

        private void optionExport_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton3.Checked = false;
            groupBox1.Enabled = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                groupBox1.Enabled = true;
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is RadioButton)
                    {
                        RadioButton r = (RadioButton)c;
                        r.Checked = false;
                    }
                    if (c is CheckBox)
                    {
                        CheckBox r = (CheckBox)c;
                        r.Checked = false;
                    }
                }
                radioButton3.Checked = true;
            }
            else
            {
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is RadioButton)
                    {
                        RadioButton r = (RadioButton)c;
                        r.Checked = false;
                    }
                    if (c is CheckBox)
                    {
                        CheckBox r = (CheckBox)c;
                        r.Checked = false;
                    }
                }

                radioButton3.Checked = false;
                groupBox1.Enabled = false;
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = "";
            string q1 ="";
            string q2 = "";
            foreach (Control c in groupBox1.Controls)
            {
                if(c is RadioButton)
                {
                    RadioButton r = (RadioButton)c;
                    if (r.Checked == true)
                        saveForExcel.l.Add(r.Text);
                }
                if (c is CheckBox)
                {
                    CheckBox r = (CheckBox)c;
                    if (r.Checked == true)
                        saveForExcel.l.Add(r.Text);
                }
            }
            

                if (comboBox1.SelectedIndex == 0)
                {
                    if (radioButton3.Checked == true)
                    {
                        saveForExcel.q = " where gioitinh =N'" + radioButton3.Text + "' ";
                    }
                    else
                    {
                        saveForExcel.q = " where gioitinh =N'" + radioButton4.Text + "' ";
                    }
                    if (checkBox1.Checked == true)
                    {
                        saveForExcel.q1 = " and giadinh != null ";
                    }
                    else
                        saveForExcel.q1 = "";
                    if (checkBox2.Checked == true)
                    {
                        saveForExcel.q2 = " and huyhieu != null ";
                    }
                    else
                        saveForExcel.q2 = "";
                }
                else
                {
                    if (radioButton3.Checked == true)
                    {
                        saveForExcel.q += " and gioitinh =N'" + radioButton3.Text + "' ";
                    }
                    else
                    {
                        saveForExcel.q += " and gioitinh =N'" + radioButton4.Text + "' ";
                    }
                    if (checkBox1.Checked == true)
                    {
                        saveForExcel.q1 = " and giadinh != null ";
                    }
                    else
                        saveForExcel.q1 = "";
                    if (checkBox2.Checked == true)
                    {
                        saveForExcel.q2 = " and huyhieu != null ";
                    }
                    else
                        saveForExcel.q2 = "";
                
            }
            DataTable tb1 = new DataTable();
            tb1.TableName = "Thông tin Đảng viên";
            DataTable tb2 = new DataTable();
            tb2.TableName = "";

            connectDb con = new connectDb();
            if (checkBox3.Checked == false)
            {
                if(radioButton1.Checked == true)
                    con.alterDatatable(tb1, comboBox1.Text, saveForExcel.q, saveForExcel.q1, saveForExcel.q2);
                if(radioButton2.Checked == true)
                    con.alterDatatable1(tb1, comboBox2.Text, saveForExcel.q, saveForExcel.q1, saveForExcel.q2);
            }
            else
            {
                if (radioButton1.Checked == true)
                    con.alterDatatable(tb1, comboBox1.Text, saveForExcel.q, saveForExcel.q1, saveForExcel.q2);
                if (radioButton2.Checked == true)
                    con.alterDatatable1(tb1, comboBox2.Text, saveForExcel.q, saveForExcel.q1, saveForExcel.q2);
            }
            MessageBox.Show(saveForExcel.q+saveForExcel.q1+saveForExcel.q2);
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
            
        }
    }
}
