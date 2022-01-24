using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_APP
{
    public class ReceiptInfo
    {
        private int sellerID;
        private int saleID;
        private string sellerName;
        private string bookTitle;
        private int num;
        private string priceOne;
        private string paid;
        private string dateSale;
        private string discount;
        
        public int SellerID
        {
            set { sellerID = value; }
            get { return sellerID; }
        }
        public int SaleID
        {
            set { saleID = value; }
            get { return saleID; }
        }
        public string SellerName
        {
            set { sellerName = value; }
            get { return sellerName; }
        }
        public string BookTitle
        {
            set { bookTitle = value; }
            get { return bookTitle; }
        }
        public int Num
        {
            set { num = value; }
            get { return num; }
        }
        public string PriceOne
        {
            set { priceOne = value; }
            get { return priceOne; }
        }
        public string Discount
        {
            set { discount = value; }
            get { return discount; }
        }
        public string Paid
        {
            set { paid = value; }
            get { return paid; }
        }
        public string DateSale
        {
            set { dateSale = value; }
            get { return dateSale; }
        }
        public ReceiptInfo()
        {
            saleID = -1;
            sellerID = -1;
            sellerName = "";
            bookTitle = "";
            num = 0;
            priceOne = "0,00";
            paid = "0,00";
            dateSale = "";
            discount = "0,00";
        }
        public ReceiptInfo(string info)
        {
            info = info.Trim();
            if (info.Length > 2)
            {
                string[] val = info.Split('!');
                saleID = Convert.ToInt32(val[0]);
                sellerID = Convert.ToInt32(val[1]);
                sellerName = val[2];
                bookTitle = val[3];
                num = Convert.ToInt32(val[4]);
                priceOne = val[5];
                paid = val[6];
                dateSale = val[7];
                discount = val[8];
                
            }

        }

    }
}
