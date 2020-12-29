using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using System.Data;

namespace CapaNegocio
{
    public class NTarjeta
    {
        //Metodo para insertar 
        public static string Insertar(string nombre)
        {
            DTarjeta Obj = new DTarjeta();
            Obj.NombreTarjeta = nombre;
            return Obj.Insertar(Obj);
        }

        //Metodo para modificar
        public static string Modificar(int id, string nombre)
        {
            DTarjeta Obj = new DTarjeta();
            Obj.IdTarjeta = id;
            Obj.NombreTarjeta = nombre;
            return Obj.Modificar(Obj);
        }

        //Metodo para anular
        public static string Eliminar(int id)
        {
            DTarjeta Obj = new DTarjeta();
            Obj.IdTarjeta = id;
            return Obj.Eliminar(Obj);
        }

        //Metodo para mostrar 
        public static DataTable Mostrar()
        {
            return new DTarjeta().Mostrar();
        }

        //Metodo para buscar nombre
        public static DataTable BuscarNombre(string textobuscar)
        {
            DTarjeta Obj = new DTarjeta();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }
    }
}
