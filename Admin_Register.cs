using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Rental
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            AutoID();
            Atadm = objadm.GetData();
            data_adm.DataSource = Atadm;
            data_adm.Refresh();
        }
        rentalTableAdapters.Adm_reg1TableAdapter objadm = new rentalTableAdapters.Adm_reg1TableAdapter();
        DataTable Atadm = new DataTable { };

        public void AutoID()
        {
            DataTable pt = new DataTable();
            pt = objadm.GetData();
            if
                (pt.Rows.Count == 0)
            {
                adm_id.Text = "A_001";
            }
            else
            {
                int size = pt.Rows.Count - 1;
                string oldid = pt.Rows[size][0].ToString();
                int newid = Convert.ToInt32(oldid.Substring(2, 3));
                if (newid >= 1 && newid < 9)
                {
                    adm_id.Text = "A_00" + (newid + 1);
                }
                else if (newid >= 1 && newid < 99)
                {
                    adm_id.Text = "A_0" + (newid + 1);
                }
                else if (newid >= 1 && newid < 9)
                {
                    adm_id.Text = "A_" + (newid + 1);
                }
            }
        }
        public void ClearTextBox()
        {
            adm_pas.Clear();
            txtname.Clear();
            Adm_phno.Clear();
            Adm_email.Clear();
            adm_address.Clear();
            pct_adm.Image = null;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

            {
                OpenFileDialog open = new OpenFileDialog();
                PictureBox p = sender as PictureBox;
                if (p != null)
                {
                    open.Filter = "(*.jpg;*.jpeg;*.png) | *.jpg;* .jpeg;*.png";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        p.Image = Image.FromFile(open.FileName);

                        {

                            if (open.ShowDialog() == DialogResult.OK)
                            {
                                p.Image = Image.FromFile(open.FileName);
                            }
                        }
                    }
                }
            }
        }

        private void adm_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_upd_Click(object sender, EventArgs e)
        {
            if (adm_id.Text == "")
            {
                MessageBox.Show("Please enter your user_id correctly");

                adm_id.Focus();
            }
            if (txtname.Text == "")
            {
                MessageBox.Show("Please enter your Password correctly");

                txtname.Focus();
            }
            if (txtname.Text.Length < 8 || txtname.Text.Length > 20)
            {
                MessageBox.Show("Please Enter your password correctly. The password should be between 8 and 20 letters");
                if (Adm_phno.Text == "")
                {
                    MessageBox.Show("please Enter e-mail correctly");
                    Adm_phno.Focus();
                }

                if (Adm_email.Text == "")
                {
                    MessageBox.Show("Please enter your NRC correctly");

                    Adm_email.Focus();
                }
                if (adm_pas.Text == "")
                {
                    MessageBox.Show("Please enter your address correctly");

                    adm_pas.Focus();
                }
                if (adm_address.Text == "")
                {
                    MessageBox.Show("Please enter your phone number correctly");

                    adm_address.Focus();
                }

            }
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {



        }

        private void data_adm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = this.data_adm.Rows[e.RowIndex];
            adm_id.Text = row.Cells[0].Value.ToString();
            txtname.Text = row.Cells[1].Value.ToString();
            Adm_phno.Text = row.Cells[2].Value.ToString();
            Adm_email.Text = row.Cells[3].Value.ToString();
            adm_pas.Text = row.Cells[4].Value.ToString();
            adm_address.Text = row.Cells[6].Value.ToString();

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (adm_id.Text == "")
            {
                MessageBox.Show("Please enter your user_id correctly");

                adm_id.Focus();
            }
            if (txtname.Text == "")
            {
                MessageBox.Show("Please enter your Name correctly");

                txtname.Focus();
            }
            if (adm_pas.Text.Length < 8 || txtname.Text.Length > 20)
            {
                MessageBox.Show("Please Enter your password correctly. The password should be between 8 and 20 letters");
                if (Adm_phno.Text == "")
                {
                    MessageBox.Show("please Enter e-mail correctly");
                    Adm_phno.Focus();
                }

                if (Adm_email.Text == "")
                {
                    MessageBox.Show("Please enter your NRC correctly");

                    Adm_email.Focus();
                }
                if (adm_pas.Text == "")
                {
                    MessageBox.Show("Please enter your address correctly");

                    adm_pas.Focus();
                }
                if (adm_address.Text == "")
                {
                    MessageBox.Show("Please enter your phone number correctly");

                    adm_address.Focus();
                }
              
            }
            else
            {
                string loction = "C:\\DDOOCP\\DDOOCP_LYNNTHANT_GA\\DDOOCP\\Program\\Rental\\Rental\\bin\\Debug";

                string img = Path.Combine(loction, txtname.Text + ".jpg");
                Image a = pct_adm.Image;
                 a.Save(img);
                objadm.Insert(adm_id.Text, txtname.Text, Adm_phno.Text, Adm_email.Text, adm_pas.Text, adm_address.Text, img);
                
                MessageBox.Show("success!");
                ClearTextBox();
                Atadm = objadm.GetData();
                data_adm.DataSource = Atadm;
                data_adm.Refresh();
                AutoID();
                appliance_renting ad = new appliance_renting();
                ad.Show();
                this.Hide();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            Atadm = objadm.GetData();
            data_adm.DataSource = Atadm;
            data_adm.Refresh();
            btnsave.Enabled = false;
            btn_clear.Enabled = false;
            btn_save.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usr_info ui = new usr_info();
            ui.Show();
            this.Hide();
        }
    }

}
