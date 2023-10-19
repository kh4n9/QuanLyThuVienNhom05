using QuanLyThuVienNhom05.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienNhom05
{
    public partial class Sach : Form
    {
        static ThuVienDBContext context = new ThuVienDBContext();
        List<SACH> listSach = context.SACHes.ToList();
        public Sach()
        {
            InitializeComponent();
            BindGrid(listSach);
        }

        private void BindGrid(List<SACH> listSach)
        {
            dgvShow.Rows.Clear();
            foreach (var item in listSach)
            {
                int index = dgvShow.Rows.Add();
                dgvShow.Rows[index].Cells[0].Value = item.MaSach;
                dgvShow.Rows[index].Cells[1].Value = item.TenSach;
                dgvShow.Rows[index].Cells[2].Value = item.TacGia;
                dgvShow.Rows[index].Cells[3].Value = item.NamXuatBan;
                dgvShow.Rows[index].Cells[4].Value = item.NhaXuatBan;
                dgvShow.Rows[index].Cells[5].Value = item.TriGia;
                dgvShow.Rows[index].Cells[6].Value = item.NgayNhap;
            }
        }

        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maSach = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            var selectedSach = context.SACHes.FirstOrDefault(s => s.MaSach.ToString() == maSach);
            txtTenSach.Text = selectedSach.TenSach.ToString();
            txtTacGia.Text = selectedSach.TacGia.ToString();
            txtNamXB.Text = selectedSach.NamXuatBan.ToString();
            txtNhaXB.Text = selectedSach.NhaXuatBan.ToString();
            txtTriGia.Text = selectedSach.TriGia.ToString();
            dtpNgayNhap.Value = selectedSach.NgayNhap.Value;
        }

        private bool MissingCheck()
        {
            if (txtTenSach.Text == "" || txtTacGia.Text == "" || txtNamXB.Text == "" || txtNhaXB.Text == "" || txtTriGia.Text == "")
            {
                return true;
            }
            return false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!MissingCheck())
            {
                SACH timSach = context.SACHes.FirstOrDefault(s => s.TenSach.Contains(txtTenSach.Text));
                if (timSach == null)
                {
                    SACH addSach = new SACH();
                    addSach.MaSach = 1;
                    addSach.TenSach = txtTenSach.Text;
                    addSach.TacGia = txtTacGia.Text;
                    addSach.NamXuatBan = int.Parse(txtNamXB.Text);
                    addSach.NhaXuatBan = txtNhaXB.Text;
                    addSach.TriGia = int.Parse(txtTriGia.Text);
                    addSach.NgayNhap = dtpNgayNhap.Value;
                    context.SACHes.Add(addSach);
                    context.SaveChanges();
                    listSach.Add(addSach);
                    BindGrid(listSach);
                    MessageBox.Show("Thêm sách thành công!");
                }
                else
                {
                    MessageBox.Show("Tên sách này đã tồn tại!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            var deleteSach = context.SACHes.FirstOrDefault(s => s.MaSach.ToString() == maSach);
            if (deleteSach != null)
            {
                context.SACHes.Remove(deleteSach);
                context.SaveChanges();
                listSach.Remove(deleteSach);
                BindGrid(listSach);
                txtTenSach.Text = "";
                txtTacGia.Text = "";
                txtNamXB.Text = "";
                txtNhaXB.Text = "";
                txtTriGia.Text = "";
                MessageBox.Show("Đã xóa thành công!");
            }
            else
            {
                MessageBox.Show("Không tồn tại sách có mã này!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!MissingCheck())
            {
                string maSach = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
                var updateSach = context.SACHes.FirstOrDefault(s => s.MaSach.ToString() == maSach);
                if (updateSach != null)
                {
                    listSach.Remove(updateSach);
                    updateSach.MaSach = 1;
                    updateSach.TenSach = txtTenSach.Text;
                    updateSach.TacGia = txtTacGia.Text;
                    updateSach.NamXuatBan = int.Parse(txtNamXB.Text);
                    updateSach.NhaXuatBan = txtNhaXB.Text;
                    updateSach.TriGia = int.Parse(txtTriGia.Text);
                    updateSach.NgayNhap = dtpNgayNhap.Value;
                    context.SaveChanges();
                    listSach.Add(updateSach);
                    BindGrid(listSach);
                    MessageBox.Show("Sửa thành công!");
                }
                else
                {
                    MessageBox.Show("Không tồn tại sách có mã này!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!");
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                BindGrid(listSach);
            }
            else
            {
                List<SACH> sachs = context.SACHes.Where(s => s.TenSach.Contains(txtTimKiem.Text) || s.TacGia.Contains(txtTimKiem.Text) || s.NhaXuatBan.Contains(txtTimKiem.Text)).ToList();
                BindGrid(sachs);
            }
        }

        private void dtpBatDau_ValueChanged(object sender, EventArgs e)
        {
            List<SACH> sachs = context.SACHes.Where(s => s.NgayNhap.Value >= dtpBatDau.Value && s.NgayNhap.Value <= dtpKetThuc.Value).ToList();
            BindGrid(sachs);
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            BindGrid(listSach);
        }

        private void txtNamXB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtNamXB_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in txtNamXB.Text)
            {
                if (!Char.IsNumber(c))
                {
                    MessageBox.Show("Chỉ được phép dán chuỗi có chứa số");
                    txtNamXB.Text = "";
                    break;
                }
            }
        }

        private void txtTriGia_TextChanged(object sender, EventArgs e)
        {
            foreach (var c in txtTriGia.Text)
            {
                if (!Char.IsNumber(c))
                {
                    MessageBox.Show("Chỉ được phép dán chuỗi có chứa số");
                    txtTriGia.Text = "";
                    break;
                }
            }
        }
    }
}
