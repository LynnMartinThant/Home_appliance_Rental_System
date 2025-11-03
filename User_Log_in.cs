using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental
{
    public partial class admLgn : Form
    {
        public admLgn()
        {
            InitializeComponent();
            
            //dgv_admin.DataSource = DT;//
            dgv_admin.Refresh();
        }
        rentalTableAdapters.user2TableAdapter AD = new rentalTableAdapters.user2TableAdapter();
        private int LoginCount = 0;
        public string Name, Password;

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (LoginCount == 3)
            {
                MessageBox.Show("Login Limit has reached");
            }
            else if (Txt_name.Text == "")
            {
                MessageBox.Show("Please Enter Your Name Sir/Madam");
            }
            else if (txt_Pass.Text == "")
            {
                MessageBox.Show("Please Enter Your Password correctly");
            }
            else
            {
                DataTable DT = new DataTable();
                DT = AD.LOG(Txt_name.Text, txt_Pass.Text);
                if (DT.Rows.Count == 1)
                {
                    MessageBox.Show("Login Successful");
                    dgv_admin.DataSource = DT;
                   // Name = dgv_admin[0, 1].Value.ToString();
                   // Password = dgv_admin[0, 2].Value.ToString();
                    
                    dgv_admin.DataSource = DT;
                    dgv_admin.Refresh();
                    borrow_form bf = new borrow_form();
                    bf.Show();
                    this.Hide();
                }
                else
                {
                    LoginCount += 1;
                    MessageBox.Show("Invaild Login! Try Again Later" + LoginCount);

                }
            }
        }

        private void dgv_admin_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void lblCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserLog A = new UserLog();
            A.Show();
            this.Hide();
        }
    }
}
