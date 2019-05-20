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
    public partial class FormNhanvien : Form
    {
        public FormNhanvien()
        {
            InitializeComponent();
        }

        private void FormNhanvien_Load(object sender, EventArgs e)
        {
            Loadnhanvien();
            dgvNhanvien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtpNgaysinh.Format = DateTimePickerFormat.Custom;
            dtpNgaysinh.CustomFormat = "MM/dd/yyyy";

            string sqlphongban = @"select * from PHONGBAN";
            DataTable dtphongban = DataProvider.LoadCSDL(sqlphongban);
            cmbPhongban.DataSource = dtphongban;
            cmbPhongban.DisplayMember = "Tenphongban";
            cmbPhongban.ValueMember = "Maphongban";

            string sqlchucvu = @"select * from CHUCVU";
            DataTable dtchucvu = DataProvider.LoadCSDL(sqlchucvu);
            cmbChucvu.DataSource = dtchucvu;
            cmbChucvu.DisplayMember = "Tenchucvu";
            cmbChucvu.ValueMember = "Machucvu";
            cmbGioitinh.SelectedIndex = 0;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        void Loadnhanvien()
        {
            string sqlnhanvien = @"select nv.Manhanvien,nv.Tennhanvien, nv.Gioitinh,nv.Ngaysinh,nv.Diachi,nv.Sodienthoai,pb.Tenphongban,cv.Tenchucvu
                                  from NHANVIEN nv, PHONGBAN pb, CHUCVU cv
                                  where nv.Maphongban = pb.Maphongban and nv.Machucvu = cv.Machucvu";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            dgvNhanvien.DataSource = dtnhanvien;
            lblTongnhanvien.Text = dtnhanvien.Rows.Count.ToString();
        }

        private void btnManhanvien_Click(object sender, EventArgs e)
        {
            string sqlnhanvien = @"select nv.Manhanvien,nv.Tennhanvien, nv.Gioitinh,nv.Ngaysinh,nv.Diachi,nv.Sodienthoai,pb.Tenphongban,cv.Tenchucvu
                                  from NHANVIEN nv, PHONGBAN pb, CHUCVU cv
                                  where nv.Maphongban = pb.Maphongban and nv.Machucvu = cv.Machucvu";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            int demnhanvien = dtnhanvien.Rows.Count;
            if (demnhanvien == 0)
            {
                txtManhanvien.Text = "1";
            }
            else if (demnhanvien == 1)
            {
                txtManhanvien.Text = "2";
            }
            else
            {
                bool kiemtrama = true;
                for (int i = 0; i < demnhanvien - 1; i++)
                {
                    if ((int.Parse(dtnhanvien.Rows[i + 1][0].ToString()) - int.Parse(dtnhanvien.Rows[i][0].ToString())) >= 2)
                    {
                        txtManhanvien.Text = (int.Parse(dtnhanvien.Rows[i][0].ToString()) + 1).ToString();
                        kiemtrama = false;
                        break;
                    }
                }
                if (kiemtrama == true)
                {
                    txtManhanvien.Text = (demnhanvien + 1).ToString();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bool kiemtranhaplieu = KiemTraNhapDuLieu();
            bool kiemtranghiepvu = true;
            if (kiemtranhaplieu == true)
            {
                // Kiểm tra mã Nhân Viên
                string sqlnhanvien = @"select sv.Manhanvien from NHANVIEN sv";
                DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
                int demnhanvien = dtnhanvien.Rows.Count;
                for (int i = 0; i < demnhanvien; i++)
                {
                    if (txtManhanvien.Text == dtnhanvien.Rows[i][0].ToString())
                    {
                        MessageBox.Show("Mã Nhân Viên Đã Bị Trùng");
                        kiemtranghiepvu = false;
                        txtManhanvien.Clear();
                        txtManhanvien.Focus();
                        break;
                    }
                }
                if (kiemtranghiepvu == true)
                {
                    if (MessageBox.Show("Xác Nhận Thêm Nhân Viên Này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            string sqlthemnhanvien = @"insert into NHANVIEN(Manhanvien,Tennhanvien,Diachi,Gioitinh,Ngaysinh,Sodienthoai,Maphongban,Machucvu)
                                               values('" + int.Parse(txtManhanvien.Text) + "', N'" + txtTennhanvien.Text + "', N'" + txtDiachi.Text + "', N'" + cmbGioitinh.SelectedItem.ToString() + "', '" + dtpNgaysinh.Value + "', '" + txtSodienthoai.Text + "', '" + int.Parse(cmbPhongban.SelectedValue.ToString()) + "', '" + int.Parse(cmbChucvu.SelectedValue.ToString()) + "')";
                            int ketquathem = DataProvider.change(sqlthemnhanvien);
                            if (ketquathem > 0)
                            {
                                Loadnhanvien();
                                MessageBox.Show("Thêm Nhân Viên Thành Công", "Thêm Nhân Viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtManhanvien.Text = "";
                                txtTennhanvien.Text = "";
                                txtDiachi.Text = "";
                                txtSodienthoai.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Thêm Nhân Viên Thất Bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Thêm Nhân Viên Thất Bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private bool KiemTraNhapDuLieu()
        {
            bool kiemtranhaplieu = true;
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Chưa Nhập Mã Nhân Viên");
                kiemtranhaplieu = false;
                txtManhanvien.Focus();
            }
            else if (txtTennhanvien.Text == "")
            {
                MessageBox.Show("Chưa Nhập Tên Nhân Viên");
                kiemtranhaplieu = false;
                txtTennhanvien.Focus();
            }
            else if (txtDiachi.Text == "")
            {
                MessageBox.Show("Chưa Nhập Địa Chỉ");
                kiemtranhaplieu = false;
                txtDiachi.Focus();
            }
            else if (txtSodienthoai.Text == "")
            {
                MessageBox.Show("Chưa Nhập Số Điện Thoại");
                kiemtranhaplieu = false;
                txtSodienthoai.Focus();
            }
            return kiemtranhaplieu;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            int chisodongdangchon = dgvNhanvien.CurrentCell.RowIndex;
            string sqlnhanvien = @"select nv.Manhanvien from NHANVIEN nv";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            int demnhanvien = dtnhanvien.Rows.Count;
            for (int i = 0; i < demnhanvien; i++)
            {
                if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumManhanvien"].Value.ToString().Trim() == dtnhanvien.Rows[i][0].ToString())
                {
                    // sửa từ dưới này
                    if (KiemTraNhapDuLieu())
                    {
                        if (MessageBox.Show("Xác Nhận Thay Đổi Thông Tin Nhân Viên?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                string sqlsua = @"update NHANVIEN
                                            set Tennhanvien = N'" + txtTennhanvien.Text + "', Diachi = N'" + txtDiachi.Text + "', Gioitinh = N'" + cmbGioitinh.SelectedItem.ToString() + "', Ngaysinh = '" + dtpNgaysinh.Value + "', Sodienthoai = '" + txtSodienthoai.Text + "', Maphongban = '" + int.Parse(cmbPhongban.SelectedValue.ToString()) + "', Machucvu = '" + int.Parse(cmbChucvu.SelectedValue.ToString()) + "' where NHANVIEN.Manhanvien = '" + int.Parse(txtManhanvien.Text) + "'";
                                int ketquasua = DataProvider.change(sqlsua);
                                if (ketquasua > 0)
                                {
                                    Loadnhanvien();
                                    MessageBox.Show("Cập Nhật Thông Tin Nhân Viên Thành Công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtManhanvien.Enabled = true;
                                    txtManhanvien.Text = "";
                                    txtTennhanvien.Text = "";
                                    txtDiachi.Text = "";
                                    txtSodienthoai.Text = "";
                                    btnSua.Enabled = false;
                                    btnXoa.Enabled = false;
                                }
                                else
                                {
                                    MessageBox.Show("Cập Nhật Nhân Viên Thất Bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Cập Nhật Nhân Viên Thất Bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool kiemtrahoadon = true;
            int chisodongdangchon = dgvNhanvien.CurrentCell.RowIndex;
            string sqlhoadon = @"select * from HOADON";
            DataTable dthoadon = DataProvider.LoadCSDL(sqlhoadon);
            int demhoadon = dthoadon.Rows.Count;
            for (int i = 0; i < demhoadon; i++)
            {
                if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumManhanvien"].Value.ToString().Trim() == dthoadon.Rows[i][2].ToString())
                {
                    kiemtrahoadon = false;
                    break;
                }
            }
            if (kiemtrahoadon == false)
            {
                MessageBox.Show("Không Được Phép Xóa Nhân Viên Này Khi Dữ Liệu Đang Bị Ràng Buộc Với Hóa Đơn. Xin Kiểm Tra Lại!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // được phép xóa
                if (MessageBox.Show("Xác Nhận Chắc Chắn Xóa Nhân Viên Này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string sqlxoanhanvien = @"delete from NHANVIEN where NHANVIEN.Manhanvien = '" + int.Parse(txtManhanvien.Text) + "'";
                        int ketquaxoa = DataProvider.change(sqlxoanhanvien);
                        if (ketquaxoa > 0)
                        {
                            Hoantac();
                            MessageBox.Show("Xóa Thành Công.", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Xóa Thất Bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Xóa Thất Bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        void Hoantac()
        {
            Loadnhanvien();
            txtManhanvien.Enabled = true;
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            txtDiachi.Text = "";
            txtSodienthoai.Text = "";
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        private void btnHoantac_Click(object sender, EventArgs e)
        {
            Hoantac();
        }

        private void btnInnhanvien_Click(object sender, EventArgs e)
        {
            FormInnhanvien frm = new FormInnhanvien();
            frm.ShowDialog();
        }

        private void dgvNhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtManhanvien.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            int chisodongdangchon = dgvNhanvien.CurrentCell.RowIndex;
            string sqlnhanvien = @"select nv.Manhanvien, nv.Tennhanvien, nv.Gioitinh, nv.Ngaysinh, nv.Diachi, nv.Sodienthoai, pb.Maphongban, cv.Machucvu 
                            from NHANVIEN nv, PHONGBAN pb, CHUCVU cv
                            where nv.Machucvu = cv.Machucvu and nv.Maphongban = pb.Maphongban";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            int demnhanvien = dtnhanvien.Rows.Count;
            for (int i = 0; i < demnhanvien; i++)
            {
                if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumManhanvien"].Value.ToString().Trim() == dtnhanvien.Rows[i][0].ToString())
                {
                    txtManhanvien.Text = dgvNhanvien.Rows[chisodongdangchon].Cells["ColumManhanvien"].Value.ToString().Trim();
                    txtTennhanvien.Text = dgvNhanvien.Rows[chisodongdangchon].Cells["ColumTennhanvien"].Value.ToString().Trim();
                    txtDiachi.Text = dgvNhanvien.Rows[chisodongdangchon].Cells["ColumDiachi"].Value.ToString().Trim();
                    txtSodienthoai.Text = dgvNhanvien.Rows[chisodongdangchon].Cells["ColumSodienthoai"].Value.ToString().Trim();
                    if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumGioitinh"].Value.ToString().Trim() == "Nam")
                    {
                        cmbGioitinh.SelectedIndex = 0;
                    }
                    else if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumGioitinh"].Value.ToString().Trim() == "Nữ")
                    {
                        cmbGioitinh.SelectedIndex = 1;
                    }
                    else
                    {
                        cmbGioitinh.SelectedIndex = 2;
                    }

                    string sqlphongban = @"select * from PHONGBAN";
                    DataTable dtphongban = DataProvider.LoadCSDL(sqlphongban);
                    int demphongban = dtphongban.Rows.Count;
                    for (int j = 0; j < demphongban; j++)
                    {
                        if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumTenphongban"].Value.ToString().Trim() == dtphongban.Rows[j][1].ToString())
                        {
                            cmbPhongban.SelectedIndex = j;
                            break;
                        }
                    }
                    string sqlchucvu = @"select * from CHUCVU";
                    DataTable dtchucvu = DataProvider.LoadCSDL(sqlchucvu);
                    int demchucvu = dtchucvu.Rows.Count;
                    for (int j = 0; j < demchucvu; j++)
                    {
                        if (dgvNhanvien.Rows[chisodongdangchon].Cells["ColumTenchucvu"].Value.ToString().Trim() == dtchucvu.Rows[j][1].ToString())
                        {
                            cmbChucvu.SelectedIndex = j;
                            break;
                        }
                    }
                    dtpNgaysinh.Value = DateTime.Parse(dtnhanvien.Rows[i][3].ToString());
                    break;
                }
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            //FormTimkiemnhanvien frm = new FormTimkiemnhanvien();
            //if (txtTimkiem.Text != "")
            //{
            //    if (Timkiemthaoma(txtTimkiem.Text))
            //    {
            //        frm.Manhanvien = txtTimkiem.Text;
            //        frm.kieutimkiem = 1;
            //        frm.ShowDialog();
            //    }
            //    else if (Timkiemthaoten(txtTimkiem.Text))
            //    {
            //        frm.Tennhanvien = txtTimkiem.Text;
            //        frm.kieutimkiem = 2;
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không Tồn Tại Nhân Viên Có Thông Tin Này.");
            //    }
           // }
            string timkiem = @"select * from NHANVIEN where Tennhanvien like N'%" + txtTimkiem.Text + "%' or Manhanvien like'%" + txtTimkiem.Text + "%'";
            DataTable dttimkiem = DataProvider.LoadCSDL(timkiem);
            dgvNhanvien.DataSource = dttimkiem;
        }
        bool Timkiemthaoma(string matimkiem)
        {
            matimkiem = matimkiem.Trim().ToLower();
            string sqlnhanvien = @"select nv.Manhanvien from NHANVIEN nv";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            int demnhanvien = dtnhanvien.Rows.Count;
            for (int i = 0; i < demnhanvien; i++)
            {
                if (matimkiem == dtnhanvien.Rows[i][0].ToString())
                {
                    return true;
                }
            }
            return false;
        }
        bool Timkiemthaoten(string tentimkiem)
        {
            tentimkiem = tentimkiem.Trim().ToLower();
            string sqlnhanvien = @"select nv.Tennhanvien from NHANVIEN nv";
            DataTable dtnhanvien = DataProvider.LoadCSDL(sqlnhanvien);
            int demnhanvien = dtnhanvien.Rows.Count;
            for (int i = 0; i < demnhanvien; i++)
            {
                if (tentimkiem == dtnhanvien.Rows[i][0].ToString().ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void txtSodienthoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtManhanvien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnTrangchu_Click(object sender, EventArgs e)
        {
            FormTrangChu frm = new FormTrangChu();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvNhanvien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
