using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using SQLite;
using Android.Util;
using BookScanner.Droid.Shared;


namespace BookScanner.Droid.Data
{
    public class DataOperation
    {
        SQLiteConnection db = null;
        string ApplicationName = string.Empty;
        public DataOperation()
        {
            IDatabaseConnection sql = (IDatabaseConnection)new DatabaseConnection();
            db = sql.DBConnection();
            ApplicationName = ApplicationSetting.ApplicationName();
        }
        public bool insertUpdateData(Book data)
        {
            try
            {
                //InsertTable();
                if (db.Insert(data) != 0)
                    db.Update(data);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "insertUpdateData" +ex.Message);
                return false;
            }
        }

        public bool UpdateData(Book data)
        {
            try
            {
                db.Update(data);
                return true;
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "UpdateData" + ex.Message);
                return false;
            }
        }

        public bool ExecuteDataOperation(Book book)
        {
            try
            {
                Book myBook = GetBookByASIN(book.ASIN);
                if (myBook == null)
                    return insertUpdateData(book);
                else
                {
                    //keep notes
                    book.Id = myBook.Id;
                    book.Note = myBook.Note;
                    return UpdateDataExistingASIN(book);
                }
                    
                    

                
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "ExecuteDataOperation" + ex.Message);
                return false;
            }
        }
        public bool UpdateDataExistingASIN(Book data)
        {
            try
            {
                db.Update(data);
                return true;
            }        
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "UpdateDataExistingASIN" +ex.Message);
                return false;
            }
        }

        //public Book CheckBookIsExistByASIN(string ASIN)
        //{
        //    try
        //    {
        //        return GetBookByASIN(ASIN);
                
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Log.Info(ApplicationName, "CheckBookIsExistByASIN" + ex.Message);
        //        return null;
        //    }
        //}

        public Book GetBookById(int ID)
        {
            Book book = null;
            try
            {
                book = db.Query<Book>("SELECT * FROM BOOK WHERE Id", ID).FirstOrDefault();
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, ex.Message);
            }
            return book;

        }

        public Book GetBookByASIN(string ASIN)
        {
            Book book = null;
            try
            {

                book = db.Query<Book>("SELECT * FROM BOOK WHERE ASIN = ?", ASIN).FirstOrDefault();
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "GetBookByASIN " +ex.Message);
            }
            return book;
        }

        public List<Book> GetBookInfo(string author, string title, string pageNumber, string publishedDate, string price, string status)
        {
            List<Book> bookList = new List<Book>();
            try
            {
                
                
                string query = "SELECT * FROM BOOK";
                string where = " WHERE ";
                string builder = string.Empty;
                
                string parameters  = string.Empty;

                
                if (!String.IsNullOrEmpty(author))
                {
                    builder += String.IsNullOrEmpty(builder) ? "AUTHOR LIKE ?" : " AND AUTHOR LIKE ?";

                    parameters += ('%' + author + '%') + ",";
                }
                if (!String.IsNullOrEmpty(title))
                {
                    builder += String.IsNullOrEmpty(builder) ? "TITLE LIKE ?" : " AND TITLE LIKE ?";
                    parameters += ('%' + title + '%') + ",";
                }
                if (!String.IsNullOrEmpty(pageNumber))
                {
                    builder += String.IsNullOrEmpty(builder) ? "PAGENUMBER = ?" : " AND PAGENUMBER = ?";
                    parameters += (pageNumber) + ",";
                }
                if (!String.IsNullOrEmpty(publishedDate))
                {
                    builder += String.IsNullOrEmpty(builder) ? "PUBLISHEDDATE = ?" : " AND PUBLISHEDDATE = ?";
                    parameters += (publishedDate)+",";
                }
                if (!String.IsNullOrEmpty(price))
                {
                    builder += String.IsNullOrEmpty(builder) ? "PRICE = ?" : " AND PRICE = ?";
                    parameters += (price) + ",";
                }
                if (!String.IsNullOrEmpty(status))
                {
                    builder += String.IsNullOrEmpty(builder) ? "STATUSCODE = ?" : " AND STATUSCODE = ?";
                    parameters += (status) + ","; 
                }


                //fill parameter
                string[] newParameter = new string[] { };
                if (!String.IsNullOrEmpty(parameters))
                {
                    parameters = parameters.Remove(parameters.Length - 1);
                    newParameter = new string[parameters.Split(',').Count()];

                    newParameter = parameters.Split(',');
                }

                if (builder.Length > 0)
                {
                    
                    query += where + builder + " AND STATUSCODE IS NOT NULL";

                    parameters = parameters.Remove(parameters.Length - 1);

                    

                    //bookList = db.Query<Book>(query, "%tom%", "%tom%");
                    bookList = db.Query<Book>(query, newParameter);
                }
                else
                {
                    query += " WHERE STATUSCODE IS NOT NULL";
                    bookList = db.Query<Book>(query);
                }


                
                
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "GetInfo " + ex.Message);
            }
            return bookList;
        }

        public List<Book> GetAllBookInfos()
        {
            List<Book> bookList = new List<Book>();
            try
            {
                bookList = db.Query<Book>("SELECT * FROM BOOK WHERE STATUSCODE IS NOT NULL");
            }
            catch (SQLiteException ex)
            {
                Log.Info(ApplicationName, "GetAllBookInfos " + ex.Message);
            }
            return bookList;
        }


        //public void InsertTable()
        //{
        //    string tableName = "Books";
        //    SQLite.TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
        //    object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

        //    Int32 tableCount = db.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'", ps).Count; // Executes the query from which we can count the results

        //    if (tableCount == 0)
        //    {
        //        db.CreateTable<Book>();//database created first time    
        //    }
            
        //    else
        //    {
        //        throw new Exception("More than one table by the name of " + tableName + " exists in the database.", null);

        //    }
        //}

        
    }
}