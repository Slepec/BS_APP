using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_APP
{
    public class Catalog
    {
        private int ID_cat;
        private string name_cat;
        public int ID_Cat
        {
            get { return ID_cat; }
        }
        public string Name_cat
        {
            set { name_cat = value; }
            get { return name_cat; }
        }
        public Catalog()
        {
            ID_cat = -1;
            name_cat = "";
        }
        public Catalog(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_cat = Convert.ToInt32(val[0]);
                name_cat = val[1];
            }

        }
    }
}
