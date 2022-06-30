using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Stationery_Order
{
    public class StockManager
    {
        private DatabaseManager dbMgr;
       
        public StockManager(DatabaseManager dbMgr)
        {
            this.dbMgr = dbMgr;
        }

        public Dictionary<int, Stock> GetAllStock()
        {
            Dictionary<int, Stock> stock = new Dictionary<int, Stock>();

            OracleConnection conn = dbMgr.GetConnection();

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT * FROM STOCK order by itemcode",
                CommandType = CommandType.Text
            };

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                Console.WriteLine("\n Stationery List");
                Console.WriteLine(" ===============\n");
                Console.WriteLine("\t{0, -20} {1, -20} {2} ", "Item Code", "Item Name", "Item QTY");
                Console.WriteLine("\t{0, -20} {1, -20} {2} ", "========", "=========", "=========");
                while (dr.Read())
                {
                   
                    Stock Stock = new Stock(dr.GetInt32(0), dr.GetDateTime(1), dr.GetString(2), dr.GetInt32(3), dr.GetDouble(4)); ;
                    stock.Add(Stock.ITEMCODE, Stock);

                }
                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: retrieval of Stock failed.", e);
            }
            dbMgr.CloseConnection(conn);

            return stock;
        }
        public Dictionary<int, Stock> GetFinancialDetails()
        {
            Dictionary<int, Stock> stock = new Dictionary<int, Stock>();

            OracleConnection conn = dbMgr.GetConnection();

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT Itemcode, Itemname, itemqty, Cost FROM STOCK order by itemcode",
                CommandType = CommandType.Text
            };

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();

                Console.WriteLine("\n Financial Report");
                Console.WriteLine(" ===============\n");
                Console.WriteLine("\t{0, -20} {1, -20} {2} ", "Item Code", "Item Name", "Total Expenditure Per Item");
                Console.WriteLine("\t{0, -20} {1, -20} {2} ", "========", "=========", "=========");
                while (dr.Read())
                {
                    
                    Stock Stock = new Stock(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetDouble(3)); ;
                    stock.Add(Stock.ITEMCODE, Stock);

                }
                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: retrieval of Financial Report failed.", e);
            }
            dbMgr.CloseConnection(conn);

            return stock;
        }

        public Dictionary<double, Stock> GetTotalExpenditure()
        {
            Dictionary<double, Stock> stock = new Dictionary<double, Stock>();

            OracleConnection conn = dbMgr.GetConnection();

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "Select Sum(COST) from stock",
                CommandType = CommandType.Text
            };

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
             
                while (dr.Read())
                {

                    Stock Stock = new Stock(dr.GetDouble(0)); ;
                    stock.Add(Stock.COST, Stock);

                }
                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: retrieval of Total Expenditure failed.", e);
            }
            dbMgr.CloseConnection(conn);

            return stock;
        }
       
        public Stock FindStock(int ITEMCODE)
        {
            Stock stock = null;

            OracleConnection conn = dbMgr.GetConnection();

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT * FROM Stock WHERE ITEMCODE = :ITEMCODE",
                CommandType = CommandType.Text
            };

            try
            {
                cmd.Prepare();
                cmd.Parameters.Add(":ItemCode", ITEMCODE);

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Stock Stock = new Stock(dr.GetInt32(0), dr.GetDateTime(1), dr.GetString(2), dr.GetInt32(3), dr.GetDouble(4));
                    
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: Stock Not Found.", e);
            }

            dbMgr.CloseConnection(conn);

            return stock;
        }

        public Dictionary<int, Stock> AddQtyToStock(int ITEMCODE, int ITEMQTY, DateTime TRANS_DATE, double COST)
        {
            Dictionary<int, Stock> stock = new Dictionary<int, Stock>();

            OracleConnection conn = dbMgr.GetConnection();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "UPDATE Stock SET ITEMQTY =  ITEMQTY + :ITEMQTY, TRANS_DATE = :TRANS_DATE, COST = COST + :COST WHERE ITEMCODE = :ITEMCODE",
                CommandType = CommandType.Text
            };

            try
            {
                cmd.Prepare();
                cmd.Parameters.Add(":ITEMQTY", ITEMQTY);
                cmd.Parameters.Add(":TRANS_DATE", TRANS_DATE);
                cmd.Parameters.Add(":COST", COST * ITEMQTY);
                cmd.Parameters.Add(":ITEMCODE", ITEMCODE);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Stock Stock = new Stock(dr.GetInt32(0), dr.GetDateTime(1), dr.GetString(2), dr.GetInt32(3), dr.GetDouble(4));
                    stock.Add(Stock.ITEMCODE, Stock);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: Failed to Update/Add to Stock Quantity.", e);
            }

            dbMgr.CloseConnection(conn);

            Console.WriteLine("\n Stock Updated/Added to");

            return stock;
        }

        public Dictionary<int, Stock> TakeQtyFromStock(int ITEMCODE, int ITEMQTY)
        {
            Dictionary<int, Stock> stock = new Dictionary<int, Stock>();

            OracleConnection conn = dbMgr.GetConnection();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "UPDATE Stock SET ITEMQTY =  ITEMQTY - :ITEMQTY WHERE ITEMCODE = :ITEMCODE",
                CommandType = CommandType.Text
            };

            try
            {
                cmd.Prepare();
                cmd.Parameters.Add(":ITEMQTY", ITEMQTY);
                cmd.Parameters.Add(":ITEMCODE", ITEMCODE);

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Stock Stock = new Stock(dr.GetInt32(0), dr.GetDateTime(2), dr.GetString(3), dr.GetInt32(4), dr.GetDouble(5));
                    stock.Add(Stock.ITEMCODE, Stock);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: Failed to Update/Remove Quantity from Stock.", e);
            }

            dbMgr.CloseConnection(conn);

            Console.WriteLine("\n Stock Updated/Removed from Stock");

            return stock;
        }
        public Dictionary<string, Stock> AddStock(DateTime TRANS_DATE, string ITEMNAME, int ITEMQTY, double COST)
        {
            Dictionary<string, Stock> stock = new Dictionary<string, Stock>();
            OracleConnection conn = dbMgr.GetConnection();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO Stock (ITEMCODE, TRANS_DATE, ITEMNAME, ITEMQTY, COST) VALUES (Stock_Seq.nextval, :TRANS_DATE, :ITEMNAME, :ITEMQTY, :COST)",
                CommandType = CommandType.Text
            };

            try
            {
                cmd.Prepare();
             
                cmd.Parameters.Add(":TRANS_DATE", TRANS_DATE);
                cmd.Parameters.Add(":ITEMNAME", ITEMNAME);
                cmd.Parameters.Add(":ItemQty", ITEMQTY);
                cmd.Parameters.Add(":COST", ITEMQTY * COST);
                
                OracleDataReader dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Stock Stock = new Stock(dr.GetDateTime(0), dr.GetString(1), dr.GetInt32(2), dr.GetDouble(3));
                    stock.Add(Stock.ITEMNAME, Stock);
                    
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception(" ERROR: Failed to Add New Stock Item to Stock.", e);
            }

            dbMgr.CloseConnection(conn);

            Console.WriteLine("\n New Stock Item Added to Stock");

            return stock;
        }

        public Dictionary<DateTime, TransactionLog> AddTransactionLogs()
        {
            Dictionary<DateTime, TransactionLog> transactions = new Dictionary<DateTime, TransactionLog>();

            OracleConnection conn = dbMgr.GetConnection();

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT trans_date, trans_type, itemcode, itemname, itemqty, cost FROM transactionlog where trans_type LIKE 'Add' order by trans_date",
                CommandType = CommandType.Text
            };

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                Console.WriteLine("\n Transactions Report By (Type: Add)");
                Console.WriteLine("\t{0, -25} {1, -8} {2, -10} {3, -20} {4, -10} {5}", "Date", "Type", "ItemCode", "ItemName", "ItemQty", "Cost");
                while (dr.Read())
                {
                    TransactionLog t = new TransactionLog(dr.GetDateTime(0), dr.GetString(1), dr.GetInt32(2), dr.GetString(3), dr.GetInt32(4), dr.GetDouble(5));
                    transactions.Add(t.TRANS_DATE, t);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: retrieval of Add tansactions failed", e);
            }

            dbMgr.CloseConnection(conn);

            return transactions;
        }

        public Dictionary<string, TransactionLog> RemoveTransactionLogs()
        {
            Dictionary<string, TransactionLog> transactions = new Dictionary<string, TransactionLog>();

            OracleConnection conn = dbMgr.GetConnection();

            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT trans_date, trans_type, usersname, itemcode, itemname, itemqty, cost FROM transactionlog where trans_type LIKE 'Remove' order by trans_date",
                CommandType = CommandType.Text
            };

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();
                Console.WriteLine("\n Transactions Report By (Type: Remove)");
                Console.WriteLine("\t{0, -25} {1, -10} {2, -18} {3, -10} {4, -18} {5}", "Date", "Type", "Usersname", "ItemCode", "ItemName", "ItemQty");
                while (dr.Read())
                {
                    TransactionLog t = new TransactionLog(dr.GetDateTime(0), dr.GetString(1), dr.GetString(2), dr.GetInt32(3), dr.GetString(4), dr.GetInt32(5));
                    transactions.Add(t.ITEMNAME, t);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: retrieval of Remove tansactions failed", e);
            }

            dbMgr.CloseConnection(conn);

            return transactions;
        }
        public Dictionary<int, TransactionLog> GetUserTransactions(string USERSNAME)
        {
            Dictionary<int, TransactionLog> transaction = new Dictionary<int, TransactionLog>();

            OracleConnection conn = dbMgr.GetConnection();
            OracleCommand cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "SELECT * FROM TransactionLog Where Usersname like '%" + USERSNAME + "%' order by trans_date",
                CommandType = CommandType.Text
            };

            try
            {
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmd.Prepare();
                    cmd.Parameters.Add(":Username", USERSNAME);
                    TransactionLog t = new TransactionLog(dr.GetInt32(0), dr.GetString(1), dr.GetDateTime(2), dr.GetString(3), dr.GetInt32(4), dr.GetString(5), dr.GetInt32(6), dr.GetDouble(7));
                    transaction.Add(t.TRANSID, t);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: retrieval of Personal Usage Report failed", e);
            }

            dbMgr.CloseConnection(conn);

            return transaction;
        }
    }
}


