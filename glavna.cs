using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ednevnik
{
    public partial class glavna : Form
    {
        public glavna()
        {
            InitializeComponent();
        }

        private void osobaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Osoba frm_osoba=new Osoba();    
            frm_osoba.Show();
        }

        private void smeroviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sifarnik frm_sifarnik = new sifarnik("smer");
            frm_sifarnik.Show();
        }

        private void skolskeGodineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sifarnik frm_sifarnik = new sifarnik("Skolska_godina");
            frm_sifarnik.Show();
        }

        private void predmetiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sifarnik frm_sifarnik = new sifarnik("predmet");
            frm_sifarnik.Show();
        }

        private void osobaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sifarnik frm_sifarnik = new sifarnik("osoba");
            frm_sifarnik.Show();
        }
    }
}
