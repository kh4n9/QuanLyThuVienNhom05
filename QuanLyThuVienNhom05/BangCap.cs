using QuanLyThuVienNhom05.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVienNhom05
{
    public partial class BangCap : Form
    {
        static Models.ThuVienDBContext context = new Models.ThuVienDBContext();
        List<BANGCAP> listBangCap = context.BANGCAPs.ToList();

        public BangCap()
        {
            InitializeComponent();
            BindGrid(listBangCap);
        }

        private void BindGrid(List<BANGCAP> listBangCap)
        {
            dgvShow.Rows.Clear();
            foreach (var item in listBangCap)
            {
                int index = dgvShow.Rows.Add();
                {
                    dgvShow.Rows[index].Cells[0].Value = item.MaBangCap;
                    dgvShow.Rows[index].Cells[1].Value = item.TenBangCap;
                }
            }
        }
        private void dgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            txtBangCap.Text = dgvShow.Rows[index].Cells[1].Value.ToString();
          
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            BANGCAP Tim = context.BANGCAPs.FirstOrDefault(s => s.TenBangCap.Contains(txtBangCap.Text));
            if(Tim == null)
            {
                BANGCAP addBangCap = new BANGCAP();
                addBangCap.TenBangCap = txtBangCap.Text;
                context.BANGCAPs.Add(addBangCap);
                context.SaveChanges();
                listBangCap.Add(addBangCap);
                BindGrid(listBangCap);
                MessageBox.Show("Thêm thành công " + addBangCap.TenBangCap + "!");
            } 
            else
            {
                MessageBox.Show("Đã có bằng cấp này!!");
            }    
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maBangCap = dgvShow.SelectedRows[0].Cells[0].Value.ToString();
                var updateBangCap = context.BANGCAPs.FirstOrDefault(s => s.MaBangCap.ToString() == maBangCap);
                if (updateBangCap != null)
                {
                    listBangCap.Remove(updateBangCap);
                    updateBangCap.TenBangCap = txtBangCap.Text;
                    context.SaveChanges();
                    listBangCap.Add(updateBangCap);
                    BindGrid(listBangCap);
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin không thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var deleteBangCap = context.BANGCAPs.FirstOrDefault(s => s.TenBangCap.Equals(txtBangCap.Text));
                if (deleteBangCap != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xoá ", "Cảnh báo", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        context.BANGCAPs.Remove(deleteBangCap);
                        context.SaveChanges();
                        var BANGCAPs = context.BANGCAPs.ToList();
                        BindGrid(BANGCAPs);
                        MessageBox.Show("Xóa thành công", "Thông báo");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Tên bằng cấp cần xoá không tồn tại", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void txtBangCap_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem phím được nhấn có phải là số không.
            if (char.IsDigit(e.KeyChar))
            {
                // Hiển thị hộp thông báo yêu cầu người dùng nhập ký tự không phải số.
                MessageBox.Show("Vui lòng nhập ký tự không phải số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Đặt tiêu điểm trở lại điều khiển.
                ((Control)sender).Focus();

                // Ngăn chặn việc nhấn phím được xử lý.
                e.Handled = true;
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if(txtTimKiem.Text == "")
            {
                BindGrid(listBangCap);
            }
            else
            {
                List<BANGCAP> BANGCAPs = context.BANGCAPs.Where(s => s.TenBangCap.Contains(txtTimKiem.Text)).ToList();
            }
        }
    }
}


                  