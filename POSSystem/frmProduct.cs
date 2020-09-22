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
    public partial class frmProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmProductList flist;
        public frmProduct(frmProductList frmPL)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            flist = frmPL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCategory()
        {
            cmboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cmboCategory.Items.Add(dr[1].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void LoadBrand()
        {
            cmboBrand.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM brand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cmboBrand.Items.Add(dr[1].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void Clear()
        {
            txtPrice.Clear();
            txtProductName.Clear();
            textDesc.Clear();
            cmboBrand.Text = "";
            cmboCategory.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Inserting data into database
                    
                    int cid_index = cmboCategory.SelectedIndex;
                    int cid= cid_index + 1;
                    int Bid_index = cmboBrand.SelectedIndex;
                    int Bid = Bid_index + 1;
                    
                    cn.Open();

                    cm = new SqlCommand("INSERT INTO product(pname,pdesc,bid,cid,price,qty)VALUES(@pname,@pdesc,@bid,@cid,@price,@qty)", cn);
                    cm.Parameters.AddWithValue("@pname",txtProductName.Text);
                    cm.Parameters.AddWithValue("@pdesc", textDesc.Text);
                    cm.Parameters.AddWithValue("@bid", Bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    cm.Parameters.AddWithValue("@qty", txtQty.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    flist.LoadRecords();
                    MessageBox.Show("New Product added sucessfully");
                    Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                //accept backspace
            }
            else if (e.KeyChar == 46)
            {
                //accept dot(.) for float value
            }
            else if((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (e.KeyChar == 8) { 
            //accept backspace
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }
    }
}
