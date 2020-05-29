using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial
{
    public static class UsuarioDAO
    {
        public static List<Usuario> getLista()
        {
            string sql = "select * from appuser";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Usuario> lista = new List<Usuario>();
            foreach (DataRow fila in dt.Rows)
            {
                Usuario u = new Usuario();
                u.idUser = Convert.ToInt32(fila[0].ToString());
                u.fullname = fila[1].ToString();
                u.username = fila[2].ToString();
                u.password = fila[3].ToString();
                u.userType = Convert.ToBoolean(fila[4].ToString());

                lista.Add(u);
            }
            return lista;
        }

        public static void actualizarContra(string username, string nuevaContra)
        {
            string sql = String.Format(
                "update appuser set password='{0}' where username='{1}';",
                nuevaContra, username);

            Conexion.realizarAccion(sql);
        }

        public static void crearNuevo(string fullname, string username)
        {
            string sql = String.Format(
                "insert into appuser(fullname, username, password, userType) " +
                "values('{0}', '{1}', '{2}', false);", fullname, username, username);

            Conexion.realizarAccion(sql);
        }

        public static void eliminar(string username)
        {
            string sql = String.Format(
                "delete from appuser where username='{0}';", username);

            Conexion.realizarAccion(sql);
        }
    }
}
