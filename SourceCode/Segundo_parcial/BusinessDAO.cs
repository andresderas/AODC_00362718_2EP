using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial
{
    public static class BusinessDAO
    {
        public static List<Business> getLista()
        {
            string sql = "select * from business";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Business> lista = new List<Business>();
            foreach (DataRow fila in dt.Rows)
            {
                Business u = new Business();
                u.idBusiness = Convert.ToInt32(fila[0].ToString());
                u.name = fila[1].ToString();
                u.description = fila[2].ToString();

                lista.Add(u);
            }
            return lista;
        }
        public static void crearNuevo(string name, string description)
        {
            string sql = String.Format(
                "insert into business(name, description) " +
                "values('{0}', '{1}');", name, description);

            Conexion.realizarAccion(sql);
        }

        public static void eliminar(int idBusiness)
        {
            string sql = String.Format(
                "delete from business where idBusiness ={0}; ", idBusiness);

            Conexion.realizarAccion(sql);
        }

        public static List<Estadisticas> getEstadisticas()
        {
            string sql = "SELECT b.name AS \"Negocio\", sum(cp.cant) AS \"Total pedidos\" " +
                "FROM BUSINESS b, (SELECT p.idBusiness, p.name, count(ap.idProduct) AS \"cant\" FROM PRODUCT p, APPORDER ap " +
                "WHERE p.idProduct = ap.idProduct GROUP BY p.idProduct ORDER BY p.name ASC) AS cp WHERE b.idBusiness = cp.idBusiness " +
                "GROUP BY b.idBusiness;";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Estadisticas> lista = new List<Estadisticas>();
            foreach (DataRow fila in dt.Rows)
            {
                Estadisticas E = new Estadisticas();
                E.business = fila[0].ToString();
                E.cantidad = Convert.ToInt32(fila[1].ToString());

                lista.Add(E);
            }
            return lista;
        }
    }
}
