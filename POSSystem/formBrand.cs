using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using POSSystem.BLL;
using POSSystem.DataAccessLayer;

namespace POSSystem
{
    public partial class formBrand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();

        //posBLL p = new posBLL();
        //brandDAL bdal = new brandDAL();
        frmBrandList frmlist;

        public formBrand(frmBrandList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = flist;
           
        }

        private void formBrand_Load(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBrand.Clear();
            txtBrand.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Getting data from UI
            //p.brand = txtBrand.Text;
            try
            {
                if (MessageBox.Show("Are you sure you want to save this brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Inserting data into database
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO brand(brand)VALUES(@brand)",cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    Clear();
                    frmlist.LoadRecords();
                    MessageBox.Show("New brand added sucessfully");
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         
        }

        public string LabelText
        {
            get { return txtBrand.Text; }
            set { txtBrand.Text = value; }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { 
                    cn.Open();
                    cm = new SqlCommand("UPDATE brand set brand = @brand WHERE id = @id", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.Parameters.AddWithValue("@id", lbliD.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    frmlist.LoadRecords();
                    MessageBox.Show("Brand has been successfully updated");

                }
             }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
