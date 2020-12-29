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
    public partial class FrmFactura : Form
    {
        public FrmFactura()
        {
            InitializeComponent();
        }

        private void Habilitar(bool valor)
        {
            this.txtIdFactura.ReadOnly = !valor;
            this.dateFechaFactura.Enabled = !valor;
            this.txtCliente.ReadOnly = !valor;
            this.txtEmpleado.ReadOnly = !valor;
        }


        private void Mostrar()
        {
            this.dataListado.DataSource = NFactura.Mostrar();
        }

        private void BuscarNombre()
        {
            //this.dataListado.DataSource = NFactura.BuscarNombre(this.txtNombreCliente.Text);
            this.dataListado.DataSource = NFactura.BusquedaAvanzada(this.txtNombreCliente.Text, Convert.ToString(this.dateFechaDesde.Value).Substring(0, 10), Convert.ToString(this.dateFechaHasta.Value).Substring(0, 10));
        }

        private void FrmFactura_Load(object sender, EventArgs e)
        {
            this.Mostrar();
            dateFechaDesde.Value = DateTime.Now;
            dateFechaHasta.Value = DateTime.Now;
            this.BuscarNombre();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtNombreCliente_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            decimal TotalAux;
            TotalAux = Convert.ToDecimal(this.dataListado.CurrentRow.Cells["total"].Value);
            this.txtIdFactura.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["id"].Value);
            this.dateFechaFactura.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["fecha"].Value);
            this.txtCliente.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["cliente"].Value);
            this.txtEmpleado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["empleado"].Value);
            this.lblTotal.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["total"].Value);
            this.dataDetalle.DataSource = NDetalle_Factura.Mostrar(Convert.ToString(this.dataListado.CurrentRow.Cells["id"].Value));
            this.dataDetalle.Columns[0].Visible = false;
            this.dataDetalle.Columns[1].Visible = false;
            this.dataDetalle.Columns[2].Visible = false;
            this.tabFactura.SelectedIndex = 1;
            this.Habilitar(false);
        }

    }
}
