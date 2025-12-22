using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace ednevnik
{
    internal class konekcija
    {
        static public SqlConnection connect()
        {
            string cs = ConfigurationManager.ConnectionStrings["kuca"].ConnectionString;
            SqlConnection veza= new SqlConnection(cs);
            return veza;
        }
    }
}
