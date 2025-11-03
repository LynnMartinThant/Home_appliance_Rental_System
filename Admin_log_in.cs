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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        rentalTableAdapters.Adm_reg1TableAdapter AD = new rentalTableAdapters.Adm_reg1TableAdapter();
        private int LoginCounts = 0;
        public static string Admin_name,Password;


        private void button2_Click(object sender, EventArgs e)
        {
            if (LoginCounts == 3)
            {
                MessageBox.Show("Login had been failed Three Times! In that case U have reach your limit");
            }
            else if (txt_name. Text =="")
            {
                MessageBox.Show("Enter the name");
            }
            else  if (txt_ID. Text =="")
            {
                MessageBox.Show ("enter the Password") ;
            }
            else 
            {
                DataTable TB = new DataTable();
           TB = AD.AdminLog(txt_ID.Text,txt_name.Text);
           if (TB.Rows.Count == 1)
           {
               MessageBox.Show("Successfully Logined");
               dgv_admlog.DataSource = TB;
              // Admin_name = dgv_admlog[0,1].Value.ToString();
               //Password = dgv_admlog[0, 5].Value.ToString();
               appliance_renting HP = new appliance_renting();
               HP.Show();
               this.Hide();

               
           }
           else
           {
               LoginCounts += 1;
               MessageBox.Show("login invaild!" + LoginCounts);
           }
            }
            }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserLog usr = new UserLog();
            usr.Show();
           this.Hide();

        }

        private void txt_ID_TextChanged(object sender, EventArgs e)
        {

        }
        
    
    }
    }
