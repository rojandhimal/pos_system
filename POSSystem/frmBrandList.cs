using POSSystem.BLL;
using POSSystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSSystem
{
    public partial class frmBrandList : Form
    {
        // posBLL p = new posBLL();
        // brandDAL bdal = new brandDAL();
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public frmBrandList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadRecords();
        }

        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            formBrand fb = new formBrand(this);
            fb.btnUpdate.Visible = false;
            fb.ShowDialog();
        }

        public void LoadRecords()
        {
            dataGridView2.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from brand order by brand", cn);
            dr = cm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                i += 1;
                dataGridView2.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string collName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (collName == "Edit")
            {
                formBrand frm = new formBrand(this);
                frm.btnSave.Visible = false;
                frm.btnUpdate.BackColor = Color.FromArgb(20, 158, 132);
                frm.btnUpdate.ForeColor = Color.White;
                frm.LabelText = dataGridView2[e.ColumnIndex, 1].Value.ToString();
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                frm.LabelText = row.Cells[2].Value.ToString();
                frm.lbliD.Text = row.Cells[1].Value.ToString();
                frm.ShowDialog();
                
            }

            if(collName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this brand?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM brand where id =@id", cn);
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                    string id = row.Cells[1].Value.ToString();
                    cm.Parameters.AddWithValue("@id", id);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecords();
                    MessageBox.Show("Brand deleted sucessfully");
                   
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
