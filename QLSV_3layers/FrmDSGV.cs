using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3layers
{
    public partial class FrmDSGV : Form
    {
        public FrmDSGV()
        {
            InitializeComponent();
        }
        private string tukhoa = "";

        private void dgvGiaoVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadGiaoVien()
        {
            //khaibao toan bo giao vien
            //khai bao list customparameter
            List<CustomParameter> listPara = new List<CustomParameter>();
            listPara.Add(new CustomParameter()
            {
                //set tu khoa bang null
                key="@tukhoa",
                value = tukhoa
            });
            dgvGiaoVien.DataSource = new Database().SelectData("SelectAllGiaoVien", listPara);

            //custom ten cot
            dgvGiaoVien.Columns["MaGiaoVien"].HeaderText = "Mã GV";
            dgvGiaoVien.Columns["HoTen"].HeaderText = "Họ tên đầy đủ";
            dgvGiaoVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvGiaoVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvGiaoVien.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dgvGiaoVien.Columns["Email"].HeaderText = "Email Cá Nhân";
            dgvGiaoVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";

        }

        private void dgvGiaoVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //y tuong khi double click vao giao vien tren dgv
            //se hien ra form cap nhat thong tin
            //de cap nhat dc ta can lay dc ma giao vien
            if(e.RowIndex>=0)
            {
                var mgv = dgvGiaoVien.Rows[e.RowIndex].Cells["MaGiaoVien"].Value.ToString();
                //can truyen ong nay sang cho frmGiaoVien
                new FrmGiaoVien(mgv).ShowDialog();
                //sau khi cap nhat xong thi load lai datagrv
                LoadGiaoVien();
            
            }
        }

        private void FrmDSGV_Load(object sender, EventArgs e)
        {
            LoadGiaoVien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //tim kiem thi giong y het form loaddata 
            //chi khac la ta se truyen gia tri cua tu khoa = txtTimKiem.text
            List<CustomParameter> listPara = new List<CustomParameter>();
            tukhoa = txtTimKiem.Text;
            listPara.Add(new CustomParameter()
            {
                key = "@tukhoa",
                value = tukhoa
            });
            dgvGiaoVien.DataSource = new Database().SelectData("SelectAllGiaoVien", listPara);

            //custom ten cot
            dgvGiaoVien.Columns["MaGiaoVien"].HeaderText = "Mã GV";
            dgvGiaoVien.Columns["HoTen"].HeaderText = "Họ tên đầy đủ";
            dgvGiaoVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvGiaoVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvGiaoVien.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dgvGiaoVien.Columns["Email"].HeaderText = "Email Cá Nhân";
            dgvGiaoVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";

        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {

        }
    }
}
