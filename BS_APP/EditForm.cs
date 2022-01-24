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
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
            
        }
        public List<Catalog> listCatalog = new List<Catalog>();
        public List<Authors> listAuthors = new List<Authors>();
        public List<EditForm> listEdit = new List<EditForm>();
        ClassDataBase db = new ClassDataBase();
        private void EditForm_Load(object sender, EventArgs e)
        {
            
            LoadData();
            ShowData();
        }
        private int ID_b;
        private string name_b;
        private string author_b;
        private int year_b;
        private string catalog_b;
        private decimal price_b;
        private int pages_b;
        private string binding_b;
        private int number_b;

        public int ID_B
        {
            get { return ID_b; }
        }
        public string Name_b
        {
            set { name_b = value; }
            get { return name_b; }
        }
        public string Author_b
        {
            set { author_b = value; }
            get { return author_b; }
        }
        public int Year_b
        {
            set { year_b = value; }
            get { return year_b; }
        }
        public string Catalog_b
        {
            set { catalog_b = value; }
            get { return catalog_b; }
        }
        public decimal Price_b
        {
            set { price_b = value; }
            get { return price_b; }
        }
        public int Pages_b
        {
            set { pages_b = value; }
            get { return pages_b; }
        }
        public string Binding_b
        {
            set { binding_b = value; }
            get { return binding_b; }
        }
        public int Number_b
        {
            set { number_b = value; }
            get { return number_b; }
        }
        public EditForm(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_b = Convert.ToInt32(val[0]);
                name_b = val[1];
                if (val.Length > 2)
                {
                    ID_b = Convert.ToInt32(val[0]);
                    name_b = val[1];
                    author_b = val[2];
                    year_b = Convert.ToInt32(val[3]);
                    catalog_b = val[4];
                    price_b = Convert.ToDecimal(val[5].Replace(".", ","));
                    pages_b = Convert.ToInt32(val[6]);
                    binding_b = val[7];
                    number_b = Convert.ToInt32(val[8]);
                }
            }

        }
        public void LoadData()
        {
            string q = @"select ID_cat, name_cat from catalog";
            db.Execute<Catalog>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listCatalog);
            q = @"SELECT ID_a, concat(name_a,' ', sname_a) FROM authors";
            db.Execute<Authors>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listAuthors);
        }

        public void ShowData()
        {
            
            for (int i = 0; i < listCatalog.Count; i++) { cbCatalog.Items.Add(listCatalog[i].Name_cat); }
            for (int i = 0; i < listAuthors.Count; i++) { cbAuthors.Items.Add(listAuthors[i].Name_a); }
            tbName.Text = Name_b;
            cbAuthors.Text = Author_b;
            tbYear.Text = Year_b.ToString();
            cbCatalog.Text = Catalog_b;
            tbPrice.Text = Price_b.ToString();
            tbPages.Text = Pages_b.ToString();
            tbBinding.Text = Binding_b;
            tbNumber.Text = Number_b.ToString();

        }
        private int get_ID_a(string au)
        {
            for (int i = 0; i < listAuthors.Count; i++)
            {
                if (listAuthors[i].Name_a == au) return listAuthors[i].ID_A;
            }
            return -1;
        }
        private int get_ID_cat(string ct)
        {
            for (int i = 0; i < listCatalog.Count; i++)
            {
                if (listCatalog[i].Name_cat == ct) return listCatalog[i].ID_Cat;
            }
            return -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Check_Books())
            {
                
                string q = @"UPDATE books SET title_b = '"+tbName.Text+"', ID_a = '"+get_ID_a(cbAuthors.Text)+"', date_b = '"+tbYear.Text+"', ID_cat = '"+get_ID_cat(cbCatalog.Text)+"', price_b = '"+tbPrice.Text.Replace(",", ".") + "', pages = '"+tbPages.Text+"', cover = '"+tbBinding.Text.Replace("'","''")+"', number_b = '"+tbNumber.Text+"' WHERE title_b='"+Name_b+"'";
                
                db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                button1.BackColor = Color.Green;
            }
            else { button1.BackColor = Color.Red; }
        }
        public bool Check_Books()
        {
            try
            {
                if (tbName.Text.Trim() == "")
                {
                    MessageBox.Show(@"Поле Назва є обов'язковим ", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (tbYear.Text.Trim() == "" || Convert.ToInt32(tbYear.Text) < 0 || Convert.ToInt32(tbYear.Text) > DateTime.Now.Year)
                {
                    MessageBox.Show(@"Поле Рік Публікації є обов'язковим і його значення має бути в діапазоні від 0 до " + DateTime.Now.Year, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (tbPrice.Text.Trim() == "" || Convert.ToDecimal(tbPrice.Text) < 0)
                {
                    MessageBox.Show(@"Поле Ціна є обов'язковим і його значення має бути додатнім", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (tbPages.Text.Trim() == "" || Convert.ToInt32(tbPages.Text) < 1)
                {
                    MessageBox.Show(@"Значення поля Сторінки має бути більше 0", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (tbNumber.Text.Trim() == "" || Convert.ToInt32(tbNumber.Text) < 1)
                {
                    MessageBox.Show(@"Поле Кількість є обов'язковим і його значення має бути більше 0", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show(@"Невірно введене значення ", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.DarkGray;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
