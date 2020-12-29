using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NFactura
    {
        public static string Insertar(int IdFactura, DateTime FechaFactura, int PersonaId, string ClienteNombre, int EmpleadoId, string EmpleadoNombre, int EmpleadoLegajo, decimal Total, DataTable dtDetalle)
        {
            DFactura Obj = new DFactura();
            Obj.IdFactura = IdFactura;
            Obj.FechaFactura = FechaFactura;
            Obj.PersonaId = PersonaId;
            Obj.ClienteNombre = ClienteNombre;
            Obj.EmpleadoId = EmpleadoId;
            Obj.EmpleadoNombre = EmpleadoNombre;
            Obj.EmpleadoLegajo = EmpleadoLegajo;
            Obj.Total = Total;
            List<DDetalle_Factura> detalles = new List<DDetalle_Factura>();
            foreach (DataRow row in dtDetalle.Rows)
            {
                DDetalle_Factura detalle = new DDetalle_Factura();
                detalle.IdFactura = Convert.ToInt32(row["idfactura"].ToString());
                detalle.IdProducto = Convert.ToInt32(row["IdProducto"].ToString());
                detalle.PU = Convert.ToDecimal(row["PU"].ToString());
                detalle.Cantidad = Convert.ToInt32(row["Cantidad"].ToString());
                detalle.Subtotal = Convert.ToDecimal(row["Subtotal"].ToString());
            }
            return Obj.Insertar(Obj, detalles);
        }

        //Metodo para mostrar 
        public static DataTable Mostrar()
        {
            return new DFactura().Mostrar();
        }

        //Metodo para buscar nombre
        public static DataTable BuscarNombre(string textobuscar)
        {
            DFactura Obj = new DFactura();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombreCliente(Obj);
        }

        //Metodo para buscar nombre y rango de fechas
        public static DataTable BusquedaAvanzada(string textobuscar, string FechaDesde, string FechaHasta)
        {
            DFactura Obj = new DFactura();
            Obj.TextoBuscar = textobuscar;
            Obj.FechaDesde = FechaDesde;
            Obj.FechaHasta = FechaHasta;
            return Obj.BusquedaAvanzada(Obj);
        }
    }
}
