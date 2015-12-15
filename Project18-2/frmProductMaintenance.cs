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

namespace Project18_2
{
    public partial class frmProductMaintenance : Form
    {
        public frmProductMaintenance()
        {
            InitializeComponent();
        }

        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                try
                {
                    this.Validate();
                    this.productsBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.techSupportDataSet);
                    productCodeTextBox.ReadOnly = true;

                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "argument Exception");
                    productsBindingSource.CancelEdit();
                }
                catch (DBConcurrencyException)
                {
                    MessageBox.Show("A concurrency error has occurred. " + "Some rows were not updated.",
                        "Concurrency Exception");
                    this.productsTableAdapter.Fill(this.techSupportDataSet.Products);

                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                    productsBindingSource.CancelEdit();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error # " + ex.Number + ": " +
                        ex.Message, ex.GetType().ToString());
                }
            }
            else
            {
                try
                {
                    this.tableAdapterManager.UpdateAll(this.techSupportDataSet);
                    
                }
                catch (DBConcurrencyException)
                {
                    MessageBox.Show("A concurrency error has occurred. " + "Some rows were not updated.",
                        "Concurrency Exception");
                    this.productsTableAdapter.Fill(this.techSupportDataSet.Products);

                }

                catch (SqlException ex)
                {
                    MessageBox.Show("Database error # " + ex.Number + ": " +
                        ex.Message, ex.GetType().ToString());
                }
                
            }
        }

        private void frmProductMaintenance_Load(object sender, EventArgs e)
        {
            this.productsTableAdapter.Fill(this.techSupportDataSet.Products);

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            String shortDate = currentDateTime.ToShortDateString();
            productCodeTextBox.ReadOnly = false;
            releaseDateTextBox.Text = shortDate;
        }
        
        //handling for the fill by product code tool strip
        //will fill the whole data set if the text box is empty
        private void fillByProductCodeToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                string productCode = productCodeToolStripTextBox.Text;
                if (productCode == "")
                {
                    this.productsTableAdapter.Fill(this.techSupportDataSet.Products);
                }
                else
                {
                    this.productsTableAdapter.FillByProductCode(this.techSupportDataSet.Products, productCode);
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private bool IsValidData()
        {
            return
             Validator.IsPresent(productCodeTextBox) &&
             Validator.IsCorrectLength(productCodeTextBox) &&
             Validator.IsPresent(nameTextBox) &&
             Validator.IsPresent(versionTextBox) &&
             Validator.IsDecimal(versionTextBox);
            
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            if (productCodeTextBox.ReadOnly == false)
            {
                productsBindingSource.CancelEdit();
                productCodeTextBox.ReadOnly = true;
            }
        }
    }
}
