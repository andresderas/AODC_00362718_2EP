using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using CartesianChart = LiveCharts.WinForms.CartesianChart;

namespace Segundo_parcial
{
    public partial class frmPrincipal : Form
    {
        private Usuario usuario;
        private CartesianChart graficoEstadisticas;
        public frmPrincipal(Usuario pUsuario)
        {
            InitializeComponent();
            usuario = pUsuario;

            if (usuario.userType)
            {
                graficoEstadisticas = new CartesianChart();
                this.Controls.Add(graficoEstadisticas);
                graficoEstadisticas.Parent = tabControl1.TabPages[8];
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text =
                "Bienvenido " + usuario.username + " [" + (usuario.userType ? "Administrador" : "Usuario") + "]";

            if (usuario.userType)
            {
                tabControl1.TabPages[4].Parent = null;
                tabControl1.TabPages[4].Parent = null;
                tabControl1.TabPages[4].Parent = null;
                tabControl1.TabPages[4].Parent = null;
                configurarGrafico();
                actualizarControlesAd();
            }
            else
            {
                tabControl1.TabPages[0].Parent = null;
                tabControl1.TabPages[0].Parent = null;
                tabControl1.TabPages[0].Parent = null;
                tabControl1.TabPages[0].Parent = null;
                tabControl1.TabPages[4].Parent = null;
                actualizarControlesUs();
            }
        }

        private void actualizarControlesAd()
        {
            // Realizar consulta a la base de datos
            List<Usuario> lista = UsuarioDAO.getLista();
            List<Business> lista2 = BusinessDAO.getLista();
            List<Apporder> lista4 = ApporderDAO.getAll();
            // Menu / Usuarios (data grid view)
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            // Menu / Usuarios desplegable (combo box)
            cmbUsuario.DataSource = null;
            cmbUsuario.ValueMember = "password";
            cmbUsuario.DisplayMember = "username";
            cmbUsuario.DataSource = lista;
            // Menu  / Negocio (data grid view)
            dataGridView5.DataSource = null;
            dataGridView5.DataSource = lista2;
            // Menu / Negocio (combo box)
            cmbNegocio0.DataSource = null;
            cmbNegocio0.ValueMember = "idBusiness";
            cmbNegocio0.DisplayMember = "name";
            cmbNegocio0.DataSource = lista2;
            // Menu / Productos (combo box1)
            cmbNegocioP.DataSource = null;
            cmbNegocioP.ValueMember = "idBusiness";
            cmbNegocioP.DisplayMember = "name";
            cmbNegocioP.DataSource = lista2;
            // Menu / Productos (combo box2)
            cmbNegocio2.DataSource = null;
            cmbNegocio2.ValueMember = "idBusiness";
            cmbNegocio2.DisplayMember = "name";
            cmbNegocio2.DataSource = lista2;
            // Menu / Productos (combo box3)
            int id = Convert.ToInt32(cmbNegocio2.SelectedValue.ToString());
            List<Product> lista3 = ProductDAO.getLista(id);
            cmbProducto0.DataSource = null;
            cmbProducto0.ValueMember = "idProduct";
            cmbProducto0.DisplayMember = "name";
            cmbProducto0.DataSource = lista3;
            // Menu  / Pedidos (data grid view)
            dgvPedidosR.DataSource = null;
            dgvPedidosR.DataSource = lista4;
            //Grafico con estadisticas
            poblarGrafico();
        }

        private void configurarGrafico()
        {
            graficoEstadisticas.Top = 10;
            graficoEstadisticas.Left = 10;
            graficoEstadisticas.Width= graficoEstadisticas.Parent.Width - 20;
            graficoEstadisticas.Height = graficoEstadisticas.Parent.Height - 20;

            graficoEstadisticas.Series = new SeriesCollection
            {
                new ColumnSeries{Title = "Cantidad de pedidos por negocios", Values = new ChartValues<int>(), DataLabels = true}
            };
            graficoEstadisticas.AxisX.Add(new Axis { Labels = new List<string>() });
            graficoEstadisticas.AxisX[0].Separator = new Separator() { Step = 1, IsEnabled = false };
            graficoEstadisticas.LegendLocation = LegendLocation.Top;
        }
        private void poblarGrafico()
        {
            graficoEstadisticas.Series[0].Values.Clear();
            graficoEstadisticas.AxisX[0].Labels.Clear();

            foreach (Estadisticas E in BusinessDAO.getEstadisticas())
            {
                graficoEstadisticas.Series[0].Values.Add(E.cantidad);
                graficoEstadisticas.AxisX[0].Labels.Add(E.business);
            }
        }

        private void actualizarControlesUs()
        {
            //Realizar consulta a la base de datos
            List<Business> lista2 = BusinessDAO.getLista();
            List<Address> lista5 = AddressDAO.getLista(usuario.idUser);
            List<Apporder> lista6 = ApporderDAO.getUser(usuario.idUser);
            // Menu  / Direcciones (data grid view)
            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataSource = lista5;
            // Menu / Modificar (combo box)
            cmbDireccion1.DataSource = null;
            cmbDireccion1.ValueMember = "idAddress";
            cmbDireccion1.DisplayMember = "address";
            cmbDireccion1.DataSource = lista5;
            // Menu / Modificar (combo box2)
            cmbDireccion3.DataSource = null;
            cmbDireccion3.ValueMember = "idAddress";
            cmbDireccion3.DisplayMember = "address";
            cmbDireccion3.DataSource = lista5;
            // Menu / Realizar pedido (combo box1)
            cmbNegocio.DataSource = null;
            cmbNegocio.ValueMember = "idBusiness";
            cmbNegocio.DisplayMember = "name";
            cmbNegocio.DataSource = lista2;
            // Menu / Realizar pedido (combo box2)
            int id = Convert.ToInt32(cmbNegocio.SelectedValue.ToString());
            List<Product> lista3 = ProductDAO.getLista(id);
            cmbProducto.DataSource = null;
            cmbProducto.ValueMember = "idProduct";
            cmbProducto.DisplayMember = "name";
            cmbProducto.DataSource = lista3;
            // Menu / Realizar pedido (combo box3)
            cmbDireccion.DataSource = null;
            cmbDireccion.ValueMember = "idAddress";
            cmbDireccion.DisplayMember = "address";
            cmbDireccion.DataSource = lista5;
            // Menu / Pedidos (combo box)
            cmbPedido.DataSource = null;
            cmbPedido.ValueMember = "idOrder";
            cmbPedido.DisplayMember = "idOrder";
            cmbPedido.DataSource = lista6;
            // Menu  / Direcciones (data grid view)
            dgvPedidosU.DataSource = null;
            dgvPedidosU.DataSource = lista6;

        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir, " + usuario.username + "?",
                "Hugo App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cmbNegocio2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cmbNegocio2.SelectedValue?.ToString());
            List<Product> lista3 = ProductDAO.getLista(id);
            cmbProducto0.DataSource = null;
            cmbProducto0.ValueMember = "idProduct";
            cmbProducto0.DisplayMember = "name";
            cmbProducto0.DataSource = lista3;
        }

        private void cmbNegocio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cmbNegocio.SelectedValue?.ToString());
            List<Product> lista3 = ProductDAO.getLista(id);
            cmbProducto.DataSource = null;
            cmbProducto.ValueMember = "idProduct";
            cmbProducto.DisplayMember = "name";
            cmbProducto.DataSource = lista3;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (txbUsuario.Text.Length >= 5)
                {
                    UsuarioDAO.crearNuevo(txbNombreC.Text, txbUsuario.Text);

                    MessageBox.Show("¡Usuario agregado exitosamente! Valores por defecto: " +
                                    "contrasena igual a usuario y no administrador.",
                        "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txbUsuario.Clear();
                    txbNombreC.Clear();
                    actualizarControlesAd();
                }
                else
                    MessageBox.Show("Favor digite un usuario valido (longitud minima, 5 caracteres)",
                        "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Usuario no disponible.",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar al usuario " + cmbUsuario.Text + "?",
               "Hugo App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                UsuarioDAO.eliminar(cmbUsuario.Text);

                MessageBox.Show("¡Usuario eliminado exitosamente!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                actualizarControlesAd();
            }
        }

        private void btnCrearNegocio_Click(object sender, EventArgs e)
        {
            if (txbNegocio.Text.Equals("") ||
               txbdescripcion.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios", "Hugo App", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    BusinessDAO.crearNuevo(txbNegocio.Text, txbdescripcion.Text);
                    MessageBox.Show("Negocio agregado exitosamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbNegocio.Clear();
                    txbdescripcion.Clear();

                    actualizarControlesAd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error intente nuevamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //Boton borrar de tab Negocios
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar el negocio " + cmbNegocio0.Text + "?",
                "Hugo App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(cmbNegocio0.SelectedValue.ToString());
                BusinessDAO.eliminar(id);

                MessageBox.Show("¡Negocio eliminado exitosamente!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                actualizarControlesAd();
            }
        }

        private void btnCrearP_Click(object sender, EventArgs e)
        {
            if (txbProducto.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios", "Hugo App", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(cmbNegocioP.SelectedValue.ToString());
                    ProductDAO.crearNuevo(id, txbProducto.Text);
                    MessageBox.Show("Producto agregado exitosamente a " + cmbNegocioP.Text , "Hugo App",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbProducto.Clear();
                    actualizarControlesAd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error intente nuevamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar el producto " + cmbProducto0.Text + "?",
                "Hugo App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(cmbProducto0.SelectedValue.ToString());
                ProductDAO.eliminar(id);

                MessageBox.Show("¡Producto eliminado exitosamente!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                actualizarControlesAd();
            }
        }

        private void btnAgregarD_Click(object sender, EventArgs e)
        {
            if (txbDireccion.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios", "Hugo App", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    AddressDAO.crearNuevo(usuario.idUser, txbDireccion.Text);
                    MessageBox.Show("Direccin agregada exitosamente " + usuario.username, "Hugo App",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbDireccion.Clear();
                    actualizarControlesUs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error intente nuevamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txbNuevaD.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios", "Hugo App", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(cmbDireccion1.SelectedValue.ToString());
                    AddressDAO.actualizar(txbNuevaD.Text, id);
                    MessageBox.Show("Direccion actualizada exitosamente " + usuario.username, "Hugo App",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbNuevaD.Clear();
                    actualizarControlesUs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error intente nuevamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar la direccion?",
                "Hugo App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(cmbDireccion3.SelectedValue.ToString());
                AddressDAO.eliminar(id);

                MessageBox.Show("¡Dirección eliminada exitosamente!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                actualizarControlesUs();
            }
        }

        private void btnRealizarOrden_Click(object sender, EventArgs e)
        {
            if (cmbNegocio.Text.Equals("") ||
               cmbProducto.Text.Equals("") ||
               cmbDireccion.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios", "Hugo App", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    int idP = Convert.ToInt32(cmbProducto.SelectedValue.ToString());
                    int idA = Convert.ToInt32(cmbDireccion.SelectedValue.ToString());
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    ApporderDAO.crearNuevo(date, idP, idA);
                    MessageBox.Show("Pedido realizado exitosamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    actualizarControlesUs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error intente nuevamente", "Hugo App",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar su pedido?",
                "Hugo App", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(cmbPedido.SelectedValue.ToString());
                ApporderDAO.eliminar(id);

                MessageBox.Show("¡Pedido eliminado exitosamente!",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);

                actualizarControlesUs();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            actualizarControlesAd();
        }
    }
}
