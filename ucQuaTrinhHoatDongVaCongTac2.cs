﻿using System;
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
    public partial class ucQuaTrinhHoatDongVaCongTac2 : UserControl
    {
        public ucQuaTrinhHoatDongVaCongTac2()
        {
            InitializeComponent();
        }
        private static ucQuaTrinhHoatDongVaCongTac2 _instance;
        public static ucQuaTrinhHoatDongVaCongTac2 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ucQuaTrinhHoatDongVaCongTac2();
                }
                return _instance;
            }
        }
        private void ucQuaTrinhHoatDongVaCongTac_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "I. THÔNG TIN CƠ BẢN";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucTTCBDangVien.Instance);
            ucTTCBDangVien.Instance.Dock = DockStyle.Fill;
            ucTTCBDangVien.Instance.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                dtvttdv.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "III. ĐÀO TẠO, BỒI DƯỠNG VỀ CHUYÊN MÔN, NGHIỆP VỤ, LÝ LUẬN CHÍNH TRỊ, NGOẠI NGỮ";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(UCDaoTaoChung3.Instance);
            UCDaoTaoChung3.Instance.Dock = DockStyle.Fill;
            UCDaoTaoChung3.Instance.BringToFront();
        }
    }
}