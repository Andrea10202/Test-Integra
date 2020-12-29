using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class FrmTarjeta : Form
    {
        private bool IsNuevo = false;
        private bool IsModificar = false;
        private bool IsEliminar = false;

        public FrmTarjeta()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre de la tarjeta");
        }

        private void MensajeOK (string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError (string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            this.txtId.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
        }

        private void Habilitar(bool valor)
        {
            //this.txtId.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
        }

        private void Botones()
        {
            if (this.IsNuevo || this.IsModificar || this.IsEliminar)
            {
                this.Habilitar(true);
                this.btnGuardar.Enabled = true;
                this.btnCancelar.Enabled = true;
            }
        else
            {
                this.Habilitar(false);
                this.btnGuardar.Enabled = false;
                this.btnCancelar.Enabled = false;
            }
        }

        private void OcultarColumnas()
        {
            this.dataListado.Columns[3].Visible = false;
        }

        private void  Mostrar()
        {
            this.dataListado.DataSource = NTarjeta.Mostrar();
            this.OcultarColumnas();
            if (this.dataListado.RowCount == 0)
            {
                this.btnModif.Enabled = false;
                this.btnEliminar.Enabled = false;
            }
        }

        private void BuscarNombre()
        {
            this.dataListado.DataSource = NTarjeta.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
        }

        private void FrmTarjeta_Load(object sender, EventArgs e)
        {
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
            this.OcultarColumnas();
            this.dataListado.Columns[0].Visible = false;
            if (this.dataListado.RowCount != 0)
            {
                this.dataListado.Rows[0].Selected = false;
            }
            this.btnModif.Enabled = false;
            this.btnEliminar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
            this.OcultarColumnas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rta = "";
                if (txtNombre.Text==string.Empty)
                {
                    MensajeError("Falta ingresar datos");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rta = NTarjeta.Insertar(this.txtNombre.Text.Trim().ToUpper());
                    }
                    else
                    {
                        if (this.IsModificar)
                        {
                            rta = NTarjeta.Modificar(Convert.ToInt32(this.txtId.Text), this.txtNombre.Text.Trim().ToUpper());
                        }
                        else
                        {
                            DialogResult Opcion;
                            Opcion = MessageBox.Show("Confirma la eliminación del registro?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            string Codigo;
                            if (Opcion == DialogResult.OK)
                            {
                                Codigo = this.txtId.Text;
                                rta = NTarjeta.Eliminar(Convert.ToInt32(Codigo));
                            }
                        }
                    }
                    if (rta.Equals ("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOK("La tarjeta se registró en forma correcta");
                        }
                        else
                        {
                            if (this.IsModificar)
                            {
                                this.MensajeOK("La tarjeta se modificó en forma correcta");
                            }
                            else
                            {
                                this.MensajeOK("La tarjeta se eliminó en forma correcta");
                            }
                        }
                    }
                    else
                    {
                        this.MensajeError(rta);
                    }
                    this.IsNuevo = false;
                    this.IsModificar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                    this.tabControl1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsModificar = false;
            this.IsEliminar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
            this.tabControl1.SelectedIndex = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
            this.IsNuevo = true;
            this.IsModificar = true;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }

        private void btnModif_Click(object sender, EventArgs e)
        {
          
            this.txtId.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tarj_id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tarj_nombre"].Value);
            this.tabControl1.SelectedIndex = 1;
            IsModificar = true;
            this.btnGuardar.Text = "Guardar";
            this.Habilitar(true);
            if (!this.txtId.Text.Equals(""))
            {
                this.IsModificar = true;
                this.Botones();
                this.Habilitar(true);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.txtId.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tarj_id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tarj_nombre"].Value);
            this.tabControl1.SelectedIndex = 1;
            this.IsEliminar = true;
            this.btnGuardar.Text = "Aceptar";
            this.Botones();
            this.Habilitar(false);
        }

        private void dataListado_SelectionChanged(object sender, EventArgs e)
        {
            this.btnModif.Enabled = true;
            this.btnEliminar.Enabled = true;
        }
    }
}

