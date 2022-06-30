using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Stationery_Order
{
    public class TransactionLog
    {
        private DatabaseManager dbMgr;
        public TransactionLog(DatabaseManager dbMgr)
        {
            this.dbMgr = dbMgr;
        }
    
        public int TRANSID { get; set; }
        public string USERSNAME { get; set; }
        public DateTime TRANS_DATE { get; set; }
        public string TRANS_TYPE { get; set; }
        public int ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public int ITEMQTY { get; set; }
        public double COST { get; set; }

        public Dictionary<int, TransactionLog> UpdateTransactionLog(Stock S, string USERSNAME, DateTime TRANS_DATE, string TRANS_TYPE, int ITEMCODE, string ITEMNAME, int ITEMQTY, double COST)
        {
            Dictionary<int, TransactionLog> transaction = new Dictionary<int, TransactionLog>();


            OracleConnection conn = dbMgr.GetConnection();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO TransactionLog (TransactionID, Usersname, Trans_Date, Trans_Type, Itemcode, Itemname, ItemQty, Cost) " +
                                     "VALUES (TransLog_Seq.nextval, :Usersname, :Trans_Date, :Trans_Type, :Itemcode, :Itemname, :ItemQty, :Cost)",
                CommandType = CommandType.Text

            };

            try
            {

                cmd.Prepare();
                cmd.Parameters.Add(":Usersname", USERSNAME);
                cmd.Parameters.Add(":Trans_Date", TRANS_DATE);
                cmd.Parameters.Add(":Tran_Type", TRANS_TYPE);
                cmd.Parameters.Add(":Itemcode", ITEMCODE);
                cmd.Parameters.Add(":Itemname", ITEMNAME);
                cmd.Parameters.Add(":ItemQty", ITEMQTY);
                cmd.Parameters.Add(":Cost", COST * ITEMQTY);

                
                int numRowsAffected = cmd.ExecuteNonQuery();
                
                if (numRowsAffected != 1)
                {
                    throw new Exception(" ERROR: Transaction not Logged");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

            dbMgr.CloseConnection(conn);
            Console.WriteLine("\nNew Transaction Inserted!");
            return transaction;

        }

        public TransactionLog(int TRANSID, string USERSNAME, DateTime TRANS_DATE, string TRANS_TYPE, int ITEMCODE, string ITEMNAME, int ITEMQTY, double COST)
        {
            
            this.TRANSID = TRANSID;
            this.USERSNAME = USERSNAME;
            this.TRANS_DATE = TRANS_DATE;
            this.TRANS_TYPE = TRANS_TYPE;
            this.ITEMCODE = ITEMCODE;
            this.ITEMNAME = ITEMNAME;
            this.ITEMQTY = ITEMQTY;
            this.COST = COST;
        }
        public TransactionLog(DateTime TRANS_DATE, string TRANS_TYPE, int ITEMCODE, string ITEMNAME, int ITEMQTY, double COST)
        {
            this.TRANS_DATE = TRANS_DATE;
            this.TRANS_TYPE = TRANS_TYPE;
            this.ITEMCODE = ITEMCODE;
            this.ITEMNAME = ITEMNAME;
            this.ITEMQTY = ITEMQTY;
            this.COST = COST;
        }
        public TransactionLog(DateTime TRANS_DATE, string TRANS_TYPE, string USERSNAME, int ITEMCODE, string ITEMNAME, int ITEMQTY)
        {
            this.TRANS_DATE = TRANS_DATE;
            this.TRANS_TYPE = TRANS_TYPE;
            this.USERSNAME = USERSNAME;
            this.ITEMCODE = ITEMCODE;
            this.ITEMNAME = ITEMNAME;
            this.ITEMQTY = ITEMQTY;
           
        }
    }
}
