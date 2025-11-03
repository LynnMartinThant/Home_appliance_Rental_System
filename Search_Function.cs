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
    public partial class Search : Form
    {
        rentalTableAdapters.RentFrm3TableAdapter Psrent = new rentalTableAdapters.RentFrm3TableAdapter();
        DataTable dtrent = new DataTable();
        public Search()
        {
            InitializeComponent();
            SetRentID();
        }
        private void SetRentID()
        {
            dtrent = Psrent.GetData();
            Cbo_search.Items.Add("Choose Borrow ID");
            for (int y = 0; y< dtrent.Rows.Count; y++)
            {
                Cbo_search.Items.Add(dtrent.Rows[y][1].ToString());
            }
            Cbo_search.SelectedIndex = 0;
               }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            DateTime FromDate = DateTime.Parse(txt_from.Text);
            DateTime ToDate = DateTime.Parse(txt_To.Text);
            if (rdo_searchID.Checked)
            {
                dtrent = Psrent.SearchBy_ID(Cbo_search.SelectedItem.ToString());
                dgv_Search.DataSource = dtrent;
                dgv_Search.Refresh();
            }
            else if (rdo_searchDate.Checked)
            {
                dtrent = Psrent.SearchBy_Date(FromDate, ToDate);
                dgv_Search.DataSource = dtrent;
                dgv_Search.Refresh();

            }
        }

        private void Btn_showall_Click(object sender, EventArgs e)
        {
            dtrent = Psrent.GetDataByAllUser();
            dgv_Search.DataSource = dtrent;
            dgv_Search.Refresh();
        }

        private void Cbo_search_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
    }
}
