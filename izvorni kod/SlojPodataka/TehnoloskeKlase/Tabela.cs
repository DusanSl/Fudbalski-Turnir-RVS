using System.Data;
using Microsoft.Data.SqlClient;

namespace SlojPodataka.TehnoloskeKlase
{
    public abstract class Tabela
    {
        protected string _stringKonekcije = Konekcija.NizKonekcije;

        public DataTable IzvrsiUpit(string sql)
        {
            using (SqlConnection conn = new SqlConnection(_stringKonekcije))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}