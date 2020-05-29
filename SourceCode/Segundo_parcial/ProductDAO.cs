using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial
{
    public static class ProductDAO
    {
        public static List<Product> getLista(int idBusiness)
        {
            string sql = $"SELECT p.idProduct, p.name FROM PRODUCT p WHERE idBusiness = {idBusiness}";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Product> lista = new List<Product>();
            foreach (DataRow fila in dt.Rows)
            {
                Product u = new Product();
                u.idProduct = Convert.ToInt32(fila[0].ToString());
                u.idBusiness = idBusiness;
                u.name = fila[1].ToString();

                lista.Add(u);
            }
            return lista;
        }
        public static void crearNuevo(int idBusiness, string name)
        {
            string sql = String.Format(
                "insert into product(idBusiness, name) " +
                "values({0}, '{1}');", idBusiness, name);

            Conexion.realizarAccion(sql);
        }

        public static void eliminar(int idProduct)
        {
            string sql = String.Format(
                "delete from product where idProduct ={0}; ", idProduct);

            Conexion.realizarAccion(sql);
        }
    }
}
