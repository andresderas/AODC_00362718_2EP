using System;
using System.Windows.Forms;

namespace Segundo_parcial
{
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }

        private void frmInicioSesion_Load(object sender, EventArgs e)
        {
            poblarControles();
        }
        private void poblarControles()
        {
            //Actualizar combobox
            cmbUser.DataSource = null;
            cmbUser.ValueMember = "password";
            cmbUser.DisplayMember = "username";
            cmbUser.DataSource = UsuarioDAO.getLista();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            if (cmbUser.SelectedValue.Equals(txbContraseña.Text))
            {
                Usuario u = (Usuario)cmbUser.SelectedItem;

                MessageBox.Show("¡Bienvenido!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmPrincipal ventana = new frmPrincipal(u);
                ventana.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("¡Contraseña incorrecta!", "Hugo App",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txbContraseña.Clear();
            }
        }

        private void txbContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnIniciarSesion_Click(sender, e);
        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            frmCambiarContra unaVentana = new frmCambiarContra();
            unaVentana.ShowDialog();
            poblarControles();
        }
    }
}
