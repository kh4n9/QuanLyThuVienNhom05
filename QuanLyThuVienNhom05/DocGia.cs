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
    public partial class DocGia : Form
    {
        static ThuVienDBContext context = new ThuVienDBContext();
        List<DOCGIA> listDocGia = context.DOCGIAs.ToList();
        public DocGia()
        {
            InitializeComponent();
        }
    }
}
