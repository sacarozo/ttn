﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace project
{
    public partial class FormTrangChu : Form
    {
        public FormTrangChu()
        {
            InitializeComponent();
        }
        public static string Manhanvien,quyenhan;
        string tenhanvien = "";
        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            if (quyenhan != "Admin")
            {
                quảnLýNToolStripMenuItem.Enabled = false;
            }
            string sqlnhanvien = @"select * from NHANVIEN";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            int demnhanvien = dtnhanvien.Rows.Count;
            for (int i = 0; i < demnhanvien; i++)
            {
                if (dtnhanvien.Rows[i][0].ToString() == Manhanvien)
                {
                    tenhanvien = dtnhanvien.Rows[i][1].ToString();
                    break;
                }
            }
            sttloichao.Text = "-- Xin chào " + tenhanvien + ". Cảm ơn bạn đã lựa chọn sử dụng phần mềm của tôi --";
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangnhap frm = new FormDangnhap();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void thôngTinNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThongkenhanvien frm = new FormThongkenhanvien();
            frm.ShowDialog();
        }

        private void thôngTinSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThongkehanghoa frm = new FormThongkehanghoa();
            frm.ShowDialog();
        }

        private void thôngTinHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThongkehoadon frm = new FormThongkehoadon();
            frm.ShowDialog();
        }

        private void Thongkedoanhthu_Click(object sender, EventArgs e)
        {
            FormThongkedoanhthutungmathang frm = new FormThongkedoanhthutungmathang();
            frm.ShowDialog();
        }

        private void btnDoanhthu_Click(object sender, EventArgs e)
        {
            FormThongkedoanhthutungmathang frm = new FormThongkedoanhthutungmathang();
            frm.ShowDialog();
        }

        private void nhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormNhanvien frm = new FormNhanvien();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void hóaĐơnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormBanhang frm = new FormBanhang();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void btnBanhang_Click(object sender, EventArgs e)
        {
            FormBanhang frm = new FormBanhang();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void sảnPhẩmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormHanghoa frm = new FormHanghoa();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void phòngBanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPhongban frm = new FormPhongban();
            frm.ShowDialog();
        }

        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChucvu frm = new FormChucvu();
            frm.ShowDialog();
        }

        private void loạiSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoaisanpham frm = new FormLoaisanpham();
            frm.ShowDialog();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNhacungcap frm = new FormNhacungcap();
            frm.ShowDialog();
        }

        private void quảnLýNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQuanlynguoidung frm = new FormQuanlynguoidung();
            frm.ShowDialog();
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            FormNhanvien frm = new FormNhanvien();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHoadon frm = new FormHoadon();
            frm.ShowDialog();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKhachhang frm = new FormKhachhang();
            frm.ShowDialog();
        }

        private void btnKhohang_Click(object sender, EventArgs e)
        {
            FormHanghoa frm = new FormHanghoa();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void f1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTroGiup frm = new FormTroGiup();
            frm.ShowDialog();
        }
        
    }
}
