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
    public partial class HomeLgn : Form
    {
        public HomeLgn()
        {
            InitializeComponent();
        }

        private void btn_User_Click(object sender, EventArgs e)
        {
            login lg = new login();
            lg.Show();
            this.Hide();
        }

        private void btn_Admin_Click(object sender, EventArgs e)
        {
            admLgn ad = new admLgn();
            ad.Show();
            this.Hide();

        }
    }
}
