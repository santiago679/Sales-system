using SalesSystem.BLL;
using SalesSystem.Entities;   // Aquí está tu Product entity
using SalesSystem.UI.Products;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalesSystem.UI
{
    public partial class ProductForm : Form
    {
        private readonly ProductBLL productBLL;
        private AdminForm _adminForm; // nueva referencia al AdminForm

        // Constructor original
        public ProductForm()
        {
            InitializeComponent();
            productBLL = new ProductBLL();
            LoadProducts();
        }

        // Nuevo constructor que recibe el AdminForm
        public ProductForm(AdminForm adminForm)
        {
            InitializeComponent();
            productBLL = new ProductBLL();
            LoadProducts();
            _adminForm = adminForm; // guardamos la referencia
        }

        private void LoadProducts()
        {
            List<Product> products = productBLL.GetAllProducts();
            dataGridView1.DataSource = null; // resetear en caso de recarga
            dataGridView1.DataSource = products;

            dataGridView1.Columns["ProductID"].HeaderText = "ID";
            dataGridView1.Columns["ProductName"].HeaderText = "Name";
            dataGridView1.Columns["Description"].HeaderText = "Description";
            dataGridView1.Columns["Price"].HeaderText = "Price";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProduct add = new AddProduct();
            add.FormClosed += (s, args) => LoadProducts();
            add.ShowDialog();
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int productID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
                EditProduct editProduct = new EditProduct(productID);
                this.Close();
                editProduct.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int productID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
                DialogResult result = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este registro?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    productBLL.DeleteProduct(productID);
                    this.Close();
                    ProductForm productForm = new ProductForm();
                    productForm.Show();
                }
            }
        }


        private void btnBack_Click_1(object sender, EventArgs e)
        {
            AdminForm admin = new AdminForm();
            this.Close();
            admin.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
