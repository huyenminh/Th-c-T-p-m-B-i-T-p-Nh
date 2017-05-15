using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ql_thu_vien.ValueObject;
using ql_thu_vien.BusinessLogicLayer;
using ql_thu_vien.DataAccessLayer;
using System.Data.SqlClient;

namespace ql_thu_vien
{
    public partial class Chitietphieumuon : Form
    {
        public string ma;
        string sql = @"Server=DESKTOP-MJKVIGJ\SQLEXPRESS;Database=TTCSDL;Trusted_Connection=true ";
        SqlConnection conn;

        public Chitietphieumuon()
        {
            InitializeComponent();
        }
         public Chitietphieumuon(string _ma):this()
        { 
            ma=_ma;
            txtmaphieu.Text= ma;
        }
        private void Chitietphieumuon_Load(object sender, EventArgs e)
        {
            dgvChitietmuon.DataSource = Bus.Getlistchitietphieumuon();
            cmbmacs.DataSource = Bus.GetListCuonSach();
            cmbmacs.ValueMember = "macs";
            cmbtruoc.DataSource = Bus.Getlistchitietphieumuon();
            cmbtruoc.ValueMember = "tinhtrangtruocchomuon";
            cmbsau.DataSource = Bus.Getlistchitietphieumuon();
            cmbsau.ValueMember = "tinhtrangsauchomuon";
            conn = new SqlConnection(sql);
            conn.Open();
            SqlCommand cmd = new SqlCommand("xemchitiethoadon", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter p = new SqlParameter("@ma", ma);
            cmd.Parameters.Add(p);
            SqlDataAdapter dg = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dg.Fill(dt);
            dgvChitietmuon.DataSource = dt;
        }

        private void dgvChitietmuon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbmacs.Text = dgvChitietmuon.CurrentRow.Cells["macs"].Value.ToString();
            cmbtruoc.Text = dgvChitietmuon.CurrentRow.Cells["tinhtrangtruocchomuon"].Value.ToString();
            cmbsau.Text = dgvChitietmuon.CurrentRow.Cells["tinhtrangsauchomuon"].Value.ToString();
        }

        private void but_them_Click(object sender, EventArgs e)
        {
            cmbmacs.Text = string.Empty;
            cmbtruoc.Text = string.Empty;
            cmbsau.Text = string.Empty;
        }

        private void but_sua_Click(object sender, EventArgs e)
        {
            if ( cmbmacs.Text == "")
            {
                MessageBox.Show("chưa nhập thông tin");
                return;
            }
            chitietphieumuon ctpm = new chitietphieumuon();
            ctpm.macs = cmbmacs.Text;
            ctpm.tinhtrangtruocchomuon = cmbtruoc.Text;
            ctpm.tinhtrangsauchomuon = cmbsau.Text;
            if (MessageBox.Show(string.Format("sửa thông tin chi tiết phiếu"), "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Bus.UpdateProfile(ctpm) > 0)
                {
                    MessageBox.Show("Đã sửa");
                    Chitietphieumuon_Load(sender, e);
                }
                else
                    MessageBox.Show("Lỗi");
            }

        }

        private void but_xoa_Click(object sender, EventArgs e)
        {
            if ( cmbmacs.Text == "")
            {
                MessageBox.Show("chưa có thông tin");
                return;
            }
            chitietphieumuon ctpm = new chitietphieumuon();
            ctpm.macs = cmbmacs.Text;
            ctpm.tinhtrangtruocchomuon = cmbtruoc.Text;
            ctpm.tinhtrangsauchomuon = cmbsau.Text;
            if (MessageBox.Show(string.Format("xóa chi tiết phiếu"), "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Bus.DeleteProfile(ctpm) > 0)
                {
                    MessageBox.Show("Đã xóa");
                    Chitietphieumuon_Load(sender, e);
                }
                else
                    MessageBox.Show("Lỗi");
            }

        }

        private void but_luu_Click(object sender, EventArgs e)
        {
            if (cmbmacs.Text == "" )
            {
                MessageBox.Show("chưa nhập thông tin");
                return;
            }
            chitietphieumuon ctpm = new chitietphieumuon();
            ctpm.macs = cmbmacs.Text;
            ctpm.tinhtrangtruocchomuon = cmbtruoc.Text;
            ctpm.tinhtrangsauchomuon = cmbsau.Text;
            if (MessageBox.Show(string.Format("Thêm chi tiết phiếu"), "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (Bus.InsertProfile(ctpm) > 0)
                {
                    MessageBox.Show("Đã thêm");
                    Chitietphieumuon_Load(sender, e);
                }
                else
                    MessageBox.Show("Lỗi");
            }
        }

        private void but_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Dispose();
            }
        }

        public  void Hienthi(string ma)
        {
            dgvChitietmuon.DataSource = Bus.Timkiemctpm(ma);
        }
        private void but_timkiem_Click(object sender, EventArgs e)
        {
            Hienthi(" where maphieu like '%" +  txt_timkiem.Text +  "'" );
        }

        private void txt_timkiem_Click(object sender, EventArgs e)
        {
            txt_timkiem.Text = string.Empty;
        }
    }
}
