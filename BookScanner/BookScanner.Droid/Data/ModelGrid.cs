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
using Android.Graphics;
using Java.Lang;

namespace BookScanner.Droid.Data
{
    public class ModelGrid : Java.Lang.Object
    {
        private Bitmap imageBook;
        private string titleBook;
        private string ASIN;

        private string isbn;
        private string author;
        private string pageNumber;
        private string publishedDate;
        private string currency;
        private string price;
        private string urlImage;
        private string ean;
        public string urlDetail;
        public ModelGrid(string ASIN, string TitleBook, Bitmap ImageBook, string isbn, string ean, string author, string pageNumber, string publishedDate, string currency, string price, string urlImage, string urlDetail)
        {
            this.ASIN = ASIN;
            this.imageBook = ImageBook;
            this.titleBook = TitleBook;
            this.isbn = isbn;
            this.author = author;
            this.pageNumber = pageNumber;
            this.publishedDate = publishedDate;
            this.currency = currency;
            this.price = price;
            this.urlImage = urlImage;
            this.ean = ean;
            this.urlDetail = urlDetail;
        }

        public void setUrlImage(string url)
        {
            this.urlImage = url;
        }
        public string getUrlImage()
        {
            return urlImage;
        }

        public void setUrlDeail(string url)
        {
            this.urlDetail = url;
        }
        public string getUrlDetail()
        {
            return urlDetail;
        }
        public void setIsbn(string isbn)
        {
            this.isbn = isbn;
        }
        public string getISBN()
        {
            return isbn;
        }

        public void setEan(string ean)
        {
            this.ean = ean;
        }
        public string getEan()
        {
            return ean;
        }
        public void setAuthor(string author)
        {

            this.author = author;
        }
        public string getAuthor()
        {
            return author;
        }

        public void setPageNumber(string pageNumber)
        {
            this.pageNumber = pageNumber;
        }
        public string getPageNumber()
        {
            return pageNumber;
        }
        public void setPublishedDate(string publishedDate)
        {
            this.publishedDate = publishedDate;
        }
        public string getPublishedDate()
        {
            return publishedDate;
        }

        public void setCurrency(string currency)
        {
            this.currency = currency;
        }
        public string getCurrency()
        {
            return currency;
        }

        public void setPrice(string price)
        {
            this.price = price;
        }
        public string getPrice()
        {
            return price;
        }


        

        public void setAsin(string asin)
        {
            this.ASIN = asin;
        }
        public void setTitleBook(string titleBook)
        {
            this.titleBook = titleBook;
        }

        public void setImageBook(Bitmap imageBook)
        {
            this.imageBook = imageBook;
        }
        public string getAsin() {
            return ASIN;
        }
        public string getTitleBook() {
            return titleBook;
        }
        public Bitmap getImageBook() {
            return imageBook;
        }

        
    }
}