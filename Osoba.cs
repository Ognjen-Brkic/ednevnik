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
    public partial class Osoba : Form
    {
        int broj_sloga = 0;
        DataTable tabela;
        public Osoba()
        {
            InitializeComponent();
        }
        private void Load_data()
        {
            SqlConnection veza = konekcija.connect();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from osoba", veza);
            tabela= new DataTable();
            adapter.Fill(tabela);
        }
        private void Load_txt()
        {
            if (tabela.Rows.Count == 0)
            {
                txt_id.Text = "";
                txt_ime.Text = "";
                txt_prezime.Text = "";
                txt_adresa.Text = "";
                txt_jmbg.Text = "";
                txt_email.Text = "";
                txt_pass.Text = "";
                txt_uloga.Text = "";
                btn_delete.Enabled = false;
            }
            else
            {
                txt_id.Text = tabela.Rows[broj_sloga]["id"].ToString();
                txt_ime.Text = tabela.Rows[broj_sloga]["ime"].ToString();
                txt_prezime.Text = tabela.Rows[broj_sloga]["prezime"].ToString();
                txt_adresa.Text = tabela.Rows[broj_sloga]["adresa"].ToString();
                txt_jmbg.Text = tabela.Rows[broj_sloga]["jmbg"].ToString();
                txt_email.Text = tabela.Rows[broj_sloga]["email"].ToString();
                txt_pass.Text = tabela.Rows[broj_sloga]["pass"].ToString();
                txt_uloga.Text = tabela.Rows[broj_sloga]["uloga"].ToString();
                btn_delete.Enabled = true;
            }
            if (broj_sloga == 0)
            {
                btn_first.Enabled = false;
                btn_prev.Enabled = false;
            }
            else
            {
                btn_first.Enabled = true;
                btn_prev.Enabled = true;
            }
            if (broj_sloga==tabela.Rows.Count-1)
            {
                btn_last.Enabled = false;
                btn_next.Enabled = false;
            }
            else
            {
                btn_last.Enabled = true;
                btn_next.Enabled = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Load_data();
            Load_txt();
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            broj_sloga = 0;
            Load_txt();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            broj_sloga--;
            Load_txt();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            broj_sloga++;
            Load_txt();
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            broj_sloga = tabela.Rows.Count - 1;
            Load_txt();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            string naredba = ("insert into osoba values ('" + txt_ime.Text + "', '" + txt_prezime.Text + "', '" + txt_adresa.Text + "', '" + txt_jmbg.Text + "', '" + txt_email.Text + "', '" + txt_pass.Text + "', '" + txt_uloga.Text + "')");
            SqlConnection veza = konekcija.connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();

            }
            catch(Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
            Load_data();
            broj_sloga = tabela.Rows.Count - 1;
            Load_txt();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string naredba = "Update osoba set ime = '" + txt_ime.Text + "', prezime = '" + txt_prezime.Text + "', adresa = '" + txt_adresa.Text + "', jmbg = '" + txt_jmbg.Text + "', email = '" + txt_email.Text + "', pass = '" + txt_pass.Text + "', uloga = '" + txt_uloga.Text + "' where id =" + txt_id.Text;
            SqlConnection veza = konekcija.connect();
            SqlCommand komanda = new SqlCommand(naredba.ToString(), veza);
            try
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();

            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
            Load_data();
         
            Load_txt();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string naredba = "delete from osoba where id =" + txt_id.Text;
            SqlConnection veza = konekcija.connect();
            SqlCommand komanda = new SqlCommand(naredba, veza);
            Boolean brisano = false;
            try
              
            {
                veza.Open();
                komanda.ExecuteNonQuery();
                veza.Close();
                brisano = true;
            }
            catch (Exception greska)
            {
                MessageBox.Show(greska.Message);
            }
            if (brisano)
            {
                Load_data();
                if (broj_sloga > 0) broj_sloga--;
                Load_txt();
            }
        }
    }
}
