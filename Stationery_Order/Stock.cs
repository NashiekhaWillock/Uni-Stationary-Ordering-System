using System;

namespace Stationery_Order
{
    public class Stock
    {
      
        public int ITEMCODE { get; set; }
        public DateTime TRANS_DATE { get; set; }
        public string ITEMNAME { get; set; }
        public int ITEMQTY { get; set; }
        public double COST{ get; set; }

        public Stock(int ITEMCODE, DateTime TRANS_DATE, string ITEMNAME, int ITEMQTY, double COST)
        {
            this.ITEMCODE = ITEMCODE;
            this.TRANS_DATE = TRANS_DATE;
            this.ITEMNAME= ITEMNAME;
            this.ITEMQTY = ITEMQTY;
            this.COST = COST;
        }
        public Stock(DateTime TRANS_DATE, string ITEMNAME, int ITEMQTY, double COST)
        {
            this.TRANS_DATE = TRANS_DATE;
            this.ITEMNAME = ITEMNAME;
            this.ITEMQTY = ITEMQTY;
            this.COST = COST;
        }
        public Stock(int ITEMCODE, string ITEMNAME, int ITEMQTY, double COST)
        {
            this.ITEMCODE = ITEMCODE;
            this.ITEMNAME = ITEMNAME;
            this.ITEMQTY = ITEMQTY;
            this.COST = COST;
        }
        public Stock(double COST)
        {
            this.COST = COST;
        }

    }
}