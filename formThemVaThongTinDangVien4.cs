﻿using System;
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
    public partial class formThemVaThongTinDangVien4 : Form
    {

        public static bool check = false;
        public bool dt1 = false;
        public bool dt2 = false;
        public bool dt3 = false;
        public bool dt4 = false;
        public bool dt5 = false;
        public bool dt6 = false;
        public bool dt7 = false;
        public bool dt8 = false;
        public formThemVaThongTinDangVien4()
        {
            InitializeComponent();
        }

        private void formThemVaThongTinDangVien4_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker3.ValueChanged += new EventHandler(dateTimePicker3_ValueChanged);
            dateTimePicker4.ValueChanged += new EventHandler(dateTimePicker4_ValueChanged);
            dateTimePicker5.ValueChanged += new EventHandler(dateTimePicker5_ValueChanged);
            dateTimePicker6.ValueChanged += new EventHandler(dateTimePicker6_ValueChanged);
            dateTimePicker7.ValueChanged += new EventHandler(dateTimePicker7_ValueChanged);
            dateTimePicker8.ValueChanged += new EventHandler(dateTimePicker8_ValueChanged);
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            dateTimePicker3.MaxDate = DateTime.Now;
            dateTimePicker4.MaxDate = DateTime.Now;
            dateTimePicker5.MaxDate = DateTime.Now;
            dateTimePicker6.MaxDate = DateTime.Now;
            dateTimePicker7.MaxDate = DateTime.Now;
            dateTimePicker8.MaxDate = DateTime.Now;
            connectDb con = new connectDb();
            con.themChiBoCombo(comboBox1);
            con.themChiBoCombo(comboBox2);
            con.themChiBoCombo(comboBox3);
            con.themChiBoCombo(comboBox4);

            enableGrb(checkBox1, groupBox1);
            enableGrb(checkBox2, groupBox2);

            if(check == true)
            {
                getInfor();
            }
        }


        private void enableGrb(CheckBox cb, GroupBox grb)
        {
            grb.Enabled = cb.Checked;
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dt1 = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dt2 = true;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            dt3 = true;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            dt4 = true;
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            dt5 = true;
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            dt6 = true;
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            dt7 = true;
        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            dt8 = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            enableGrb(checkBox1, groupBox1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            enableGrb(checkBox2, groupBox2);
        }

        private void setInfor()
        {
            dangvien.bixoaten = textBox1.Text;
            if (dt1 == true)
            {
                dangvien.thoigian = dateTimePicker1.Value;
            }
            else
            {
                dangvien.thoigian = null;
            }

            dangvien.xoataichibo = comboBox1.SelectedText.ToString();
            dangvien.ketnaplai = textBox3.Text;

            if (dt2 == true)
            {
                dangvien.ngayvao = dateTimePicker2.Value;
            }
            else
            {
                dangvien.ngayvao = null;
            }


            dangvien.vaochibo = comboBox2.SelectedText.ToString();
            dangvien.vaonguoigt1 = textBox5.Text;
            dangvien.vaochucvu1 = textBox6.Text;
            dangvien.vaodonvi1 = textBox7.Text;
            dangvien.vaonguoigt2 = textBox8.Text;
            dangvien.vaochucvu2 = textBox10.Text;
            dangvien.vaodonvi2 = textBox9.Text;

            if (dt3 == true)
            {
                dangvien.ngaychinhthuc2 = dateTimePicker3.Value;
            }
            else
            {
                dangvien.ngaychinhthuc2 = null;
            }


            dangvien.vaochibo2 = comboBox3.SelectedText.ToString();

            if (dt4 == true)
            {
                dangvien.ngaykhoiphucdangtich = dateTimePicker4.Value;
            }
            else
            {
                dangvien.ngaykhoiphucdangtich = null;
            }

            dangvien.vaochibo3 = comboBox4.SelectedText.ToString();

            if (dt5 == true)
            {
                dangvien.ngaybikyluat = dateTimePicker5.Value;
            }
            else
            {
                dangvien.ngaybikyluat = null;
            }

            dangvien.thongtinkyluat = richTextBox5.Text;

            if (dt6 == true)
            {
                dangvien.ngaylamviecchedocu = dateTimePicker6.Value;
            }
            else
            {
                dangvien.ngaylamviecchedocu = null;
            }

            dangvien.thongtinchedocu = richTextBox2.Text;

            if (dt7 == true)
            {
                dangvien.dinuocngoaitu = dateTimePicker7.Value;
            }
            else
            {
                dangvien.dinuocngoaitu = null;
            }

            if (dt8 == true)
            {
                dangvien.dinuocngoaiden = dateTimePicker8.Value;
            }
            else
            {
                dangvien.dinuocngoaiden = null;
            }

            dangvien.thongtindinuocngoai = richTextBox1.Text;
            dangvien.thamgiatochucnuocngoai = richTextBox3.Text;
            dangvien.nguoithannuocngoai = richTextBox4.Text;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien3 f = (formThemVaThongTinDangVien3)Application.OpenForms["formThemVaThongTinDangVien3"];
            if (f == null)
            {
                f = new formThemVaThongTinDangVien3();
                f.AutoScroll = true;
            }
            f.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            setInfor();
            formThemVaThongTinDangVien5 f = (formThemVaThongTinDangVien5)Application.OpenForms["formThemVaThongTinDangVien5"];
            if (f == null)
            {
                f = new formThemVaThongTinDangVien5();
                f.AutoScroll = true;
            }
            f.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void formThemVaThongTinDangVien4_FormClosed(object sender, FormClosedEventArgs e)
        {
            usercontrolForm.closeForm();
        }

        private void getInfor()
        {
            connectDb con = new connectDb();
            con.con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from dacdiemlichsu where solylich='" + dangvien.solylich + "'", con.con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        textBox1.Text = read.GetString(2);

                        if (!read.IsDBNull(3))
                        {
                            dateTimePicker1.Value = read.GetDateTime(3);
                        }
                        else
                        {
                            dateTimePicker1.Value = DateTime.Today;
                        }

                        comboBox1.Text = read.GetString(4);
                        textBox3.Text = read.GetString(5);

                        if (!read.IsDBNull(6))
                        {
                            dateTimePicker2.Value = read.GetDateTime(6);
                        }
                        else
                        {
                            dateTimePicker2.Value = DateTime.Today;
                        }

                        comboBox2.Text = read.GetString(7);
                        textBox5.Text = read.GetString(8);
                        textBox6.Text = read.GetString(9);
                        textBox7.Text = read.GetString(10);
                        textBox8.Text = read.GetString(11);
                        textBox10.Text = read.GetString(12);
                        textBox9.Text = read.GetString(13);

                        if (!read.IsDBNull(14))
                        {
                            dateTimePicker3.Value = read.GetDateTime(14);
                        }
                        else
                        {
                            dateTimePicker3.Value = DateTime.Today;
                        }

                        comboBox3.Text = read.GetString(15);

                        if (!read.IsDBNull(16))
                        {
                            dateTimePicker4.Value = read.GetDateTime(14);
                        }
                        else
                        {
                            dateTimePicker4.Value = DateTime.Today;
                        }

                        comboBox4.Text = read.GetString(15);

                        if (!read.IsDBNull(16))
                        {
                            dateTimePicker5.Value = read.GetDateTime(16);
                        }
                        else
                        {
                            dateTimePicker5.Value = DateTime.Today;
                        }

                        richTextBox5.Text = read.GetString(17);

                        if (!read.IsDBNull(18))
                        {
                            dateTimePicker6.Value = read.GetDateTime(18);
                        }
                        else
                        {
                            dateTimePicker6.Value = DateTime.Today;
                        }

                        richTextBox2.Text = read.GetString(19);
                    }
                }
            }

            using (SqlCommand cmd = new SqlCommand("select * from qhnuocngoai where solylich='" + dangvien.solylich + "'", con.con))
            {
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        if (!read.IsDBNull(2))
                        {
                            dateTimePicker7.Value = read.GetDateTime(2);
                        }
                        else
                        {
                            dateTimePicker7.Value = DateTime.Today;
                        }

                        if (!read.IsDBNull(3))
                        {
                            dateTimePicker8.Value = read.GetDateTime(3);
                        }
                        else
                        {
                            dateTimePicker8.Value = DateTime.Today;
                        }

                        richTextBox1.Text = read.GetString(4);
                        richTextBox3.Text = read.GetString(5);
                        richTextBox4.Text = read.GetString(6);
                    }
                }
            }
            con.con.Close();
        }

        private void formThemVaThongTinDangVien4_FormClosing(object sender, FormClosingEventArgs e)
        {
            usercontrolForm.closeForm();
        }
    }
}
