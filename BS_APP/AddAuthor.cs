using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BS_APP
{
    public partial class AddAuthor : Form
    {
        public AddAuthor()
        {
            InitializeComponent();
        }

        private int ID_a;
        private string name_a;
        private string sname_a;
        public int ID_A
        {
            get { return ID_a; }
        }
        public string Name_a
        {
            set { name_a = value; }
            get { return name_a; }
        }
        public string Sname_a
        {
            set { sname_a = value; }
            get { return sname_a; }
        }
        public AddAuthor(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_a = Convert.ToInt32(val[0]);
                name_a = val[1];
                sname_a = val[2];
            }

        }
        ClassDataBase db = new ClassDataBase();
        public bool Check_author()
        {
            if(tbName_a.Text.Trim() =="")
            {
                MessageBox.Show(@"Поле Ім'я є обов'язковим", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void saveAuthor()
        {
            if(Check_author())
            {
                AddAuthor adda = new AddAuthor();
                adda.Name_a = tbName_a.Text.Trim().Replace("'", "''");
                adda.Sname_a = tbSname_a.Text.Trim().Replace("'", "''");
                string q = @"insert into authors (name_a, sname_a) values('"+adda.Name_a+@"','"+adda.Sname_a+@"')";
                tbName_a.Clear();
                tbSname_a.Clear();
                db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                Success.Visible = true;
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveAuthor();
        }

        private void AddAuthor_Load(object sender, EventArgs e)
        {

        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Success.Visible = false;
        }
    }
}
