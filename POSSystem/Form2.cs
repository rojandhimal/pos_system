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

namespace POSSystem
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public Form2()
        {
            InitializeComponent();
            con.ConnectionString = @"server=localhost;user id=root;database=pos_system;persistsecurityinfo=True";

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {

            con.Open();
            com.Connection = con;
            com.CommandText = "select * from brand";
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
               if(dr["brand_name"].ToString() !="rojan")
                {
                    MessageBox.Show("yes");
                }
            }
            MessageBox.Show("completed");
        }
    }
}
