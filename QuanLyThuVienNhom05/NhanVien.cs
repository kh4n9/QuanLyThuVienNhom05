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
    public partial class NhanVien : Form
    {
        static ThuVienDBContext context = new ThuVienDBContext();
        List<NHANVIEN> listNhanVien = context.NHANVIENs.ToList();
        List<BANGCAP> listBangCap = context.BANGCAPs.ToList();
        public NhanVien()
        {
            InitializeComponent();
            BindGrid(listNhanVien);
            LoadCBB(listBangCap);
        }
        private void BindGrid(List<NHANVIEN> list)
        {
            dgvShow.Rows.Clear();
            foreach (var item  in list)
            {
                int index = dgvShow.Rows.Add();
                dgvShow.Rows[index].Cells[0].Value = item.MaNhanVien;
                dgvShow.Rows[index].Cells[1].Value = item.HoTenNhanVien;
                dgvShow.Rows[index].Cells[2].Value = item.NgaySinh;
                dgvShow.Rows[index].Cells[3].Value = item.DiaChi;
                dgvShow.Rows[index].Cells[4].Value = item.DienThoai;
                dgvShow.Rows[index].Cells[5].Value = item.BANGCAP.TenBangCap;
            }
        }

        private void LoadCBB(List<BANGCAP> list)
        {

            cbbBangCap.DataSource = list;
            cbbBangCap.DisplayMember = "TenBangCap";
            cbbBangCap.ValueMember = "MaBangCap";

            cbbLocBangCap.DataSource = list;
            cbbLocBangCap.DisplayMember = "TenBangCap";
            cbbLocBangCap.ValueMember = "MaBangCap";
        }

        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maNhanVien = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            var nhanVien = context.NHANVIENs.FirstOrDefault(n => n.MaNhanVien.ToString() == maNhanVien);
            txtHoten.Text = nhanVien.HoTenNhanVien.ToString();
            dtpNgaySinh.Value = nhanVien.NgaySinh.Value;
            txtDiaChi.Text = nhanVien.DiaChi.ToString();
            txtSdt.Text = nhanVien.DienThoai.ToString();
            cbbBangCap.SelectedValue = nhanVien.MaBangCap;
        }

        private bool KiemTraDuThongTin()
        {
            if (txtHoten.Text == "" || txtDiaChi.Text == "" || txtSdt.Text == "")
            {
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraDuThongTin())
            {
                NHANVIEN addNhanVien = new NHANVIEN();
                addNhanVien.MaNhanVien = 1;
                addNhanVien.HoTenNhanVien = txtHoten.Text;
                addNhanVien.NgaySinh = dtpNgaySinh.Value;
                addNhanVien.DiaChi = txtDiaChi.Text;
                addNhanVien.DienThoai = txtSdt.Text;
                addNhanVien.MaBangCap = int.Parse(cbbBangCap.SelectedValue.ToString());
                context.NHANVIENs.Add(addNhanVien);
                listNhanVien.Add(addNhanVien);
                context.SaveChanges();
                BindGrid(listNhanVien);
                MessageBox.Show("Thêm thành công nhân viên");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            var deleteNhanVien = context.NHANVIENs.FirstOrDefault(n => n.MaNhanVien.ToString() == maNhanVien);
            if (deleteNhanVien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa");
            }
            else
            {
                if (MessageBox.Show("Xác nhận xóa nhân viên này","Cảnh báo",MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    context.NHANVIENs.Remove(deleteNhanVien);
                    context.SaveChanges();
                    listNhanVien.Remove(deleteNhanVien);
                    BindGrid(listNhanVien);
                    MessageBox.Show("Xóa nhân viên thành công");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhanVien = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            var updateNhanVien = context.NHANVIENs.FirstOrDefault(n => n.MaNhanVien.ToString() == maNhanVien);
            if (updateNhanVien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa");
            }
            else
            {
                updateNhanVien.HoTenNhanVien = txtHoten.Text;
                updateNhanVien.NgaySinh = dtpNgaySinh.Value;
                updateNhanVien.DiaChi = txtDiaChi.Text;
                updateNhanVien.DienThoai = txtSdt.Text;
                updateNhanVien.MaBangCap = int.Parse(cbbBangCap.SelectedValue.ToString());
                context.SaveChanges();
                listNhanVien.Clear();
                listNhanVien = context.NHANVIENs.ToList();
                BindGrid(listNhanVien);
                MessageBox.Show("Sửa nhân viên thành công");
            }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            if (txtTim.Text == "")
            {
                BindGrid(listNhanVien);
            }
            else
            {
                List<NHANVIEN> nhanViens = context.NHANVIENs.Where(n => n.HoTenNhanVien.Contains(txtTim.Text) || n.DienThoai.Contains(txtTim.Text) || n.MaNhanVien.ToString().Contains(txtTim.Text)).ToList();
                BindGrid(nhanViens);
            }
        }

        private void btnResetLoc_Click(object sender, EventArgs e)
        {
            BindGrid(listNhanVien);
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            var cbbValue = cbbLocBangCap.SelectedValue.ToString();
            List<NHANVIEN> nhanViens = context.NHANVIENs.Where(n => n.MaBangCap.Value.ToString() == cbbValue).ToList();
            BindGrid(nhanViens);
        }
    }
}
