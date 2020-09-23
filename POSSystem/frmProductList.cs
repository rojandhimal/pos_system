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
    public partial class frmProductList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;

        public frmProductList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadRecords();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.LoadCategory();
            frm.LoadBrand();
            frm.ShowDialog();
        }
        public void LoadRecords()
        {
            dataGridView2.Rows.Clear();
            cn.Open();
            string sql = "SELECT p.pcode,p.pname,p.pdesc,b.brand,c.category_name,p.price, p.qty FROM product as p inner join brand as b on b.id = p.bid inner join category as c on c.id = p.cid";
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
            int i = 0;
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string collName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (collName == "Edit")
            {
                frmProduct frm = new frmProduct(this);
                frm.btnSave.Visible = false;
                frm.btnUpdate.BackColor = Color.FromArgb(20, 158, 132);
                frm.btnUpdate.ForeColor = Color.White;
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                frm.txtProductName.Text = row.Cells[2].Value.ToString();
                frm.lbliD.Text = row.Cells[1].Value.ToString();
                frm.textDesc.Text = row.Cells[3].Value.ToString();
                frm.cmboBrand.Text = row.Cells[4].Value.ToString();
                frm.cmboCategory.Text = row.Cells[5].Value.ToString();
                frm.txtPrice.Text = row.Cells[6].Value.ToString();
                frm.txtQty.Text = row.Cells[7].Value.ToString();
                frm.ShowDialog();
            }

            if (collName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM product where id =@id", cn);
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                    string id = row.Cells[1].Value.ToString();
                    cm.Parameters.AddWithValue("@id", id);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product deleted sucessfully");
                    LoadRecords();

                }
            }
        }
    }
}
