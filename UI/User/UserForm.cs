using SalesSystem.BLL;
using SalesSystem.UI.User;
using System;
using System.Windows.Forms;

namespace SalesSystem.UI
{
    public partial class UserForm : Form
    {
        private AdminForm adminForm;
        private UserBLL userBLL;
        public UserForm(AdminForm admin)
        {
            InitializeComponent();
            adminForm = admin;
            userBLL = new UserBLL();
            CargarUsuarios();
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Cuando cierres el UserForm, mostramos de nuevo el AdminForm
            adminForm.Show();
        }

        private void CargarUsuarios()
        {
            try
            {
                var usuarios = userBLL.GetAll(); // obtienes la lista de usuarios
                dataGridView1.DataSource = usuarios; // la asignas al DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            if (addUser.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int userId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["UserID"].Value);

                EditUser editUser = new EditUser(userId, this);
                this.Hide();

                if (editUser.ShowDialog() == DialogResult.OK)
                {
                    CargarUsuarios();
                }

                this.Show();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un usuario para editar.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int userId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["UserID"].Value);

                DialogResult result = MessageBox.Show(
                    "¿Estás seguro de que deseas eliminar este usuario?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int filasAfectadas =  userBLL.DeleteUser(userId);
                        
                        if(filasAfectadas > 0)
                        {
                            MessageBox.Show("Usuario eliminado correctamente");
                            CargarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido eliminar el usuario");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error eliminando usuario: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un usuario para eliminar.");
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            adminForm.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AddUser addUserForm = new AddUser(this);
            this.Hide();
            addUserForm.Show();
        }

        

    }
}

