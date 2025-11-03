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
    public partial class appliance_renting : Form
    {
        public appliance_renting()
        {
            InitializeComponent();
            AutoID();
            ClearTextBox();
            GetApplianceTypeData();
            dtappl = objappl.GetData();
            dgv_Appl.DataSource = dtappl;
            dgv_Appl.Refresh();
        }
        rentalTableAdapters.Appliance_Rent2TableAdapter objappl = new rentalTableAdapters.Appliance_Rent2TableAdapter();
        rentalTableAdapters.ApplianceTableAdapter objappltype = new rentalTableAdapters.ApplianceTableAdapter();
        DataTable dtappl = new DataTable();
        DataTable dtappltype = new DataTable();

        public void ClearTextBox()
        {
            
            txt_dim.Clear();
           Txt_energy.Clear();
           txt_fees.Clear();
           Txt_mdl.Clear();
           txt_quantity.Clear();
            
               
        }
        public void GetApplianceTypeData()
        {
            DataTable dtappl = new DataTable();
            dtappl = objappl.GetData();
            int RowCount = dtappl.Rows.Count;
            if (dtappl.Rows.Count > 0)
            {
                DataRow R = dtappl.NewRow();

            }
        }
        public void GetAppTypeData()
        {
            DataTable dtappltype = new DataTable();
            dtappl = objappltype.GetData();
            int RowCount = dtappltype.Rows.Count;
            if (dtappl.Rows.Count > 0)
            {
                DataRow R = dtappl.NewRow();
                cbo_Apl.DataSource = dtappl;
                cbo_Apl.DisplayMember = "ApplianceTypeID";
                cbo_Apl.ValueMember = "ApplianceTypeName";
            }
        }

        public void AutoID()
        {
            
            dtappl = objappl.GetData();
            if
                (dtappl.Rows.Count == 0)
            {
                appl_ID.Text = "A_001";
            }
            else
            {
                int size = dtappl.Rows.Count - 1;
                string oldid = dtappl.Rows[size][0].ToString();
                int newid = Convert.ToInt32(oldid.Substring(2, 3));
                if (newid >= 1 && newid < 9)
                {
                    appl_ID.Text = "A_00" + (newid + 1);
                }
                else if (newid >= 1 && newid < 99)
                {
                    appl_ID.Text = "A_0" + (newid + 1);
                }
                else if (newid >= 1 && newid < 9)
                {
                    appl_ID.Text = "A_" + (newid + 1);
                }
            }
        }



        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            AutoID();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
           
            int Quantity = Convert.ToInt32(txt_quantity.Text);
            int Monthly = Convert.ToInt32(txt_fees.Text);
            string loction = "C:\\DDOOCP\\DDOOCP_LYNNTHANT_GA\\DDOOCP\\Program\\Rental\\Rental\\bin\\Debug";
            string path = Path.Combine(loction, appl_ID.Text  + ".jpg");
            Image A = appl_photo.Image;
            A.Save(path);
            objappl.Insert(appl_ID.Text, cbo_Apl.Text, Brand_1.Text, Txt_mdl.Text, txt_dim.Text, tx_color2.Text, Txt_energy.Text, Monthly, Quantity, path,txt_Name.Text);
            MessageBox.Show("Saved Successfully");
            dtappl = objappl.GetData();
            dgv_Appl.DataSource = dtappl;
            dgv_Appl.Refresh();
           
            AutoID();
            borrow_form bf = new borrow_form();
            bf.Show();
            this.Hide();
            
        }

        private void appl_photo_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox Pb = sender as PictureBox;
            if (Pb!= null)
            {
                open.Filter ="(*.jpg;*.jpeg;*.png) | *.jpg;* .jpeg;*.png";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Pb.Image =Image.FromFile(open.FileName);
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_dim_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        private void cbo_Apl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_Appl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_Appl_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row =this.dgv_Appl.Rows[e.RowIndex];
            appl_ID.Text = row.Cells[0].Value.ToString();
               cbo_Apl.Text = row.Cells[1].Value.ToString();
                Brand_1.Text =row.Cells[2].Value.ToString();
             Txt_mdl.Text =row.Cells[3].Value.ToString();
            txt_dim.Text = row.Cells[4].Value.ToString();
                tx_color2.Text=row.Cells[5].Value.ToString();
            Txt_energy.Text = row.Cells[6].Value.ToString();
                txt_fees.Text =row.Cells[7].Value.ToString();
            txt_quantity.Text =row.Cells[8].Value.ToString();
            txt_Name.Text =row.Cells[9].Value.ToString();
            btn_save.Enabled = false;
            button1.Enabled = false;
        }

        private void appliance_renting_Load(object sender, EventArgs e)
        {
            dtappl = objappl.GetData();
            dgv_Appl.DataSource = dtappl;
            dgv_Appl.Refresh();
        }

    }
}
