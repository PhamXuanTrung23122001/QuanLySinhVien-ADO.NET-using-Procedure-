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
    public partial class FrmDSSV : Form
    {
        public FrmDSSV()
        {
            InitializeComponent();
        }

        private void DSSV_Load(object sender, EventArgs e)
        {
            LoadSinhVien();//goi toi ham load sinh vien
        }

        private void dgvSinhVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ý tưởng khi double click vào sinh viên trên dgv
            //sẽ hiện ra form cập nhật thông tin
            //để cập nhật đc ta cần lấy đã mã sinh viên
            if(e.RowIndex>=0)
            {
               var msv = dgvSinhVien.Rows[e.RowIndex].Cells["MaSinhVien"].Value.ToString();

                //can truyen ong nay sang cho frmSinhVien
                new FrmSinhVien(msv).ShowDialog();

                //sau khi form FrmSinhVien dong lai
                //can load lai danh sach sinh vien
                LoadSinhVien();
            }
        }
        private String tukhoa = "";
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            new FrmSinhVien(null).ShowDialog();
            //neu them moi sinh vien -> ma sinh vien = null
            LoadSinhVien();//them thanh cong thi load lai data
        }

        private void LoadSinhVien()
        {
            //load toan bo danh sach sinh vien
            //khai bao list customparameter
            List<CustomParameter> listPara = new List<CustomParameter>();
            listPara.Add(new CustomParameter()
            {
                //ban dau tu khoa  = null
                key="@tukhoa",
                value = tukhoa

            });
            
            dgvSinhVien.DataSource = new Database().SelectData("SelectAllSinhVien", listPara);
            //dat ten cot
            dgvSinhVien.Columns["MaSinhVien"].HeaderText = "Mã SV";
            dgvSinhVien.Columns["HoTen"].HeaderText = "Họ tên đầy đủ";
            dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvSinhVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvSinhVien.Columns["QueQuan"].HeaderText = "Quê Quán";
            dgvSinhVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvSinhVien.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dgvSinhVien.Columns["Email"].HeaderText = "Email Cá Nhân";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<CustomParameter> listPara = new List<CustomParameter>();
            //set tu khoa =noi dung cua text box timkiem
            tukhoa =txtTimKiem.Text;
            listPara.Add(new CustomParameter()
            {
                key = "@tukhoa",
                value = tukhoa

            });

            dgvSinhVien.DataSource = new Database().SelectData("SelectAllSinhVien", listPara);
            //dat ten cot
            dgvSinhVien.Columns["MaSinhVien"].HeaderText = "Mã SV";
            dgvSinhVien.Columns["HoTen"].HeaderText = "Họ tên đầy đủ";
            dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvSinhVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvSinhVien.Columns["QueQuan"].HeaderText = "Quê Quán";
            dgvSinhVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvSinhVien.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dgvSinhVien.Columns["Email"].HeaderText = "Email Cá Nhân";
        }
    }
}
