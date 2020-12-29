using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DFactura
    {
        private int _IdFactura;
        private DateTime _FechaFactura;
        private int _PersonaId;
        private string _ClienteNombre;
        private int _EmpleadoId;
        private string _EmpleadoNombre;
        private int _EmpleadoLegajo;
        private decimal _Total;
        private string _TextoBuscar;
        private string _FechaDesde;
        private string _FechaHasta;

        public int IdFactura
        {
            get { return _IdFactura; }
            set { _IdFactura = value; }
        }

        public DateTime FechaFactura
        {
            get { return _FechaFactura; }
            set { _FechaFactura = value; }
        }

        public int PersonaId
        {
            get { return _PersonaId; }
            set { _PersonaId = value; }
        }

        public string ClienteNombre
        {
            get { return _ClienteNombre; }
            set { _ClienteNombre = value; }
        }

        public int EmpleadoId
        {
            get { return _EmpleadoId; }
            set { _EmpleadoId = value; }
        }

        public string EmpleadoNombre
        {
            get { return _EmpleadoNombre; }
            set { _EmpleadoNombre = value; }
        }

        public int EmpleadoLegajo
        {
            get { return _EmpleadoLegajo; }
            set { _EmpleadoLegajo = value; }
        }

        public decimal Total
        {
            get { return _Total;}
            set {_Total = value;}
        }

        public string TextoBuscar
        {
            get { return _TextoBuscar; }
            set { _TextoBuscar = value; }
        }

        public string FechaDesde
        {
            get { return _FechaDesde; }
            set { _FechaDesde = value; }
        }

        public string FechaHasta
        {
            get { return _FechaHasta; }
            set { _FechaHasta = value; }
        }

        //Constructor vacio
        public DFactura()
        {
        }

        //constructor con parametros
        public DFactura(int IdFactura, DateTime FechaFactura, int PersonaId, string ClienteNombre, int EmpleadoId, string EmpleadoNombre, int EmpleadoLegajo, decimal Total, string TextoBuscar)
        {
            this.IdFactura = IdFactura;
            this.FechaFactura = FechaFactura;
            this.PersonaId = PersonaId;
            this.ClienteNombre = ClienteNombre;
            this.EmpleadoId = EmpleadoId;
            this.EmpleadoNombre = EmpleadoNombre;
            this.EmpleadoLegajo = EmpleadoLegajo;
            this.Total = Total;
            this.TextoBuscar = TextoBuscar;
        }

          public string Insertar(DFactura Factura, List<DDetalle_Factura> Detalle)
        {
            string rta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                SqlTransaction SqlTra = SqlCon.BeginTransaction();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsertar_factura";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdFactura = new SqlParameter();
                ParIdFactura.ParameterName = "@idfactura";
                ParIdFactura.SqlDbType = SqlDbType.Int;
                ParIdFactura.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdFactura);

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@fecha";
                ParNombre.SqlDbType = SqlDbType.DateTime;
                ParNombre.Value = Factura.FechaFactura;
                SqlCmd.Parameters.Add(ParNombre);

                SqlParameter ParIdCliente = new SqlParameter();
                ParIdCliente.ParameterName = "@idcliente";
                ParIdCliente.SqlDbType = SqlDbType.Int;
                ParIdCliente.Value = Factura.PersonaId;
                SqlCmd.Parameters.Add(ParIdCliente);

                SqlParameter ParNombreCliente = new SqlParameter();
                ParNombreCliente.ParameterName = "@nombrecliente";
                ParNombreCliente.SqlDbType = SqlDbType.VarChar;
                ParNombreCliente.Size = 100;
                ParNombreCliente.Value = Factura.ClienteNombre;
                SqlCmd.Parameters.Add(ParNombreCliente);

                SqlParameter ParIdEmpleado = new SqlParameter();
                ParIdEmpleado.ParameterName = "@idempleado";
                ParIdEmpleado.SqlDbType = SqlDbType.Int;
                ParIdEmpleado.Value = Factura.EmpleadoId;
                SqlCmd.Parameters.Add(ParIdEmpleado);

                SqlParameter ParNombreEmpleado = new SqlParameter();
                ParNombreEmpleado.ParameterName = "@nombreempleado";
                ParNombreEmpleado.SqlDbType = SqlDbType.VarChar;
                ParNombreEmpleado.Size = 100;
                ParNombreEmpleado.Value = Factura.EmpleadoNombre;
                SqlCmd.Parameters.Add(ParNombreEmpleado);

                SqlParameter ParLegajo = new SqlParameter();
                ParLegajo.ParameterName = "@legajoempleado";
                ParLegajo.SqlDbType = SqlDbType.Int;
                ParLegajo.Value = Factura.EmpleadoLegajo;
                SqlCmd.Parameters.Add(ParLegajo);

                SqlParameter ParTotal = new SqlParameter();
                ParTotal.ParameterName = "@total";
                ParTotal.SqlDbType = SqlDbType.Money;
                ParTotal.Value = Factura.Total;
                SqlCmd.Parameters.Add(ParTotal);

                rta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se ingreso el registro";

                if (rta.Equals("OK"))
                {
                    //Obtengo el id de factura generado
                    this.IdFactura = Convert.ToInt32(SqlCmd.Parameters["@idfactura"].Value);
                    foreach (DDetalle_Factura det in Detalle)
                    {
                        det.IdFactura = this.IdFactura;
                        //Lamo al metodo insertar de la clase DDetalle_factura
                        rta = det.Insertar(det, ref SqlCon, ref SqlTra);
                        if (!rta.Equals("OK"))
                        {
                            break;
                        }
                    }
                }
                if (rta.Equals("OK"))
                {
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback();
                }
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
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("tblFactura");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_factura";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable BuscarNombreCliente(DFactura Factura)
        {
            DataTable DtResultado = new DataTable("tblFactura");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_factura_nombrescliente";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 100;
                ParTextoBuscar.Value = Factura.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable BusquedaAvanzada(DFactura Factura)
        {
            DataTable DtResultado = new DataTable("tblFactura");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_factura_nombreyfecha";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 100;
                ParTextoBuscar.Value = Factura.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlParameter ParFechaDesde = new SqlParameter();
                ParFechaDesde.ParameterName = "@fechabuscardesde";
                ParFechaDesde.SqlDbType = SqlDbType.VarChar;
                ParFechaDesde.Size = 50;
                ParFechaDesde.Value = Factura.FechaDesde;
                SqlCmd.Parameters.Add(ParFechaDesde);

                SqlParameter ParFechaHasta = new SqlParameter();
                ParFechaHasta.ParameterName = "@fechabuscarhasta";
                ParFechaHasta.SqlDbType = SqlDbType.VarChar;
                ParFechaHasta.Size = 50;
                ParFechaHasta.Value = Factura.FechaHasta;
                SqlCmd.Parameters.Add(ParFechaHasta);

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
