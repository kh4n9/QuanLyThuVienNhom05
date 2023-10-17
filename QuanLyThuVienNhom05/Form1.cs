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
    public partial class From1 : Form
    {
        public From1()
        {
            InitializeComponent();
        }

        private void bằngCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BangCap bangCap = new BangCap();
            bangCap.MdiParent = this;
            bangCap.Show();
        }

        private void đọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocGia docGia = new DocGia();
            docGia.MdiParent = this;
            docGia.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.MdiParent = this;
            nhanVien.Show();
        }
    }
}
