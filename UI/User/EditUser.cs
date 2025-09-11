using SalesSystem.BLL;
using SalesSystem.Entities;
using System;
using System.Windows.Forms;
using UserEntity = SalesSystem.Entities.User;

namespace SalesSystem.UI.User
{
    public partial class EditUser : Form
    {
        private readonly UserBLL userBLL;
        private readonly int userId;
        private UserEntity userToEdit;
        private UserForm userForm; // referencia al formulario padre

        // Constructor recibe también el UserForm
        public EditUser(int id, UserForm form)
        {
            InitializeComponent();
            userBLL = new UserBLL();
            userId = id;
            userForm = form;

            // Cargar datos del usuario seleccionado
            userToEdit = userBLL.GetById(userId);

            if (userToEdit != null)
            {
                txtName.Text = userToEdit.FullName;
                txtidentity.Text = userToEdit.IdentityNumber.ToString();
                txtphone.Text = userToEdit.Phone;
                txtaddress.Text = userToEdit.Address;
                txtemail.Text = userToEdit.Email;
                txtpassword.Text = userToEdit.PasswordHash;
                cmbrole.SelectedItem = userToEdit.Role;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                userToEdit.FullName = txtName.Text;
                userToEdit.IdentityNumber = long.Parse(txtidentity.Text);
                userToEdit.Phone = txtphone.Text;
                userToEdit.Address = txtaddress.Text;
                userToEdit.Email = txtemail.Text;
                userToEdit.PasswordHash = txtpassword.Text;
                userToEdit.Role = cmbrole.SelectedItem.ToString();

                userBLL.UpdateUser(userToEdit);

                MessageBox.Show("Usuario actualizado correctamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar usuario: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();        // Cierra EditUser
            userForm?.Show();    // Muestra UserForm si existe referencia
        }
    }
}
