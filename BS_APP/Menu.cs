using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BS_APP
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }


        private void bks_Click(object sender, EventArgs e)
        {
            
            Books bks = new Books();
            bks.Show();
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            Customers cst = new Customers();
            cst.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Staff stf = new Staff();
            stf.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            Sale sl = new Sale();
            sl.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Receipt rec = new Receipt();
            rec.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
