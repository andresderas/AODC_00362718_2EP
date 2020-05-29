using Npgsql;
using System.Data;

namespace Segundo_parcial
{
    public static class Conexion
    {
        private static string CadenaConexion =
            "Server=127.0.0.1;Port=5432;User Id=postgres;Password=uca;Database=Segundo_parcial";

        public static DataTable realizarConsulta(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);
            DataSet ds = new DataSet();

            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();

            return ds.Tables[0];
        }

        public static void realizarAccion(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);

            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}
