using System;
using System.Globalization;
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


    public partial class Books : Form
    {


        public Books()
        {

            InitializeComponent();
            
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
        public Books(string info)
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
                    price_b = Convert.ToDecimal(val[5].Replace(".",","));
                    pages_b = Convert.ToInt32(val[6]);
                    binding_b = val[7];
                    number_b = Convert.ToInt32(val[8]);
                }
            }

        }
        


        public List<Catalog> listCatalog = new List<Catalog>();
        public List<Authors> listAuthors = new List<Authors>();
        public List<Books> listBooks = new List<Books>();

        ClassDataBase db = new ClassDataBase();
        public void LoadData()
        {
            string q =@"select ID_cat, name_cat from catalog";
            db.Execute<Catalog>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listCatalog);
            q = @"SELECT ID_a, concat(name_a,' ', sname_a) FROM authors";
            db.Execute<Authors>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listAuthors);
            q = @"SELECT b.ID_b, b.title_b, concat(a.name_a,' ',a.sname_a), b.date_b, c.name_cat, b.price_b, b.pages, b.cover, b.number_b from books b join authors a using(ID_a) join catalog c using(ID_cat)";
            db.Execute<Books>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listBooks);

        }

        public void ShowData()
        {
            for(int i = 0; i < listCatalog.Count; i++) { cbCatalog.Items.Add(listCatalog[i].Name_cat); }
            cbCatalog.Text = listCatalog[0].Name_cat;
            for (int i = 0; i < listCatalog.Count; i++) { cbCatalogF.Items.Add(listCatalog[i].Name_cat); }
            cbCatalogF.Text = listCatalog[0].Name_cat;
            for (int i = 0; i < listAuthors.Count; i++) { cbAuthors.Items.Add(listAuthors[i].Name_a); }
            cbAuthors.Text = listAuthors[0].Name_a;
            for (int i = 0; i < listAuthors.Count; i++) { cbAuthorsF.Items.Add(listAuthors[i].Name_a); }
            cbAuthorsF.Text = listAuthors[0].Name_a;
            if (cbNA.Checked)
            {
                for (int i = 0; i < listBooks.Count; i++)
                {
                    dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b);
                }
            }
            else
            {
                for (int i = 0; i < listBooks.Count; i++)
                {
                    if (Convert.ToInt32(dgvBooks[7, i].Value) > 0)
                    {
                        dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b);
                    }
                }
            }

            
            
        }

        public bool Check_Books()
        {
            try
            {
                
                if (tbName.Text.Trim() == "" )
                {
                    MessageBox.Show(@"Поле Назва є обов'язковим " , "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (tbNumber.Text.Trim() == "" || Convert.ToInt32(tbNumber.Text) < 1 )
                {
                    MessageBox.Show(@"Поле Кількість є обов'язковим і його значення має бути більше 0", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (tbBinding.Text.Trim() != "" && tbBinding.Text.Trim() !="М'яка" && tbBinding.Text.Trim() !="Тверда")
                {
                    MessageBox.Show(@"Неправильна обкладинка ", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private int get_ID_a(string au)
        {
            for(int i=0;i<listAuthors.Count;i++)
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

        public void SaveToDb()
        {
            if(Check_Books())
            {
                Books bks = new Books();
                
                bks.Name_b = tbName.Text.Trim();
                bks.Author_b = cbAuthors.Text.Trim();
                bks.Year_b = Convert.ToInt32(tbYear.Text);
                bks.Catalog_b = cbCatalog.Text;
                bks.Price_b = Convert.ToDecimal(tbPrice.Text);
                bks.Pages_b = Convert.ToInt32(tbPages.Text);
                bks.Binding_b = tbBinding.Text.Trim();
                bks.Number_b = Convert.ToInt32(tbNumber.Text);

                

                string q = @"insert into books (title_b, ID_a, date_b, ID_cat, price_b, pages, cover, number_b) values('" + bks.Name_b.Replace("'", "''") + @"' , " + Convert.ToString(get_ID_a(cbAuthors.Text)).Replace("'", "''") + @"," + Convert.ToString(bks.Year_b) + @"," + Convert.ToString(get_ID_cat(cbCatalog.Text)).Replace("'", "''") + @"," + Convert.ToString(bks.Price_b).Replace(",",".") + "," + Convert.ToString(bks.Pages_b) + @",'" + bks.Binding_b.Replace("'", "''") + @"'," + Convert.ToString(bks.Number_b) + @")";
                db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                tbName.Clear();
                tbYear.Clear();
                tbPrice.Clear();
                tbPages.Clear();
                tbBinding.Clear();
                tbNumber.Clear();
                success.Visible=true;
                listBooks.Clear();
                LoadData();
                
                dgvBooks.Rows.Add(bks.Name_b, bks.Author_b, bks.Year_b, bks.catalog_b, bks.Price_b, bks.Pages_b, bks.Binding_b, bks.Number_b);
                
            }
        }

        private void Books_Load(object sender, EventArgs e)
        {
           
            cbNA.Checked = true;
            LoadData();
            ShowData();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveToDb();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddAuthor aa = new AddAuthor();
            aa.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddCatalog ac = new AddCatalog();
            ac.ShowDialog();
        }

        public void AuthorsF()
        {
            for (int i = 0; i < dgvBooks.Rows.Count - 1; i++)
                if (dgvBooks[1, i].Value.ToString() != cbAuthorsF.Text)
                {
                    dgvBooks.Rows.RemoveAt(i);
                    i--;
                }

        }
        public void CatalogF()
        {
            for (int i = 0; i < dgvBooks.Rows.Count - 1; i++)
                if (dgvBooks[3, i].Value.ToString() != cbCatalogF.Text)
                {
                    dgvBooks.Rows.RemoveAt(i);
                    i--;
                }

        }
        public void YearF()
        {
            for (int i = 0; i < dgvBooks.Rows.Count - 1; i++)
                if (Convert.ToInt32(dgvBooks[2, i].Value) < Convert.ToInt32(tbMinYear.Text) || Convert.ToInt32(dgvBooks[2, i].Value) > Convert.ToInt32(tbMaxYear.Text))
                {
                    dgvBooks.Rows.RemoveAt(i);
                    i--;
                }

        }
        public void PriceF()
        {
            for (int i = 0; i < dgvBooks.Rows.Count - 1; i++)
                if (Convert.ToDecimal(dgvBooks[4, i].Value) < Convert.ToDecimal(tbMinPrice.Text) || Convert.ToDecimal(dgvBooks[4, i].Value) > Convert.ToDecimal(tbMaxPrice.Text))
                {
                    dgvBooks.Rows.RemoveAt(i);
                    i--;
                }

        }
       
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                AuthorsF();
                
            }
            else 
            {
                dgvBooks.Rows.Clear();
                for (int i = 0; i < listBooks.Count; i++) { dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b); }
                if (checkBox4.Checked == true)
                {
                    CatalogF();
                }
                if (checkBox2.Checked == true)
                {
                    YearF();
                }
                if (checkBox1.Checked == true)
                {
                    PriceF();
                }
                if(!cbNA.Checked)
                {
                    not_ava_del();
                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                CatalogF();
            }
            else
            {
                dgvBooks.Rows.Clear();
                for (int i = 0; i < listBooks.Count; i++) { dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b); }
                if (checkBox3.Checked == true)
                {
                    AuthorsF();
                }
                if (checkBox2.Checked == true)
                {
                    YearF();
                }
                if (checkBox1.Checked == true)
                {
                    PriceF();
                }
                if (!cbNA.Checked)
                {
                    not_ava_del();
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                if (CheckYearF())
                    YearF();
            }
            else
            {
                dgvBooks.Rows.Clear();
                for (int i = 0; i < listBooks.Count; i++) { dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b); }
                if (checkBox4.Checked == true)
                {
                    CatalogF();
                }
                if (checkBox3.Checked == true)
                {
                    AuthorsF();
                }
                if (checkBox1.Checked == true)
                {
                    PriceF();
                }
                if (!cbNA.Checked)
                {
                    not_ava_del();
                }
            }
        }
        public bool CheckYearF()
        {
            try
            {
                if (tbMinYear.Text.Trim() == "" || Convert.ToInt32(tbMinYear.Text) < 0 || Convert.ToInt32(tbMinYear.Text) > DateTime.Now.Year || tbMaxYear.Text.Trim() == "" || Convert.ToInt32(tbMaxYear.Text) < 0 || Convert.ToInt32(tbMaxYear.Text) > DateTime.Now.Year || Convert.ToInt32(tbMinYear.Text) > Convert.ToInt32(tbMaxYear.Text))
                {
                    MessageBox.Show(@"Невірно введений рік", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox2.Checked = false;
                    return false;
                }
            }
            catch
            {
                MessageBox.Show(@"Невірно введений рік", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox2.Checked = false;
                return false;
            }
            return true;
        }
        public bool CheckPriceF()
        {
            try
            {
                if (tbMinPrice.Text.Trim() == "" || Convert.ToDecimal(tbMinPrice.Text) < 0 || tbMaxPrice.Text.Trim() == "" || Convert.ToDecimal(tbMaxPrice.Text) < 0 || Convert.ToDecimal(tbMinPrice.Text) > Convert.ToDecimal(tbMaxPrice.Text))
                {
                    MessageBox.Show(@"Невірно введена ціна", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox1.Checked = false;
                    return false;
                }
            }
            catch
            {
                MessageBox.Show(@"Невірно введена ціна", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox1.Checked = false;
                return false;
            }
            return true;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if(CheckPriceF())
                    PriceF();
            }
            else
            {
                dgvBooks.Rows.Clear();
                for (int i = 0; i < listBooks.Count; i++) { dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b); }
                if (checkBox4.Checked == true)
                {
                    CatalogF();
                }
                if (checkBox2.Checked == true)
                {
                    YearF();
                }
                if (checkBox3.Checked == true)
                {
                    AuthorsF();
                }
                if (!cbNA.Checked)
                {
                    not_ava_del();
                }
            }
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            success.Visible = false;
        }

      

        private void editCM_Click(object sender, EventArgs e)
        {
            EditForm ed = new EditForm();
            ed.Name_b = dgvBooks.CurrentRow.Cells[0].Value.ToString();
            ed.Author_b = dgvBooks.CurrentRow.Cells[1].Value.ToString();
            ed.Year_b = Convert.ToInt32(dgvBooks.CurrentRow.Cells[2].Value);
            ed.Catalog_b = dgvBooks.CurrentRow.Cells[3].Value.ToString();
            ed.Price_b = Convert.ToDecimal(dgvBooks.CurrentRow.Cells[4].Value);
            ed.Pages_b = Convert.ToInt32(dgvBooks.CurrentRow.Cells[5].Value);
            ed.Binding_b = dgvBooks.CurrentRow.Cells[6].Value.ToString();
            ed.Number_b = Convert.ToInt32(dgvBooks.CurrentRow.Cells[7].Value.ToString());
            ed.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void not_ava_del()
        {
            for(int i=0;i<dgvBooks.Rows.Count-1;i++)
            {
                if (Convert.ToInt32(dgvBooks[7, i].Value)<1)
                {
                    dgvBooks.Rows.RemoveAt(i);
                    i--;
                }
            }
        }
            
        private void cbNA_CheckedChanged(object sender, EventArgs e)
        {
            if(cbNA.Checked==true)
            {
                dgvBooks.Rows.Clear();
                for (int i = 0; i < listBooks.Count; i++) { dgvBooks.Rows.Add(listBooks[i].Name_b, listBooks[i].Author_b, listBooks[i].Year_b, listBooks[i].Catalog_b, listBooks[i].Price_b, listBooks[i].Pages_b, listBooks[i].Binding_b, listBooks[i].Number_b); }
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
            else
            {
                not_ava_del();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (addPanel.Visible == false)
            {
                addPanel.Visible = true;
                button1.BackColor = Color.Green;
                dgvBooks.Height = 283;
                dgvBooks.Location = new Point(32, 206);
            }
            else 
            {
                addPanel.Visible = false; 
                button1.BackColor = Color.Red;
                dgvBooks.Height = 479;
                dgvBooks.Location = new Point(32, 10);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBooks.Clear();
            listAuthors.Clear();
            listCatalog.Clear();
            tbBinding.Clear();
            tbMaxPrice.Clear();
            tbMinPrice.Clear();
            tbMinYear.Clear();
            tbMaxYear.Clear();
            tbName.Clear();
            tbNumber.Clear();
            tbPages.Clear();
            tbYear.Clear();
            tbPrice.Clear();
            cbAuthors.Items.Clear();
            cbAuthorsF.Items.Clear();
            cbCatalog.Items.Clear();
            cbCatalogF.Items.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            cbNA.Checked = true;
            dgvBooks.Rows.Clear();
            LoadData();
            ShowData();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
