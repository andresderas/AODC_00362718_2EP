using System;
using System.Windows.Forms;

namespace Segundo_parcial
{
    public partial class frmCambiarContra : Form
    {
        public frmCambiarContra()
        {
            InitializeComponent();
        }

        private void frmCambiarContra_Load(object sender, EventArgs e)
        {
            cmbUsuario.DataSource = null;
            cmbUsuario.ValueMember = "password";
            cmbUsuario.DisplayMember = "username";
            cmbUsuario.DataSource = UsuarioDAO.getLista();
        }

        private void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            bool actualIgual = cmbUsuario.SelectedValue.Equals(txbActual.Text);
            bool nuevaIgual = txbNueva.Text.Equals(txbRepetir.Text);
            bool nuevaValida = txbNueva.Text.Length > 0;

            if (actualIgual && nuevaIgual && nuevaValida)
            {
                try
                {
                    
                    UsuarioDAO.actualizarContra(cmbUsuario.Text, txbNueva.Text);

                    MessageBox.Show("¡Contraseña actualizada exitosamente!",
                        "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Contraseña no actualizada! Favor intente mas tarde.",
                        "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("¡Favor verifique que los datos sean correctos!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
