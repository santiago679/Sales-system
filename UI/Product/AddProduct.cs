using System;
using System.Windows.Forms;
using SalesSystem.Entities;
using SalesSystem.BLL;

namespace SalesSystem.UI
{
    public partial class AddProduct : Form
    {
        private readonly ProductBLL productBLL;

        public AddProduct()
        {
            InitializeComponent();
            productBLL = new ProductBLL();

            // Rellenar ComboBox con opciones de activo/inactivo
            cmbActive.Items.Add("Activo");
            cmbActive.Items.Add("Inactivo");
            cmbActive.SelectedIndex = 0; // Por defecto Activo
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product
                {
                    ProductName = txtName.Text,
                    Description = txtDescription.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    IsActive = (cmbActive.SelectedIndex == 0)
                };

                string result = productBLL.AddProduct(product);

                MessageBox.Show(result, "Resultado",
                    MessageBoxButtons.OK,
                    result.Contains("✅") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                if (result.Contains("✅"))
                {
                    txtName.Clear();
                    txtDescription.Clear();
                    txtPrice.Clear();
                    cmbActive.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            this.Close();
            productForm.Show();
        }
    }
}
