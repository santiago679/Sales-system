using SalesSystem.BLL;
using SalesSystem.Entities;
using SalesSystem.UI;
using System;
using System.Windows.Forms;

namespace SalesSystem
{
    public partial class Login : Form
    {
        private readonly UserBLL userBLL;

        public Login()
        {
            InitializeComponent();
            userBLL = new UserBLL();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                User user = userBLL.Login(email, password);

                MessageBox.Show($"Welcome {user.FullName}! Your role is {user.Role}.",
                                "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Determinar el rol del usuario para redirigir al respectivo formulario
                if (user.Role == "Administrator")
                {
                    var adminForm = new AdminForm();
                    adminForm.Show();
                }
                else if (user.Role == "Seller")
                {
                    var sellerForm = new SellerForm();
                    sellerForm.Show();
                }

                this.Hide(); // Oculta el formulario de login
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
