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
    public partial class UserLog : Form
    {
        public UserLog()
        {
            InitializeComponent();
            AutoID();
            dtuser = Objuser.GetData();
            DGV_usr.DataSource = dtuser;
            DGV_usr.Refresh();


        }
        rentalTableAdapters.user2TableAdapter Objuser = new rentalTableAdapters.user2TableAdapter();
        DataTable dtuser = new DataTable { };


        public void AutoID()
        {
            DataTable pt = new DataTable();
            pt = Objuser.GetData();
            if
                (pt.Rows.Count == 0)
            {
                id_1.Text = "Usr_00001";
            }
            else
            {
                int size = pt.Rows.Count - 1;
                string oldid = pt.Rows[size][0].ToString();
                int newid = Convert.ToInt32(oldid.Substring(4, 5));
                if (newid >= 1 && newid < 9)
                {
                    id_1.Text = "Usr_0000" + (newid + 1);
                }
                else if (newid >= 1 && newid < 99)
                {
                    id_1.Text = "Usr_000" + (newid + 1);
                }
                else if (newid >= 99 && newid < 999)
                {
                    id_1.Text = "Usr_00" + (newid + 1);
                }
                else if (newid >= 999 && newid < 9999)
                {
                    id_1.Text = "Usr_0" + (newid + 1);
                }
                else if (newid >= 9999 && newid < 99999)
                {
                    id_1.Text = "Usr_" + (newid + 1);
                }
            }
        }
        public void cleartextbox()
        {
            pas_1.Clear();
            id_1.Clear();
            NRC_1.Clear();
            address_1.Clear();
            ph_no1.Clear();
            name1.Clear();
            gender1.Clear();
            e_mail.Clear();

        }

        private void DGV_usr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DGV_usr_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = this.DGV_usr.Rows[e.RowIndex];
            id_1.Text = row.Cells[0].Value.ToString();
            name1.Text = row.Cells[1].Value.ToString();
            pas_1.Text = row.Cells[2].Value.ToString();
            NRC_1.Text = row.Cells[3].Value.ToString();
            e_mail.Text = row.Cells[4].Value.ToString();
            ph_no1.Text = row.Cells[5].Value.ToString();
            address_1.Text = row.Cells[6].Value.ToString();
        }

        private void btn_Upd_Click(object sender, EventArgs e)
        {
            if (id_1.Text == "")
            {
                MessageBox.Show("Please enter your user_id correctly");

                id_1.Focus();
            }
            if (pas_1.Text == "")
            {
                MessageBox.Show("Please enter your Password correctly");

                pas_1.Focus();
            }
            if (pas_1.Text.Length < 8 || pas_1.Text.Length > 20)
            {
                MessageBox.Show("Please Enter your password correctly. The password should be between 8 and 20 letters");
                if (e_mail.Text == "")
                {
                    MessageBox.Show("please Enter e-mail correctly");
                    e_mail.Focus();
                }
                if (NRC_1.Text == "")
                {
                    MessageBox.Show("Please enter your NRC correctly");

                    NRC_1.Focus();
                }
                if (address_1.Text == "")
                {
                    MessageBox.Show("Please enter your address correctly");

                    address_1.Focus();
                }
                if (ph_no1.Text == "")
                {
                    MessageBox.Show("Please enter your phone number correctly");

                    ph_no1.Focus();
                }
                else
                {
                    /*user_ usr = new user_();
                    usr.user_id = id_1.Text;
                    usr.Name = name1.Text;
                    usr.password = pas_1.Text;
                    usr._nRC = NRC_1.Text;
                    usr._email = e_mail.Text;
                    usr._phone_number = ph_no1.Text;
                    usr._address = address_1.Text;*/
                    Objuser.UpdateUser(id_1.Text, name1.Text, pas_1.Text, e_mail.Text, NRC_1.Text, address_1.Text, gender1.Text, ph_no1.Text);
                    MessageBox.Show("success!");
                    cleartextbox();
                    AutoID();
                    dtuser = Objuser.GetData();
                    DGV_usr.DataSource = dtuser;
                    DGV_usr.Refresh();


                }
            }

        }

        private void btn_clr_Click(object sender, EventArgs e)
        {

            cleartextbox();

            dtuser = Objuser.GetData();
            DGV_usr.DataSource = dtuser;
            DGV_usr.Refresh();
            btn_save.Enabled = false;
            btn_clr.Enabled = false;
            btn_Upd.Enabled = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (id_1.Text == "")
            {
                MessageBox.Show("Please enter your user_id correctly");

                id_1.Focus();
            }
            if (pas_1.Text == "")
            {
                MessageBox.Show("Please enter your Password correctly");

                pas_1.Focus();
            }
                if (pas_1.Text.Length < 8 || pas_1.Text.Length > 20)
                {
                    MessageBox.Show("Please Enter your password correctly. The password should be between 8 and 20 letters");
                    pas_1.Focus();
                }
                    if (e_mail.Text == "")
                    {
                        MessageBox.Show("please Enter e-mail correctly");
                        e_mail.Focus();
                    }
                    if (NRC_1.Text == "")
                    {
                        MessageBox.Show("Please enter your NRC correctly");

                        NRC_1.Focus();
                    }
                    if (address_1.Text == "")
                    {
                        MessageBox.Show("Please enter your address correctly");

                        address_1.Focus();
                    }
                    if (ph_no1.Text == "")
                    {
                        MessageBox.Show("Please enter your phone number correctly");

                        ph_no1.Focus();
                    }
                    else
                    {
                        /*user_ usr = new user_();
                        usr.user_id = id_1.Text;
                        usr.Name = name1.Text;
                        usr.password = pas_1.Text;
                        usr._nRC = NRC_1.Text;
                        usr._email = e_mail.Text;
                        usr._phone_number = ph_no1.Text;
                        usr._address = address_1.Text;*/
                        Objuser.Insert(id_1.Text, name1.Text, pas_1.Text, e_mail.Text, NRC_1.Text, address_1.Text, gender1.Text, ph_no1.Text);
                        MessageBox.Show("successfully Saved!");
                        cleartextbox();
                        dtuser = Objuser.GetData();
                        DGV_usr.DataSource = dtuser;
                        DGV_usr.Refresh();
                        AutoID();
                        borrow_form bf = new borrow_form();
                        bf.Show();
                        this.Hide();

                    }
                }

            }
        }

