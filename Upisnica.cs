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
    public partial class Upisnica : Form
    {
        DataTable dt_upisnica;
        private void cmb_godina_populate()
        {
            SqlConnection veza = konekcija.connect();
            SqlDataAdapter adapter= new SqlDataAdapter("select * from skolska_godina",veza);
            DataTable dt_godina = new DataTable();
            adapter.Fill(dt_godina);
            cmb_godina.DataSource = dt_godina;
            cmb_godina.ValueMember = "id";
            cmb_godina.DisplayMember = "naziv";
            cmb_godina.SelectedValue = 2;

        }
        private void cmb_godina_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_godina.IsHandleCreated && cmb_godina.Focused)
            {
                cmb_odeljenje_populate();
                cmb_odeljenje.SelectedValue = -1;
          
                while (grid_Upisnica.Rows.Count > 0)
                {
                    grid_Upisnica.Rows.Remove(grid_Upisnica.Rows[0]);
                }
                cmb_ucenik.SelectedIndex = -1;
                cmb_ucenik.Enabled = false;
                txt_id.Text = "";
            }
        }
        private void cmb_odeljenje_populate()
        {
            string godina = cmb_godina.SelectedValue.ToString();
            SqlConnection veza = konekcija.connect();
            SqlDataAdapter adapter = new SqlDataAdapter("select id, str(razred) + '-' + indeks as naziv from odeljenje where godina_id="+godina, veza);
            DataTable dt_odeljenje= new DataTable();
            adapter.Fill(dt_odeljenje);
            cmb_odeljenje.DataSource = dt_odeljenje;
            cmb_odeljenje.ValueMember = "id";
            cmb_odeljenje.DisplayMember = "naziv";
           
        }
        private void cmb_odeljenje_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_odeljenje.IsHandleCreated && cmb_odeljenje.Focused)
            {
                cmb_ucenik_populate();
                cmb_ucenik.Enabled = true;
                grid_populate();
            }
        }
        private void grid_populate()
        {
            SqlConnection veza = konekcija.connect();
            SqlDataAdapter adapter = new SqlDataAdapter("select upisnica.id as id,ime + prezime as naziv, osoba.id as ucenik from upisnica join osoba on osoba_id=osoba.id where odeljenje_id=" + cmb_odeljenje.SelectedValue, veza);
            dt_upisnica= new DataTable();
            adapter.Fill(dt_upisnica);
            grid_Upisnica.DataSource = dt_upisnica;
            grid_Upisnica.AllowUserToAddRows = false;
            grid_Upisnica.Columns["ucenik"].Visible=false;
        }
        private void cmb_ucenik_populate()
        {
            SqlConnection veza = konekcija.connect();
            SqlDataAdapter adapter = new SqlDataAdapter("select id, ime + prezime as naziv from osoba where uloga=1", veza);
            DataTable dt_ucenik = new DataTable();
            adapter.Fill(dt_ucenik);
            cmb_ucenik.DataSource = dt_ucenik;
            cmb_ucenik.ValueMember= "id";
            cmb_ucenik.DisplayMember = "naziv";
        }
        public Upisnica()
        {
            InitializeComponent();
        }

        private void Upisnica_Load(object sender, EventArgs e)
        {
            cmb_godina_populate();
            cmb_odeljenje_populate();
            cmb_odeljenje.SelectedValue= -1;
            cmb_ucenik.Enabled = false;
            txt_id.Enabled = false;
        }

        private void grid_Upisnica_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grid_Upisnica.CurrentRow != null)
            {
                int broj_sloga = grid_Upisnica.CurrentRow.Index;
                if (dt_upisnica.Rows.Count != 0 && broj_sloga > -1)
                {
                    cmb_ucenik.SelectedValue = grid_Upisnica.Rows[broj_sloga].Cells["ucenik"].Value.ToString();
                    txt_id.Text = grid_Upisnica.Rows[broj_sloga].Cells["id"].Value.ToString();
                }
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("insert into upisnica (odeljenje_id, osoba_id) values ('");
            naredba.Append(cmb_odeljenje.SelectedValue.ToString() + "', '");
            naredba.Append(cmb_ucenik.SelectedValue.ToString() + "')");
            SqlConnection veza = konekcija.connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                grid_populate();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            StringBuilder naredba = new StringBuilder("update upisnica set");
            naredba.Append(" osoba_id='"+cmb_ucenik.SelectedValue.ToString()+"'");
            naredba.Append(" where id=" + txt_id.Text);
            SqlConnection veza = konekcija.connect();   
            SqlCommand komanda=new SqlCommand(naredba.ToString(),veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                grid_populate();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string naredba="delete from upisnica where id="+txt_id.Text;
            SqlConnection veza = konekcija.connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                grid_populate();
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
        }
    }
}
