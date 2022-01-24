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
    public partial class AddCatalog : Form
    {
        public AddCatalog()
        {
            InitializeComponent();
        }
        ClassDataBase db = new ClassDataBase();
        public bool Check_catalog()
        {
            if (tbCatalog.Text.Trim() == "")
            {
                MessageBox.Show(@"Поле Назва є обов'язковим", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void saveCatalog()
        {
            if (Check_catalog())
            {
                Catalog ctlg = new Catalog();
                ctlg.Name_cat = tbCatalog.Text.Trim().Replace("'", "''");
                string q = @"insert into catalog (name_cat) values('" +ctlg.Name_cat + @"')";
                tbCatalog.Clear();
                db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                Success.Visible = true;
            }
        }
        private void AddCatalog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveCatalog();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Success.Visible = true;
        }
    }
}
