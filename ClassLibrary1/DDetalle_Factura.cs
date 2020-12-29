using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DDetalle_Factura
    {
        private int _IdDetalle;
        private int _IdFactura;
        private int _IdProducto;
        private decimal _PU;
        private int _Cantidad;
        private decimal _Subtotal;

        public int IdDetalle
        {
            get { return _IdDetalle; }
            set { _IdDetalle = value; }
        }

        public int IdFactura
        {
            get { return _IdFactura; }
            set { _IdFactura = value; }
        }

        public int IdProducto
        {
            get { return _IdProducto; }
            set { _IdProducto = value; }
        }

        public decimal PU
        {
            get { return _PU; }
            set { _PU = value; }
        }

        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        public decimal Subtotal
        {
            get { return _Subtotal; }
            set { _Subtotal = value; }
        }

        public DDetalle_Factura()
        {
        }

        public DDetalle_Factura(int IdDetalle,int IdFactura, int IdProducto, decimal PU, int Cantidad, decimal Subtotal)
        {
            this.IdDetalle = IdDetalle;
            this.IdFactura = IdFactura;
            this.IdProducto = IdProducto;
            this.PU = PU;
            this.Cantidad = Cantidad;
            this.Subtotal = Subtotal;
        }

        public string Insertar(DDetalle_Factura DetalleFactura, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rta = "";
            try
            {
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsertar_detalle_factura";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdDetalle = new SqlParameter();
                ParIdDetalle.ParameterName = "@iddetalle";
                ParIdDetalle.SqlDbType = SqlDbType.Int;
                ParIdDetalle.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdDetalle);

                SqlParameter ParIdFactura = new SqlParameter();
                ParIdFactura.ParameterName = "@idfactura";
                ParIdFactura.SqlDbType = SqlDbType.Int;
                ParIdFactura.Value = DetalleFactura.IdFactura;
                SqlCmd.Parameters.Add(ParIdFactura);

                SqlParameter ParIdProducto = new SqlParameter();
                ParIdProducto.ParameterName = "@idproducto";
                ParIdProducto.SqlDbType = SqlDbType.Int;
                ParIdProducto.Value = DetalleFactura.IdProducto;
                SqlCmd.Parameters.Add(ParIdProducto);

                SqlParameter ParIdCantidad = new SqlParameter();
                ParIdCantidad.ParameterName = "@cantidad";
                ParIdCantidad.SqlDbType = SqlDbType.Int;
                ParIdCantidad.Value = DetalleFactura.Cantidad;
                SqlCmd.Parameters.Add(ParIdCantidad);

                SqlParameter ParIdPU = new SqlParameter();
                ParIdPU.ParameterName = "@pu";
                ParIdPU.SqlDbType = SqlDbType.Money;
                ParIdPU.Value = DetalleFactura.PU;
                SqlCmd.Parameters.Add(ParIdPU);

                rta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se ingreso el registro";
            }
            catch (Exception ex)
            {
                rta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rta;
        }

        // Metodo Mostrar
        public DataTable Mostrar(string IdFactura)
        {
            DataTable DtResultado = new DataTable("tblFactura_Detalle");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_detalle_factura";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdFactura = new SqlParameter();
                ParIdFactura.ParameterName = "@idfactura";
                ParIdFactura.SqlDbType = SqlDbType.Text;
                ParIdFactura.Size = 100;
                ParIdFactura.Value = IdFactura;
                SqlCmd.Parameters.Add(ParIdFactura);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch
            {
                DtResultado = null;
            }
            return DtResultado;
        }
    }
}
