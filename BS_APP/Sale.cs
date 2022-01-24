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
    public partial class Sale : Form
    {
        public Sale()
        {
            InitializeComponent();
            
         
        }

        private int ID_s;
        private string name_e;
        private string name_c;
        private string date_s;

        public int ID_S
        {
            get { return ID_s; }
        }
        public string Name_e
        {
            set { name_e = value; }
            get { return name_e; }
        }
        public string Name_c
        {
            set { name_c = value; }
            get { return name_c; }
        }
        public string Date_s
        {
            set { date_s = value; }
            get { return date_s; }
        }

        public Sale(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_s = Convert.ToInt32(val[0]);
                name_e = val[1];
                name_c = val[2];
                date_s = val[3];
            }

        }

        public List<Sale> listSale = new List<Sale>();
        public List<Orders> listOrders = new List<Orders>();

        ClassDataBase db = new ClassDataBase();

        public void LoadData()
        {
           
            string q = @"SELECT s.ID_sale, concat(st.sname_e,' ',st.name_e), c.name_c, s.date_s from sale s join staff st using(ID_e) join customers c using(ID_c)";
            db.Execute<Sale>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listSale);
        }
        public void LoadOrder(string id)
        {
            string q = @"SELECT b.title_b, o.number_o, o.price_o from sale join orders o using(ID_sale) join books b using(ID_b) where ID_sale='"+id+@"';";
            db.Execute<Orders>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listOrders);

        }
        public void ShowOrder()
        {
            for (int i = 0; i < listOrders.Count; i++) { dgvOrders.Rows.Add(listOrders[i].Book_name,listOrders[i].Number_books,listOrders[i].Price_one); }

        }
        public void ShowData()
        {
            
            for (int i = 0; i < listSale.Count; i++) { dgvSale.Rows.Add(listSale[i].ID_S, listSale[i].Name_e, listSale[i].Name_c,listSale[i].Date_s); }
            dgvSale.Sort(dgvSale.Columns[0], ListSortDirection.Ascending);
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd";
            LoadData();
            ShowData();
        }

        

        private void dgvSale_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSale.CurrentRow.Cells[0].Value != null)
            {
                listOrders.Clear();
                dgvOrders.Rows.Clear();
                LoadOrder(dgvSale.CurrentRow.Cells[0].Value.ToString());
                ShowOrder();
            }
        }

        private void dgvSale_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < dgvOrders.Rows.Count; i++)
            {
                dgvOrders.Rows.Remove(dgvOrders.Rows[0]);
            }
        }
     
        
        private void mc_DateSelected(object sender, DateRangeEventArgs e)
        {
            tbf.Text = mc.SelectionStart.ToString("yyyy-MM-dd");
            tbe.Text = mc.SelectionEnd.ToString("yyyy-MM-dd");
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {

            
        }
        
        public void LoadDataD()
        {

            string q = @"SELECT s.ID_sale, concat(st.sname_e,' ',st.name_e), c.name_c, s.date_s from sale s join staff st using(ID_e) join customers c using(ID_c)where date(date_s)='"+dtp.Text+@"'";
            db.Execute<Sale>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listSale);
        }
        public void LoadDataPeriod()
        {

            string q = @"call book_shop.period_sales('"+tbf.Text+@"','"+tbe.Text+@"')";
            db.Execute<Sale>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listSale);


        }
        

        private void btnC_Click(object sender, EventArgs e)
        {
            listSale.Clear();
            dgvSale.Rows.Clear();
            LoadDataD();
            ShowData();
            Order_sum(0);
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listSale.Clear();
            dgvSale.Rows.Clear();
            LoadData();
            ShowData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listSale.Clear();
            dgvSale.Rows.Clear();
            LoadDataPeriod();
            ShowData();
            Order_sum(1);
        }
        public void Order_sum(int comtype)
        {
            string con="server=localhost;port=3306; uid=root; pwd=prl; database = book_shop";
            string q="";
            if(comtype==1)
                q = @"select sum(orders.price_o*orders.number_o) from sale join orders using(ID_sale)  where sale.date_s between'" + tbf.Text + "' and'" + tbe.Text + "' ";
            if (comtype == 0)
                q = @"select sum(orders.price_o*orders.number_o) from sale join orders using(ID_sale)  where date(sale.date_s)='"+dtp.Text+@"'";

            using (MySqlConnection connection = new MySqlConnection(con)) 
            {
                MySqlCommand command = new MySqlCommand(q, connection);
                

                try
                {
                    connection.Open();
                    tbSum2.Text = command.ExecuteScalar().ToString() ;
                    tbSum.Text = command.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
            }
        }
    }
}
