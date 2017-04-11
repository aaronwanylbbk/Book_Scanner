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
using SQLite;
using System.IO;

namespace BookScanner.Droid.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection DBConnection()
        {
            var dbName = "db_book.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var pathToDatabase = System.IO.Path.Combine(documentsPath, dbName);

            SQLiteConnection conn = new SQLiteConnection(pathToDatabase);


            conn.CreateTable<Book>();//database created first time    

            return conn;
        }
    }
}