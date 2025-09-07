using SalesSystem.BLL;
using SalesSystem.Entities;
using System;
using System.Windows.Forms;

namespace SalesSystem.UI.User
{
    public partial class AddUser : Form
    {
        private readonly UserBLL userBLL;
        private UserForm userForm; // referencia al formulario padre

        // Constructor vacío (por si lo necesitas en otros lados)
        public AddUser()
        {
            InitializeComponent();
            userBLL = new UserBLL();
        }

        // Constructor con referencia al UserForm
        public AddUser(UserForm form)
        {
            InitializeComponent();
            userBLL = new UserBLL();
            userForm = form;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SalesSystem.Entities.User nuevo = new SalesSystem.Entities.User
                {
                    FullName = txtName.Text,
                    IdentityNumber = long.Parse(txtidentity.Text),
                    Phone = txtphone.Text,
                    Address = txtaddress.Text,
                    Email = txtemail.Text,
                    PasswordHash = txtpassword.Text,
                    Role = cmbrole.SelectedItem.ToString(),
                    RegistrationDate = DateTime.Now
                };

                userBLL.Add(nuevo);

                MessageBox.Show("Usuario agregado correctamente ✅");

                // Esto notifica al UserForm que debe refrescar
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar usuario: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();       // Cierra AddUser
            userForm?.Show();   // Vuelve al UserForm si existe referencia
        }
    }
}
