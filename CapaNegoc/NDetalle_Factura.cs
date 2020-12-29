using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NDetalle_Factura
    {
        public static DataTable Mostrar(String IdFactura)
        {
            return new DDetalle_Factura().Mostrar(IdFactura);
        }
    }
}
