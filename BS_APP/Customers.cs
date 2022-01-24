using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace BS_APP
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            
        }
        ClassDataBase db = new ClassDataBase();

        private int ID_c;
        private string name_c;
        private string email_c;
        private string phone_c;
        public int ID_C
        {
            get { return ID_c; }
        }
        public string Name_c
        {
            set { name_c = value; }
            get { return name_c; }
        }
        public string Email_c
        {
            set { email_c = value; }
            get { return email_c; }
        }
        public string Phone_c
        {
            set { phone_c = value; }
            get { return phone_c; }
        }
        public Customers(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_c = Convert.ToInt32(val[0]);
                name_c = val[1];
                if (val.Length > 2)
                {
                    email_c = val[2];
                    phone_c = val[3];
                }
            }

        }
        public List<Customers> listCustomers = new List<Customers>();

        public void LoadData()
        {
            string q = @"SELECT ID_c, name_c, email_c,phone_c from customers";
            db.Execute<Customers>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listCustomers);
        }

        public void ShowData()
        {
            for (int i = 0; i < listCustomers.Count; i++) 
            {
                if (listCustomers[i].Name_c != "")
                {
                    dgvCustomers.Rows.Add(listCustomers[i].ID_C, listCustomers[i].Name_c, listCustomers[i].email_c, listCustomers[i].Phone_c);
                }
            }

        }

        private void Customers_Load(object sender, EventArgs e)
        {
            
            LoadData();
            ShowData();
        }

        public void SaveToDb()
        {
            if (checkCust())
            {
                Customers cst = new Customers();
                cst.Name_c = tbName.Text.Trim();
                    cst.Email_c = tbEmail.Text.Trim();

                    try
                    {
                        cst.Phone_c = tbPhone.Text.Substring(1, 3) + tbPhone.Text.Substring(6, 4) + tbPhone.Text.Substring(11, 3);
                        if (cst.Phone_c.IndexOf(" ") != -1) { throw new Exception(); }
                    }
                    catch (Exception) { cst.Phone_c = null; }
                    string q = @"insert into customers (name_c, email_c, phone_c) values('" + cst.Name_c.Replace("'", "''") + "','" + cst.Email_c.Replace("'", "''") + "','" + cst.Phone_c + "')";
                    db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                    tbName.Clear();
                    tbEmail.Clear();
                    tbPhone.Clear();
                    dgvCustomers.Rows.Clear();
                    listCustomers.Clear();
                    LoadData();
                    ShowData();
                    success.Visible = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveToDb();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            success.Visible = false;
        }

        private void editCM_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnExit.Visible = true;
            btnAdd.Visible = false;
            tbID.Visible = true;
            lblID.Visible = true;
            tbName.Text= dgvCustomers.CurrentRow.Cells[1].Value.ToString();
            tbEmail.Text = dgvCustomers.CurrentRow.Cells[2].Value.ToString();
            tbPhone.Text = dgvCustomers.CurrentRow.Cells[3].Value.ToString();
            tbID.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public bool checkCust()
        {
            if(tbEmail.Text.Trim()!="")
            {
                try
                {
                    MailAddress m1 = new MailAddress(tbEmail.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неіснуюча адреса електронної пошти.");
                    return false;
                }
            }
            if(tbPhone.Text!= "(   )     -")
            {


                try
                {
                    string phn = tbPhone.Text.Substring(1, 3) + tbPhone.Text.Substring(6, 4) + tbPhone.Text.Substring(11, 3);


                    string phnPatern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
                    Match m2 = Regex.Match(phn, phnPatern);
                    if (!m2.Success) { MessageBox.Show("Неправильний номер телефону."); return false; }
                }
                catch { MessageBox.Show("Неправильний номер телефону."); return false; }
            }
            if (tbName.Text.Trim() == "") { MessageBox.Show("Заповніть поле Ім'я"); return false; }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkCust())
            {
                Customers cst = new Customers();
                cst.Name_c = tbName.Text.Trim();
                cst.Email_c = tbEmail.Text.Trim();
                if (tbPhone.Text == "(   )     -") { cst.phone_c = ""; }
                else
                {
                    cst.Phone_c = tbPhone.Text.Substring(1, 3) + tbPhone.Text.Substring(6, 4) + tbPhone.Text.Substring(11, 3);
                }
                string q = @"UPDATE customers SET name_c = '" + tbName.Text.Replace("'", "''") + "', email_c = '" + tbEmail.Text.Replace("'", "''") + "', phone_c = '" + cst.Phone_c + "' WHERE ID_c = '" + tbID.Text + "';";
                db.ExecuteNonQuery("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, 0);
                btnExit_Click(this, e);
                dgvCustomers.Rows.Clear();
                listCustomers.Clear();
                LoadData();
                ShowData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnExit.Visible = false;
            btnAdd.Visible = true;
            tbID.Visible = false;
            lblID.Visible = false;
            tbName.Text ="";
            tbEmail.Text = "";
            tbPhone.Text = "";
            tbID.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }

}
