using SalesSystem.BLL;
using SalesSystem.DTOs;
using SalesSystem.DTOs.User;
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
            var loginDTO = new UserLoginDTO
            {
                Email = txtEmail.Text,
                Password = txtPassword.Text,
            };

            try
            {
                AuthenticatedUserDTO authenticatedUserDTO = userBLL.Login(loginDTO);

                MessageBox.Show($"Welcome {authenticatedUserDTO.FullName}! Your role is {authenticatedUserDTO.Role}.",
                                "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Determinar el rol del usuario para redirigir al respectivo formulario
                if (authenticatedUserDTO.Role == "Administrator")
                {
                    var adminForm = new AdminForm();
                    adminForm.Show();
                }
                else if (authenticatedUserDTO.Role == "Seller")
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
