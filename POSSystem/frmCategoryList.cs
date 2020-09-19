using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSSystem
{
    public partial class frmCategoryList : Form
    {
        public frmCategoryList()
        {
            InitializeComponent();
        }

        
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
                frm.btnSave.Enabled = true;
                frm.btnUpdate.Enabled = false;
                frm.ShowDialog();
        }
    }
}
