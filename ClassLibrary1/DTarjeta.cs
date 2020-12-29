using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DTarjeta
    {
        private int _IdTarjeta;
        private string _NombreTarjeta;
        private string _TextoBuscar;

        public int IdTarjeta
        {
            get { return _IdTarjeta;}
            set {_IdTarjeta = value;}
        }

        public string NombreTarjeta
        {
            get {return _NombreTarjeta;}
            set {_NombreTarjeta = value;}
        }

        public string TextoBuscar
        {
            get {return _TextoBuscar;}
            set {_TextoBuscar = value;}
        }

        //Constructor vacio
        public DTarjeta ()
        {
        }     

        //constructor con parametros
        public DTarjeta (int IdTarjeta, string NombreTarjeta, string TextoBuscar)
        {
            this.IdTarjeta = IdTarjeta;
            this.NombreTarjeta = NombreTarjeta;
            this.TextoBuscar = TextoBuscar;
        }

        // Metodo Insertar
        public string Insertar(DTarjeta Tarjeta)
        {
            string rta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinsertar_tarjeta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdTarjeta = new SqlParameter();
                ParIdTarjeta.ParameterName = "@id";
                ParIdTarjeta.SqlDbType = SqlDbType.Int;
                ParIdTarjeta.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdTarjeta);

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@nombre";
                ParNombre.SqlDbType = SqlDbType.VarChar;
                ParNombre.Size = 50;
                ParNombre.Value = Tarjeta.NombreTarjeta;
                SqlCmd.Parameters.Add(ParNombre);

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

        // Metodo Modificar
        public string Modificar(DTarjeta Tarjeta)
        {
            string rta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmodificar_tarjeta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdTarjeta = new SqlParameter();
                ParIdTarjeta.ParameterName = "@id";
                ParIdTarjeta.SqlDbType = SqlDbType.Int;
                ParIdTarjeta.Value = Tarjeta.IdTarjeta;
                SqlCmd.Parameters.Add(ParIdTarjeta);

                SqlParameter ParNombre = new SqlParameter();
                ParNombre.ParameterName = "@nombre";
                ParNombre.SqlDbType = SqlDbType.VarChar;
                ParNombre.Size = 50;
                ParNombre.Value = Tarjeta.NombreTarjeta;
                SqlCmd.Parameters.Add(ParNombre);

                rta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se actualizo el registro";
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

        //Metodo Eliminar
        public string Eliminar(DTarjeta Tarjeta)
        {
            string rta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCon.Open();
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speliminar_tarjeta";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdTarjeta = new SqlParameter();
                ParIdTarjeta.ParameterName = "@id";
                ParIdTarjeta.SqlDbType = SqlDbType.Int;
                ParIdTarjeta.Value = Tarjeta.IdTarjeta;
                SqlCmd.Parameters.Add(ParIdTarjeta);

                rta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se anulo el registro";
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
            DataTable DtResultado = new DataTable("tblTarjeta");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_tarjeta";
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

        // Metodo BuscarNombre
        public DataTable BuscarNombre (DTarjeta Tarjeta)
        {
            DataTable DtResultado = new DataTable("tblTarjeta");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = Conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_tarjeta_nombres";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Tarjeta.TextoBuscar;
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
    }
}
