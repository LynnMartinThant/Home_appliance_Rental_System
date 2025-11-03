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
    public partial class borrow_form : Form
    {
        rentalTableAdapters.Appliance_Rent2TableAdapter objappl = new rentalTableAdapters.Appliance_Rent2TableAdapter();
        rentalTableAdapters.user2TableAdapter objusr = new rentalTableAdapters.user2TableAdapter();
        rentalTableAdapters.RentFrm3TableAdapter objrent = new rentalTableAdapters.RentFrm3TableAdapter();
        rentalTableAdapters.detailTBTableAdapter objDL = new rentalTableAdapters.detailTBTableAdapter();
        DataTable Dtappl = new DataTable();
        DataTable dtusr = new DataTable();
        DataTable pt = new DataTable();
        DataRow DR;
        public borrow_form()
        {
            InitializeComponent();
            pt.Columns.Add("Rental_ID", typeof(string));
            pt.Columns.Add("ApplianceID", typeof(string));
            pt.Columns.Add("ApplianceType", typeof(string));

            pt.Columns.Add("BrandName",typeof(string));
            pt.Columns.Add("Price", typeof(string));

            pt.Columns.Add("Rental_period",typeof(string));
            pt.Columns.Add("Sub_Total", typeof(string));
            
            
                   
        }
        private string getAutoID()
        {
            DataTable dttemp = objrent.GetDataByLastID();
            int NumofRow = dttemp.Rows.Count;
            if (NumofRow < 1)
            {
                return "BR_00001";
            }
            else
            {
                String OldID = dttemp.Rows[0][0].ToString();
                int OldNum = Convert.ToInt32(OldID.Substring(3));
                OldNum++;
                string NewID = "BR_" + OldNum.ToString("00000");
                return NewID;
            }
        }
        private void SetApplianceName()
        {

            Dtappl = objappl.GetData();

            cbo_name.Items.Add("Choose Appliance");

            for (int i = 0; i <Dtappl.Rows.Count; i++)
            {

               cbo_name.Items.Add(Dtappl.Rows[i][2].ToString());

            }
            cbo_name.SelectedIndex = 0;

        }

        private void SetUserID()
        {

           dtusr = objusr.GetData();

            Cbo_ID.Items.Add("Choose Member ID");

            for (int j = 0; j < dtusr.Rows.Count; j++)
            {

                Cbo_ID.Items.Add(dtusr.Rows[j][0].ToString());

            }

            Cbo_ID.SelectedIndex = 0;

        } 

        private void gpbox_detail_Enter(object sender, EventArgs e)
        {

        }

        private void borrow_form_Load(object sender, EventArgs e)
        {
            txtRentalID.Text = getAutoID();
            SetApplianceName();
            SetUserID();
            DateCalculation();
                
        }

        private void cbo_name_SelectedIndexChanged(object sender, EventArgs e)
        {
    DataTable dtappl = new DataTable();

            dtappl = objappl.GetDataByBrand(cbo_name.SelectedItem.ToString());
            for (int i = 0; i < dtappl.Rows.Count; i++)
            {
                
                txt_ApplID.Text = dtappl.Rows[0][0].ToString();
                txt_brand.Text = dtappl.Rows[0][1].ToString();
                txt_avl.Text = dtappl.Rows[0][3].ToString();
                Appl_Pic.Image= Image.FromFile(dtappl.Rows[0][9].ToString());
                txtprice.Text = dtappl.Rows[i][7].ToString();
                
            }
        }

        private void Appl_Pic_Click(object sender, EventArgs e)
        {

        }

        private void Cbo_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = objusr.
                GetDataByUsrID(Cbo_ID.SelectedItem.ToString());



            for (int j = 0; j < dt.Rows.Count; j++)
            {

                txtname.Text = dt.Rows[0][1].ToString();
                Txt_NRC.Text = dt.Rows[0][2].ToString();
            }
        }
              private void DateCalculation() 

        { 
            DateTime currentDate = ST_date.Value; 
            DateTime newDate = currentDate.AddMonths(1);
             ED_date.Value = newDate; 

        }

        private void tn_add_Click(object sender, EventArgs e)
        {
           try 
            { 
                if (cbo_name.SelectedIndex == 0) 
                { 
                    MessageBox.Show("Please Select an Appliance Brand", "Warning", MessageBoxButtons.OK , MessageBoxIcon.Error ); 
                } 
                else  

                { 

                    string searchVal = txt_ApplID.Text; 

                    bool isRowExist = false; 

                    foreach (DataGridViewRow row in dgv_RentD.Rows) 

                    { 

                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(searchVal)) 

                        { 
                            isRowExist = true; 
                            break; 
                        } 

                    }

                    if (isRowExist) 

                    { 

                        MessageBox.Show("Appliance ID already exist in List!"); 

                    } 

                    else 

                    { 
                        DR= pt.NewRow();
                        DR["Rental_ID"] = txtRentalID.Text;
                        DR["ApplianceID"] = txt_ApplID.Text;
                        DR["ApplianceType"] = txt_brand.Text; 
                        
                        DR["BrandName"] = cbo_name.SelectedItem.ToString();
                        DR["Price"] = txtprice.Text;
                        DR["Rental_period"] = Cbo_rentP.SelectedItem.ToString(); 
                        DR["Sub_Total"] = Convert.ToInt32(txtprice.Text) * Convert.ToInt32(Cbo_rentP.SelectedItem.ToString());
                        
                        pt.Rows.Add(DR); 
                        dgv_RentD.DataSource = pt;  
                        int rowCount = (dgv_RentD.Rows.Count);
                        
                        txtTotal.Text = rowCount.ToString();
                         

                       

                        decimal grandTotal = 0; 

                        foreach (DataGridViewRow row in dgv_RentD.Rows) 

                        { 
                            decimal amount; 
                            if (decimal.TryParse(row.Cells["SubTotal"].Value.ToString(),out amount ))
                            { 
                                 grandTotal +=amount; 
                            } 

                        } 

                        Txt_Gtotal. Text = grandTotal.ToString(); 
                 

                    } 

                }   
            } 

            catch (Exception) 

            { 

                MessageBox.Show("Something Wrong!!!"); 

            }
           
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            if (dgv_RentD.SelectedRows.Count>0)
            {
                int rowIndex =dgv_RentD.SelectedRows[0].Index;
                  dgv_RentD.Rows.RemoveAt(rowIndex)  ;
                int rowCount = (dgv_RentD.Rows.Count);
                txtTotal.Text = rowCount.ToString();
            }
            else
            {
            MessageBox.Show ("Please Select a row to Remove!!");
            }

            }

        private void lbl_Save_Click(object sender, EventArgs e)
        {
        
        }
            
        

      private void ClearAll()
    {


            cbo_name.SelectedIndex = 0; 

            Cbo_ID.SelectedIndex = 0; 

            txt_ApplID.Text = ""; 

            txt_avl.Text = ""; 

            Appl_Pic.Image = null; 

             

            txtname.Clear(); 

            Txt_NRC.Clear(); 

            txtTotal.Text = "0"; 
 

            dgv_RentD.DataSource = null; 

            dgv_RentD.Rows.Clear(); 

        }
      private void saveToolStripMenuItem_Click(object sender, EventArgs e)
      {

      }

 

        private void cboBorrowPeriod_SelectedIndexChanged(object sender, EventArgs e) 

        { 

           
        }

        private void lbl_clr_Click(object sender, EventArgs e)
        {
        
        }

        private void txt_applID_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tool_save_Click(object sender, EventArgs e)
        {

        }

        private void dgv_RentD_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void Cbo_rentP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime currentDate = ST_date.Value;

            int BorrowPeriod = Convert.ToInt32(Cbo_rentP.SelectedItem.ToString());

            DateTime newDate = currentDate.AddMonths(BorrowPeriod);

            ED_date.Value = newDate; 

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
             

                

            }

        private void txt_avl_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblsave_Click(object sender, EventArgs e)
        {
            string Rental_ID = txt_ApplID.Text;

            DateTime StartingDate = DateTime.Parse(ST_date.Text);

            DateTime EndDate = DateTime.Parse(ED_date.Text);

            string User_ID = Cbo_ID.SelectedItem.ToString();
            int Price = Convert.ToInt32(txtprice.Text);
            int Total = Convert.ToInt32(txtTotal.Text);

            string RentStatus = "Borrowed";



            string BookStatus = "Unavailable";



            objrent.Insert(Rental_ID,StartingDate, EndDate, User_ID, Total, Price,RentStatus);







            for (int i = 0; i < dgv_RentD. Rows.Count; i++)
            {

                string Appliance = dgv_RentD.Rows[i].Cells[0].Value.ToString();

                

            // objDL.Insert(Rental_ID,Appliance_ID,);//



               // objDL.UpdateBookStatus(BookStatus, Appliance);//

            }

            MessageBox.Show("Borrow successful.");

            txt_ApplID .Text = getAutoID();

            ClearAll();



        }




           

        }

        
        } 


