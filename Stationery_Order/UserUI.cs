using System;
using System.Collections.Generic;

namespace Stationery_Order
{
    public class UserUI
    {
       
        private StockManager StkMngr;
        private TransactionLog transLog;
        
        public UserUI(StockManager sm, TransactionLog tm)
        {
            this.StkMngr = sm;
            this.transLog = tm;
        }


        public Dictionary<string, Stock> AddToStock(string USERSNAME, int ITEMCODE, int ITEMQTY, string ITEMNAME, DateTime TRANS_DATE, double COST, string TRANS_TYPE)
        {

            Stock s = StkMngr.FindStock(ITEMCODE);

            transLog.UpdateTransactionLog(s, USERSNAME, TRANS_DATE, TRANS_TYPE, ITEMCODE, ITEMNAME, ITEMQTY, COST);
            return StkMngr.AddStock(DateTime.Now, ITEMNAME, ITEMQTY, COST);

        }

        public Dictionary<int, Stock> AddQtyToStock(string USERSNAME, int ITEMCODE, int ITEMQTY, string ITEMNAME, DateTime TRANS_DATE, double COST, string TRANS_TYPE)
        {
            Stock s = StkMngr.FindStock(ITEMCODE);
            transLog.UpdateTransactionLog(s, USERSNAME, TRANS_DATE, TRANS_TYPE, ITEMCODE, ITEMNAME, ITEMQTY,  COST);

            return StkMngr.AddQtyToStock(ITEMCODE, ITEMQTY, DateTime.Now, COST);
        }

        public Dictionary<int, Stock> TakeQtyFromStock(string USERSNAME, int ITEMCODE, int ITEMQTY, string ITEMNAME, DateTime TRANS_DATE, double COST, string TRANS_TYPE)
        {
            Stock s = StkMngr.FindStock(ITEMCODE);
            transLog.UpdateTransactionLog(s, USERSNAME, TRANS_DATE, TRANS_TYPE, ITEMCODE, ITEMNAME, ITEMQTY, COST);

            return StkMngr.TakeQtyFromStock(ITEMCODE, ITEMQTY);
        }
  
        public Dictionary<int, Stock> ViewFinancialReport()
        {
            return StkMngr.GetFinancialDetails();
        }
        public Dictionary<double, Stock> ViewTotalExpenditure()
        {
            return StkMngr.GetTotalExpenditure();
        }

        public Dictionary<int, Stock> ViewInventoryReport()
        {
            return StkMngr.GetAllStock();
        }
  
        public Dictionary<DateTime, TransactionLog> AddTrans()
        {
            return StkMngr.AddTransactionLogs();
        }
        public Dictionary<string, TransactionLog> RemTrans()
        {
            return StkMngr.RemoveTransactionLogs();
        }

        public Dictionary<int, TransactionLog> PersonalUsage(string USERSNAME)
        {

            return StkMngr.GetUserTransactions(USERSNAME);

        }
        public void ViewPersonalUsage(string USERSNAME)
        {
            if (USERSNAME != null)
            {
                
               StkMngr.GetUserTransactions(USERSNAME);
            }
        }

    }

}
