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
    public partial class UCDaoTaoChung3 : UserControl
    {
        public UCDaoTaoChung3()
        {
            InitializeComponent();
        }

        private static UCDaoTaoChung3 _instance;
        public static UCDaoTaoChung3 Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new UCDaoTaoChung3();
                }
                return _instance;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formThemVaThongTinDangVien dtvttdv = (formThemVaThongTinDangVien)Application.OpenForms["formThemVaThongTinDangVien"];
            dtvttdv.label1.Text = "II. TÓM TẮT QUÁ TRÌNH HOẠT ĐỘNG VÀ CÔNG TÁC";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucQuaTrinhHoatDongVaCongTac2.Instance);
            ucQuaTrinhHoatDongVaCongTac2.Instance.Dock = DockStyle.Fill;
            ucQuaTrinhHoatDongVaCongTac2.Instance.BringToFront();
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
            dtvttdv.label1.Text = "IV. ĐẶC ĐIỂM LỊCH SỬ VÀ QUAN HỆ VỚI NƯỚC NGOÀI";
            int x = (dtvttdv.panel2.Size.Width - dtvttdv.label1.Size.Width) / 2;
            dtvttdv.label1.Location = new Point(x, dtvttdv.label1.Location.Y);
            dtvttdv.panel1.Controls.Add(ucDdlsVaQhng4.Instance);
            ucDdlsVaQhng4.Instance.Dock = DockStyle.Fill;
            ucDdlsVaQhng4.Instance.BringToFront();
        }
    }
}