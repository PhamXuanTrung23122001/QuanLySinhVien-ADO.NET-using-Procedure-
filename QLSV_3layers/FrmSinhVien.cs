using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3layers
{
    public partial class FrmSinhVien : Form
    {
        public FrmSinhVien(String msv)
        {
            this.msv = msv; //truyen lai ma sv khi form chay
            InitializeComponent();
        }
        private String msv;

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(msv))
            //neu msv k co -> them ms sinh vien
            {
                this.Text = "Thêm mới sinh viên";
            }
            else
            {
                this.Text = "Cập nhật thông tin sinh viên";
                //lay thong tin chi tiet cua 1 sinh vien dua vao msv
                //ma sinh vien da duoc truyen tu format dssv
                var r = new Database().Select("TimSv '"+msv+"'");
                //set cac gia tri vao component cua form
                txtHo.Text = r["Ho"].ToString();
                txtTenDem.Text = r["TenDem"].ToString();
                txtTen.Text = r["Ten"].ToString();
                maskedNgaySinh.Text = r["NgaySinh"].ToString();

                if (r["GioiTinh"].ToString().Equals("Nam"))
                {
                    radioMale.Checked = true;
                }
                else
                {
                    radioFemale.Checked = true;
                    
                }
                
                txtQueQuan.Text = r["QueQuan"].ToString();
                txtDiaChi.Text = r["DiaChi"].ToString();
                txtSoDienThoai.Text = r["DienThoai"].ToString();
                txtEmail.Text = r["Email"].ToString();
            }
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //button btnluu se dc su li 1 trong 2 tinh huong
            //th1: neu ma sinh vien k co gia tri -> them moi sv
            //th2:neu ma sinh vien co gia tri -> cap nhat thong tin sv
            /*
             cho du la them moi hay cap nhat
            thi deu can cac gia tri nhu ho, ten dem, ten , ngsinh
            , gioi tinh, que quan, dia chi, dien thoai , email
            cac gia tri nay dung cho ca 2 truong hop
            rieng cap nhat thi can quan tam them masv
             */
            String sql = "";
            String ho = txtHo.Text;
            String tendem = txtTenDem.Text;
            String ten = txtTen.Text;
            DateTime ngaysinh;
            try
            {
                ngaysinh = DateTime.ParseExact(maskedNgaySinh.Text, "dd/MM/yyyy",CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {

                MessageBox.Show("ngay sinh khong hop le");
                maskedNgaySinh.Select();//tro chuot ve ngay sinh
                return;//k thuc hien cac cau lenh ben duoi
            }
            //vi ngay sinh de dang masketbox 
            //nen chung ta set theo dang dd/mm/yyyy
            //nhung trong sql la yyyy/mm/dd
            //chung ta can chuyen tu dd/mm/yyyy -> yyyy/mm/dd
            string gioitinh = radioMale.Checked ? "1" : "0";//toan tu 3 ngoi
            //neu radio nam  dc check thi co gia tri =1
            //ngc lai nu la 0, phu hop de luu o csdl

            string quequan = txtQueQuan.Text;
            string diachi = txtDiaChi.Text;
            string dienthoai = txtSoDienThoai.Text;
            string email =txtEmail.Text;
            //khai bao danh sach tham so = class CustomParameter
            List<CustomParameter>listPara = new List<CustomParameter>();
            if(string.IsNullOrEmpty(msv))//them ms sv
            {
                sql = "ThemMoiSv";// goi toi procedure them moi sih vien
            }
            else//cap nhat sv
            {
                sql = "CapNhatSv";// goi toi procedure cap nhat sinh vien
                listPara.Add(new CustomParameter()
                {
                    key = "@MaSinhVien",
                    value = msv
                });
            }
            listPara.Add(new CustomParameter()
            {
                key="@Ho",
                value =ho
            });
            listPara.Add(new CustomParameter()
            {
                key = "@TenDem",
                value = tendem
            });
            listPara.Add(new CustomParameter()
            {
                key = "@Ten",
                value = ten
            });
            listPara.Add(new CustomParameter()
            {
                key = "@NgaySinh",
                value = ngaysinh.ToString("yyyy-MM-dd")
            });
            listPara.Add(new CustomParameter()
            {
                key = "@GioiTinh",
                value = gioitinh
            });
            listPara.Add(new CustomParameter()
            {
                key = "@QueQuan",
                value = quequan
            });
            listPara.Add(new CustomParameter()
            {
                key = "@DiaChi",
                value = diachi
            });
            listPara.Add(new CustomParameter()
            {
                key = "@DienThoai",
                value = dienthoai
            });
            listPara.Add(new CustomParameter()
            {
                key = "@Email",
                value = email
            });


            var rs = new Database().Execute(sql, listPara);
            //truyen 2 tham so la cau lenh sql va list cac tham so
            if(rs==1)
            {
                if(string.IsNullOrEmpty(msv))//neu them moi
                {
                    MessageBox.Show("Them moi sinh vien thanh cong");
                }
                else//neu cap nhat
                {
                    MessageBox.Show("Cap nhat sinh vien thanh cong");
                }
                this.Dispose();//dong form sau khi thao tac thanh cong
            }
            else//neu k thanh cong
            {
                MessageBox.Show("Thuc thi cau lenh that bai");
            }
        }
    }
}
