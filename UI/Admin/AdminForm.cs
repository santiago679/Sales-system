
using SalesSystem.UI.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesSystem.UI
{
    public partial class AdminForm : Form
    {

        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm(this);
            productForm.Show(); // mostramos el AdminForm original
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Abrimos UserForm y ocultamos AdminForm
            UserForm userForm = new UserForm(this);
            userForm.Show();
            this.Hide();
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm(this);
            this.Hide();
            userForm.Show();
        }
    }
}
