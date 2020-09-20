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
    public partial class frmCategory : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        frmCategoryList frmClist;
        public frmCategory(frmCategoryList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmClist = flist;
        }
        public void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCategory.Clear();
            txtCategory.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Inserting data into database
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO category(category_name)VALUES(@brand)", cn);
                    cm.Parameters.AddWithValue("@brand", txtCategory.Text);
                    cm.ExecuteNonQuery();
                    Clear();
                    frmClist.LoadRecords();
                    MessageBox.Show("New Category added sucessfully");
                    cn.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }
    }
}
