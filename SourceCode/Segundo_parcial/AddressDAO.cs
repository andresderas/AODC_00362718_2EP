using System;
using System.Collections.Generic;
using System.Data;

namespace Segundo_parcial
{
    public static class AddressDAO
    {
        public static List<Address> getLista(int idUser)
        {
            string sql = $"SELECT ad.idAddress, ad.address FROM ADDRESS ad WHERE idUser = {idUser}";

            DataTable dt = Conexion.realizarConsulta(sql);

            List<Address> lista = new List<Address>();
            foreach (DataRow fila in dt.Rows)
            {
                Address u = new Address();
                u.idAddress = Convert.ToInt32(fila[0].ToString());
                u.idUser = idUser;
                u.address = fila[1].ToString();

                lista.Add(u);
            }
            return lista;
        }
        public static void crearNuevo(int idUser, string address)
        {
            string sql = String.Format(
                "insert into address(idUser, address) " +
                "values({0}, '{1}');", idUser, address);

            Conexion.realizarAccion(sql);
        }
        public static void actualizar(string nueva, int idAddress)
        {
            string sql = String.Format(
                $"update address set address='{nueva}' where idAddress={idAddress}");

            Conexion.realizarAccion(sql);
        }
        public static void eliminar(int idAddress)
        {
            string sql = String.Format(
                "delete from address where idAddress ={0}; ", idAddress);

            Conexion.realizarAccion(sql);
        }

    }
}
