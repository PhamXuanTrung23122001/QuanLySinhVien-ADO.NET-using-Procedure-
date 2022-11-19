using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic.ApplicationServices;

namespace QLSV_3layers
{
    public class Database
    {
        private string connectionString = "Data Source=LAPTOP-RVEHG95S\\DBI202;Initial Catalog = StudentManagerment; Persist Security Info=True;User ID = sa; Password=123";
        private SqlConnection conn;
        private DataTable dt;
        private SqlCommand cmd;
        public Database()
        {
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                //MessageBox.Show("Connected successfully ");
            
            }
            catch (Exception ex)
            {

                MessageBox.Show("Connected fail " + ex.Message);
            }
        }
        public DataTable SelectData(String sql, List<CustomParameter> listPara)
        {
            try
            {
                if(conn.State==System.Data.ConnectionState.Open)
                {
                    
                    cmd = new SqlCommand(sql, conn);// noi dung sql dc truyen vao
                    cmd.CommandType = CommandType.StoredProcedure;//set command type cho cmd
                    foreach (var p in listPara)//gan cac tham so cho cmd
                    {
                        cmd.Parameters.AddWithValue(p.key, p.value);
                    }
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
                else
                {
                    return null;
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Loading error " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataRow Select(String sql)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {

                    cmd = new SqlCommand(sql, conn);
                    dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt.Rows[0];
                }
                else
                {
                    return null;
                }
               

            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi load data " + ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public int Execute(String sql,List<CustomParameter> listPara)
        {
            //can chinh sua lai ham execute nhu sau
            //string sql,List<CustomParameter> listPara la them so truyen vao
            //customParameter da dc dinh nghia
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {

                    cmd = new SqlCommand(sql, conn);//thuc thi cau lenh sql
                    cmd.CommandType = CommandType.StoredProcedure;//gan cac tham so cho cmd
                    foreach (var p in listPara)
                    {
                        cmd.Parameters.AddWithValue(p.key, p.value);
                    }
                    var rs = cmd.ExecuteNonQuery();//lay ket qua thuc thu truy van
                    return (int)rs; //tra ve ket qua
                }
                else
                {
                    return -1;
                }
             

            }
            catch (Exception ex)
            {

                MessageBox.Show("lỗi thực thi câu lệnh " + ex.Message);
                return -100;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
