using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_APP
{
    public class Post
    {
        private int ID_p;
        private string name_p;
        public int ID_P
        {
            get { return ID_p; }
        }
        public string Name_p
        {
            set { name_p = value; }
            get { return name_p; }
        }
        public Post()
        {
            ID_p = -1;
            name_p = "";
        }
        public Post(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                ID_p = Convert.ToInt32(val[0]);
                name_p = val[1];
            }

        }
    }
}
