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

namespace BookScanner.Droid.Data
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string ASIN { get; set; }//amazon 
        public string ISBNCode { get; set; }
        public string EANCode { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string PageNumber { get; set; }
        public string PublishedDate { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
        public string UrlImage { get; set; }
        public int StatusPriority { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }

        public DateTime Updated { get; set; }
        [MaxLength(500)]
        public string Note { get; set; }

        public string URL { get; set; }

        public string Description { get; set; }
        public override string ToString()
        {
            return string.Format("Id={0}; ASIN={1}; ISBNCode={2}; EANCode={3}; Author={4};Title={5};PageNumber={6};PublishedDate={7};Currency={8};Price={9};UrlImage={10};StatusPriority={11};StatusCode={12};StatusName={13};" +
               "Note={14}; Updated={15}; Description={16};URL={17}", Id, ASIN, ISBNCode, EANCode, Author, Title, PageNumber, PublishedDate, Currency, Price, UrlImage, StatusPriority, StatusCode, StatusName, Note, Updated, Description, URL);

        }


    }
}