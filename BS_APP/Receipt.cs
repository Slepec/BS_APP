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
    public partial class Receipt : Form
    {
        public Receipt()
        {
            InitializeComponent();
            

        }

        public List<Books> listBooks = new List<Books>();
        public List<Staff> listStaff = new List<Staff>();
        public List<Customers> listCustomers = new List<Customers>();
        public List<Sale> listSale = new List<Sale>();
        public List<Orders> listOrders = new List<Orders>();
        public List<ReceiptInfo> listRI = new List<ReceiptInfo>();
        ClassDataBase db = new ClassDataBase();
        public void LoadData()
        {
            string q = @"SELECT b.ID_b, b.title_b, concat(a.name_a,' ',a.sname_a), b.date_b, c.name_cat, b.price_b, b.pages, b.cover, b.number_b from books b join authors a using(ID_a) join catalog c using(ID_cat);";
            db.Execute<Books>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listBooks);
            q = @"select ID_c, name_c from customers";
            db.Execute<Customers>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listCustomers);
            q = @"select ID_e, concat(sname_e,' ',name_e) from staff";
            db.Execute<Staff>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listStaff);
        }

        public void ShowData()
        {
            tbSumPrice.Text = "0";
            for (int i = 0; i < listBooks.Count; i++) { cbBooks.Items.Add(listBooks[i].Name_b); }
            cbBooks.Text = listBooks[0].Name_b;
            for (int i = 0; i < listStaff.Count; i++) { cbStaff.Items.Add(listStaff[i].Name_e); }
            cbStaff.Text = listStaff[0].Name_e;
            for (int i = 0; i < listCustomers.Count; i++)
            {
                if (listCustomers[i].Name_c != "")
                {
                    cbCust.Items.Add(listCustomers[i].Name_c);
                }
            }
            cbCust.Items.Add("");
        }
        private void Receipt_Load(object sender, EventArgs e)
        {
            
            LoadData();
            ShowData();
        }
        public int getNum(string Btitle)
        {
            for(int i = 0; i < listBooks.Count; i++) 
            {
                if(listBooks[i].Name_b==Btitle)
                {
                    return listBooks[i].Number_b;
                }
            }
            return 0;
        }
        public decimal getPrice(string Btitle)
        {
            for (int i = 0; i < listBooks.Count; i++)
            {
                if (listBooks[i].Name_b == Btitle)
                {
                    return listBooks[i].Price_b;
                }
            }
            return 0;
        }
        public bool checkBooks()
        {
            try
                {
                if (Convert.ToInt32(tbNum.Text) < 1)
                {
                    MessageBox.Show(@"Кількість має бути більшою за 1", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (Convert.ToInt32(tbNum.Text) > getNum(cbBooks.Text))
                {
                    MessageBox.Show(@"Книг в наявності: " + getNum(cbBooks.Text), "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                for (int i = 0; i < dgvR.Rows.Count; i++)
                {
                    if (Convert.ToString(dgvR[0, i].Value) == cbBooks.Text)
                    {
                        MessageBox.Show(@"Така книга вже є", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch 
            {
                MessageBox.Show(@"Невірно введені дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(checkBooks())
            {
                dgvR.Rows.Add(cbBooks.Text, tbNum.Text,getPrice(cbBooks.Text));
                tbSumPrice.Text = (Convert.ToDecimal(tbSumPrice.Text)+Convert.ToInt32(tbNum.Text) * getPrice(cbBooks.Text)).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvR.Rows.Count <= 2) { dgvR.Rows.Clear();tbSumPrice.Text = "0"; }
            else 
            { 
                tbSumPrice.Text=(Convert.ToDecimal(tbSumPrice.Text)- Convert.ToDecimal(dgvR[2, dgvR.Rows.Count - 2].Value)* Convert.ToInt32(dgvR[1, dgvR.Rows.Count - 2].Value)).ToString();
                dgvR.Rows.RemoveAt(dgvR.Rows.Count-2); 
            }
        }
        
        public int get_ID_e(string st)
        {
            for (int i = 0; i < listStaff.Count; i++)
            {
                if (listStaff[i].Name_e == st)
                {
                    return listStaff[i].ID_E;
                }
            }
            MessageBox.Show("ID працівника не знайдено");
            return -1;
        }
        public int get_ID_c(string st)
        {
            for (int i = 0; i < listCustomers.Count; i++)
            {

                if (listCustomers[i].Name_c == st)
                {
                    return listCustomers[i].ID_C;
                }
            }
            MessageBox.Show("ID клієнта не знайдено");
            return -1;
        }
        public int get_ID_b(string st)
        {
            for (int i = 0; i < listBooks.Count; i++)
            {
                if (listBooks[i].Name_b == st)
                {
                    return listBooks[i].ID_B;
                }
            }
            MessageBox.Show("ID книги не знайдено");
            return -1;
        }
        public bool check()
        {
            if(dgvR.Rows.Count<2)
            {
                MessageBox.Show(@"Немає покупки", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                if (Convert.ToDecimal(tbPaid.Text) < Convert.ToDecimal(tbSumPrice.Text)) { MessageBox.Show(@"Клієнт недостатньо заплатив", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; };
            }
            catch { MessageBox.Show(@"Клієнт недостатньо заплатив", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
            return true;
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            
            if (check())
            {
                listRI.Clear();
                Sale sl = new Sale();
                sl.Name_e = cbStaff.Text.Trim();
                sl.Name_c = cbCust.Text.Trim();
                sl.Date_s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string q = "";
                if (cbCust.Text == "")
                {
                    q = @"insert into sale (ID_e, ID_c, date_s) values('" + Convert.ToString(get_ID_e(cbStaff.Text)) + "','" + Convert.ToString(newCust()) + "','" + sl.Date_s + "')";
                }
                else
                {
                    q = @"insert into sale (ID_e, ID_c, date_s) values('" + Convert.ToString(get_ID_e(cbStaff.Text)) + "','" + Convert.ToString(get_ID_c(cbCust.Text)) + "','" + sl.Date_s + "')";
                }

                db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                q = @"select ID_sale from sale order by 1 desc limit 1;";
                string new_sid = "";

                using (MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; uid = root; pwd = prl; database = book_shop"))
                {
                    MySqlCommand command = new MySqlCommand(q, connection);


                    try
                    {
                        connection.Open();
                        new_sid = command.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    connection.Close();
                }
                Orders ord = new Orders();
                for (int i = 0; i < dgvR.Rows.Count - 1; i++)
                {
                    ord.Book_name = dgvR[0, i].Value.ToString();
                    ord.Number_books = Convert.ToInt32(dgvR[1, i].Value);
                    ord.Price_one = Convert.ToDecimal(dgvR[2, i].Value);
                    q = @"insert into orders (ID_sale, ID_b, number_o, price_o) values('" + new_sid + "','" + Convert.ToString(get_ID_b(ord.Book_name)) + "','" + Convert.ToString(ord.Number_books) + "','" + Convert.ToString(ord.Price_one).Replace(",", ".") + "')";
                    db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                    q = @"UPDATE books SET number_b = '"+ Convert.ToString(getNum(ord.Book_name)-ord.Number_books)+"' WHERE ID_b = '"+Convert.ToString(get_ID_b(ord.Book_name))+"'";
                    db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                    listRI.Add(new ReceiptInfo() { SaleID = Convert.ToInt32(new_sid), SellerID = get_ID_e(sl.Name_e), SellerName = sl.Name_e, BookTitle = ord.Book_name, Num = ord.Number_books, PriceOne = ord.Price_one.ToString().Replace(".", ","),Discount = tbDiscount.Text, Paid = tbPaid.Text.Replace(".",","), DateSale = sl.Date_s });
                    
                    for (int j =0;j<listBooks.Count;j++)
                    {
                        if(listBooks[j].Name_b==ord.Book_name)
                        {
                            listBooks[j].Number_b -= ord.Number_books;
                        }
                    }
                    
                }
                btnPrint.Visible = true;
                cbCust.Text = "";
                tbNum.Clear();
                tbPaid.Clear();
                dgvR.Rows.Clear();
                btnEnter.BackColor = Color.ForestGreen;
                tbSumPrice.Text = "0";
                
            }
            else btnEnter.BackColor = Color.Red ;
        }
        public int newCust()
        {
            Customers cst = new Customers();
            listCustomers.Add(cst);

            string q = @"insert into customers (name_c, email_c, phone_c) values('','','')";
            db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
            q = @"select ID_c from customers order by 1 desc limit 1;";
            using (MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; uid = root; pwd = prl; database = book_shop"))
            {
                MySqlCommand command = new MySqlCommand(q, connection);


                try
                {
                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
            }
            
            LoadData();
            return -1;
        }

        private void btnEnter_MouseLeave(object sender, EventArgs e)
        {
            btnEnter.BackColor = Color.LightGray;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Customers cs = new Customers();
            cs.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ClassSerialiaze.SerialiazeToXml<List<ReceiptInfo>>(ref listRI, "data.xml");

            FReport frmReport = new FReport();
            frmReport.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cbBooks.Items.Clear();
            cbCust.Items.Clear();
            cbStaff.Items.Clear();
            dgvR.Rows.Clear();
            listBooks.Clear();
            listCustomers.Clear();
            listOrders.Clear();
            listSale.Clear();
            listStaff.Clear();
            tbNum.Text = "";
            LoadData();
            ShowData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void dgvR_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbRest_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPaid_TextChanged(object sender, EventArgs e)
        {
            if (dgvR.Rows.Count > 1)
            {
                if (tbPaid.Text == "") { tbRest.Clear(); }
                else
                {
                    try { tbRest.Text = Convert.ToString(Convert.ToDecimal(tbPaid.Text) - Convert.ToDecimal(tbSumPrice.Text)); }
                    catch { tbRest.Clear(); }
                }
            }
            else {  MessageBox.Show("Спочатку додадіть книги в замовлення."); }
        }

        private void tbPaid_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar!=8 && e.KeyChar!=44) { e.Handled = true; }
        }

        private bool discCheck(int id)
        {
            string con = "server=localhost;port=3306; uid=root; pwd=prl; database = book_shop";
            string q = @"select count(ID_sale) from sale where ID_c="+id;
            using (MySqlConnection connection = new MySqlConnection(con))
            {
                MySqlCommand command = new MySqlCommand(q, connection);


                try
                {
                    connection.Open();
                    if (Convert.ToInt32(command.ExecuteScalar()) > 2) { return true; }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
            }
            return false;
        }
        private void tbSumPrice_TextChanged(object sender, EventArgs e)
        {
            if(discCheck(get_ID_c(cbCust.Text)))
            {
                tbDiscount.Text = (Math.Round(Convert.ToDouble(tbSumPrice.Text) - Convert.ToDouble(tbSumPrice.Text) * 0.05, 2, MidpointRounding.ToEven)).ToString();
            }
            else { tbDiscount.Text = tbSumPrice.Text; }
        }

        private void cbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (discCheck(get_ID_c(cbCust.Text)))
            {
                tbDiscount.Text = (Math.Round(Convert.ToDouble(tbSumPrice.Text) - Convert.ToDouble(tbSumPrice.Text) * 0.05, 2, MidpointRounding.ToEven)).ToString();
            }
            else { tbDiscount.Text = tbSumPrice.Text; }
        }
    }
}
