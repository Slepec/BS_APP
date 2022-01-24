using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_APP
{
    public class Authors
    {
        private int ID_a;
        private string name_a;
        public int ID_A
        {
            get { return ID_a; }
        }
        public string Name_a
        {
            set { name_a = value; }
            get { return name_a; }
        }
        public Authors()
        {
            ID_a = -1;
            name_a = "";
        }
        public Authors(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_a = Convert.ToInt32(val[0]);
                name_a = val[1];
            }

        }
    }
}
