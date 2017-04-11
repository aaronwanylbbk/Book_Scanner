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

namespace BookScanner.Droid.Amazon
{
    public class ModelAmazon
    {
        public int Rank { get; set; }
        public string ASIN { get; set; }//amazon 

        public string ISBNCode { get; set; }
        public string EANCode { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string PageNumber { get; set; }
        public string PublishedDate { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string ImageURL { get; set; }
        public string URL { get; set; }

        public override string ToString()
        {
            return string.Format("ASIN={0}; ISBNCode={1}; EANCode={2}; Author={3}; Title={4};PageNumber={5};PublishedDate={6};Price={7};Currency={8};ImageURL={9};URL={10}"
                , ASIN, ISBNCode, EANCode, Author, Title, PageNumber, PublishedDate, Price, Currency, ImageURL,URL);
        }
    }
}