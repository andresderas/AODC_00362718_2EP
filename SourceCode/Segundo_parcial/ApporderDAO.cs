using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial
{
    public static class ApporderDAO
    {
        public static List<Apporder> getAll()
        {
            string sql = "SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                "FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                "WHERE ao.idProduct = pr.idProduct " +
                "AND ao.idAddress = ad.idAddress " +
                "AND ad.idUser = au.idUser";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Apporder> lista = new List<Apporder>();
            foreach (DataRow fila in dt.Rows)
            {
                Apporder u = new Apporder();
                u.idOrder = Convert.ToInt32(fila[0].ToString());
                u.createDate = Convert.ToDateTime(fila[1].ToString());
                u.name = fila[2].ToString();
                u.fullname = fila[3].ToString();
                u.address = fila[4].ToString();

                lista.Add(u);
            }
            return lista;
        }
        public static List<Apporder> getUser(int idUser)
        {
            string sql = "SELECT ao.idOrder, ao.createDate, pr.name, au.fullname, ad.address " +
                "FROM APPORDER ao, ADDRESS ad, PRODUCT pr, APPUSER au " +
                "WHERE ao.idProduct = pr.idProduct " +
                "AND ao.idAddress = ad.idAddress " +
                "AND ad.idUser = au.idUser " +
                $"AND au.idUser = {idUser}";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Apporder> lista = new List<Apporder>();
            foreach (DataRow fila in dt.Rows)
            {
                Apporder u = new Apporder();
                u.idOrder = Convert.ToInt32(fila[0].ToString());
                u.createDate = Convert.ToDateTime(fila[1].ToString());
                u.name = fila[2].ToString();
                u.fullname = fila[3].ToString();
                u.address = fila[4].ToString();

                lista.Add(u);
            }
            return lista;
        }
        public static void crearNuevo(string createDate, int idProduct, int idAddress)
        {
            string sql = String.Format(
                "insert into apporder(createDate, idProduct, idAddress) " +
                "values('{0}', {1}, {2});", createDate, idProduct, idAddress);

            Conexion.realizarAccion(sql);
        }
        public static void eliminar(int idOrder)
        {
            string sql = String.Format(
                "delete from apporder where idOrder ={0}; ", idOrder);

            Conexion.realizarAccion(sql);
        }
    }
}
