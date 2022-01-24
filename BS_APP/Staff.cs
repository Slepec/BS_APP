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
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }

        private int ID_e;
        private string name_e;
        private string sname_e;
        private string mname_e;
        private string post_e;
        private string phone_e;
        private string work_e;
        public int ID_E
        {
            get { return ID_e; }
        }
        public string Name_e
        {
            set { name_e = value; }
            get { return name_e; }
        }
        public string Sname_e
        {
            set { sname_e = value; }
            get { return sname_e; }
        }
        public string Mname_e
        {
            set { mname_e = value; }
            get { return mname_e; }
        }
        public string Post_e
        {
            set { post_e = value; }
            get { return post_e; }
        }
        public string Phone_e
        {
            set { phone_e = value; }
            get { return phone_e; }
        }
        public string Work_e
        {
            set { work_e = value; }
            get { return work_e; }
        }

        public Staff(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_e = Convert.ToInt32(val[0]);
                name_e = val[1];
                if (val.Length > 2)
                {
                    sname_e = val[2];
                    mname_e = val[3];
                    post_e = val[4];
                    phone_e = val[5];
                    work_e = val[6];
                }
            }

        }


        public List<Post> listPost = new List<Post>();
        public List<Staff> listStaff = new List<Staff>();

        ClassDataBase db = new ClassDataBase();
        public void LoadData()
        {
            string q = @"SELECT s.ID_e,s.name_e, s.sname_e, s.mname_e, p.name_p, s.phone_e, s.work_e from staff s join post p using (ID_p)";
            db.Execute<Staff>("server=localhost;port=3306; uid=root; pwd=prl; database = book_shop", q, ref listStaff);
        }
       
        public void ShowData()
        {
            for (int i = 0; i < listStaff.Count; i++) { dgvStaff.Rows.Add( listStaff[i].Name_e, listStaff[i].Sname_e, listStaff[i].Mname_e, listStaff[i].Post_e,  listStaff[i].Phone_e, listStaff[i].Work_e); }

        }
        private void Staff_Load(object sender, EventArgs e)
        {
            LoadData();
            ShowData();
        }

        private void mcG_DateChanged(object sender, DateRangeEventArgs e)
        {
            
        }
    }
}
