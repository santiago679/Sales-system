using SalesSystem.BLL;
using SalesSystem.Entities;
using System;
using System.Windows.Forms;

namespace SalesSystem.UI.Products
{
    public partial class EditProduct : Form
    {
        private readonly ProductBLL productBLL;
        private readonly int productID;

        public EditProduct(int productId)
        {
            InitializeComponent();
            productBLL = new ProductBLL();
            productID = productId;

            // Inicializar ComboBox
            cmbActive.Items.Add("Activo");
            cmbActive.Items.Add("Inactivo");
            cmbActive.SelectedIndex = 0;

            LoadProductData(); // Precargar datos del producto
        }

        private void LoadProductData()
        {
            Product product = productBLL.GetProductById(productID);
            if (product != null)
            {
                txtName.Text = product.ProductName;
                txtDescription.Text = product.Description;
                txtPrice.Text = product.Price.ToString("0.00");
                cmbActive.SelectedIndex = product.IsActive ? 0 : 1;
            }
            else
            {
                MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de precio
                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Precio inválido. Ingresa un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Product product = new Product
                {
                    ProductID = productID,
                    ProductName = txtName.Text,
                    Description = txtDescription.Text,
                    Price = price,
                    IsActive = (cmbActive.SelectedIndex == 0)
                };

                productBLL.UpdateProduct(product);
                MessageBox.Show("Producto editado correctamente ✅", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductForm productForm = new ProductForm();
                productForm.Show();
                this.Close();

                // Cerrar el formulario después de editar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm(); // crea una nueva instancia
            this.Close();                                // cierra EditProduct
            productForm.Show();
        }
    }
}