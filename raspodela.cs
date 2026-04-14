using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ednevnik
{
    public partial class raspodela : Form
    {
        DataTable Raspodela;
        int broj_sloga = 0;
        public raspodela()
        {
            InitializeComponent();
        }

        private void load_data()
        {
            SqlConnection veza = konekcija.connect();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from raspodela", veza);
            Raspodela = new DataTable();
            adapter.Fill(Raspodela);
        }

        private void raspodela_Load(object sender, EventArgs e)
        {

        }
        private void comboFill()
        {

        }
    }
}
