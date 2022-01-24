using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_APP
{
    public class Orders
    {
        private string book_name;
        private int number_books;
        private decimal price_one;

        public string Book_name
        {
            set { book_name = value; }
            get { return book_name; }
        }
        public int Number_books
        {
            set { number_books = value; }
            get { return number_books; }
        }
        public decimal Price_one
        {
            set { price_one = value; }
            get { return price_one; }
        }
        public Orders()
        {
            book_name = "";
            number_books = 0;
            price_one = 0;
        }
        public Orders(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                book_name = val[0];
                number_books = Convert.ToInt32(val[1]);
                price_one = Convert.ToDecimal(val[2].Replace(".",","));
            }

        }
    }
}
