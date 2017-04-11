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
using BookScanner.Droid.Data;
using Android.Graphics;
using BookScanner.Droid.FragmentHelper;
using BookScanner.Droid.Amazon;
using BookScanner.Droid.Shared;
using static BookScanner.Droid.Shared.EnumAction;

namespace BookScanner.Droid
{
    [Activity(Label = "Book Scanner")]
    public class ViewBookActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.ViewBook);
            bool isScan = Intent.GetBooleanExtra("IsScan", false);
            ModelGrid model = null;

            if (isScan)
            {
                ModelAmazon modelIntent = new ModelAmazon();
                string intentString = Intent.GetStringExtra("lookupString") ?? string.Empty;
                if (String.IsNullOrEmpty(intentString))
                {
                    //error
                    var mainIntent = new Intent(this, typeof(MainActivity));
                    StartActivity(mainIntent);
                }
                else
                {
                    string[] amazonArray = intentString.Split(';');

                    string[] asin = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.ASIN)).FirstOrDefault()).Split('=');
                    modelIntent.ASIN = asin[1];

                    string[] iSBNCode = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.ISBNCode)).FirstOrDefault()).Split('=');
                    modelIntent.ISBNCode = iSBNCode[1];

                    string[] eANCode = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.EANCode)).FirstOrDefault()).Split('=');
                    modelIntent.EANCode = eANCode[1];

                    string[] authorArr = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.Author)).FirstOrDefault()).Split('=');
                    modelIntent.Author = authorArr[1];

                    string[] titleArr = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.Title)).FirstOrDefault()).Split('=');
                    modelIntent.Title = titleArr[1];

                    string[] pageNumberArr = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.PageNumber)).FirstOrDefault()).Split('=');
                    modelIntent.PageNumber = pageNumberArr[1];

                    string[] publishedDateArr = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.PublishedDate)).FirstOrDefault()).Split('=');
                    modelIntent.PublishedDate = publishedDateArr[1];

                    string[] priceArr = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.Price)).FirstOrDefault()).Split('=');
                    modelIntent.Price = priceArr[1];

                    string[] currencyArr = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.Currency)).FirstOrDefault()).Split('=');
                    modelIntent.Currency = currencyArr[1];

                    string[] imageURL = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.ImageURL)).FirstOrDefault()).Split('=');
                    modelIntent.ImageURL = imageURL[1];

                    string[] detailURL = (amazonArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((ModelAmazon b) => b.URL)).FirstOrDefault()).Split('=');
                    modelIntent.URL = detailURL[1];


                    model = new ModelGrid(modelIntent.ASIN,
                   modelIntent.Title,
                   String.IsNullOrEmpty(modelIntent.ImageURL) ?  null : (HelperMethod.GetImageBitmapFromURL(modelIntent.ImageURL)),
                   modelIntent.ISBNCode,
                   modelIntent.EANCode,
                   modelIntent.Author,
                   modelIntent.PageNumber,
                   modelIntent.PublishedDate,
                   modelIntent.Currency,
                   modelIntent.Price,
                   modelIntent.ImageURL,
                   modelIntent.URL);

                }
            }
            else
            {
                #region collected the data
                AmazonOperation amazon = new AmazonOperation();
                ModelAmazon lookup = amazon.FindLookup(Intent.GetStringExtra("ASIN"));

                if (lookup == null)
                {
                    //error
                    var mainIntent = new Intent(this, typeof(MainActivity));
                    StartActivity(mainIntent);
                }
                else
                {
                    //continue
                    model = new ModelGrid(Intent.GetStringExtra("ASIN") ?? string.Empty,
                    Intent.GetStringExtra("titleBook") ?? string.Empty,
                    (Bitmap)Intent.GetParcelableExtra("imageBook"),
                    lookup.ISBNCode ?? string.Empty,
                    lookup.EANCode ?? string.Empty,
                    lookup.Author,
                    lookup.PageNumber,
                    lookup.PublishedDate,
                    lookup.Currency,
                    lookup.Price,
                    Intent.GetStringExtra("urlImage") ?? string.Empty,
                    lookup.URL);
                }
                
                #endregion
            }


            #region mapping 
            ImageView bookImage = FindViewById<ImageView>(Resource.Id.viewImage);
            bookImage.SetImageBitmap(model.getImageBook());
            TextView bookTitle = FindViewById<TextView>(Resource.Id.viewTitle);
            bookTitle.Text = model.getTitleBook();
            TextView isbnCode = FindViewById<TextView>(Resource.Id.viewIsbnCode);
            isbnCode.Text = model.getISBN();
            TextView author = FindViewById<TextView>(Resource.Id.viewAuthor);
            author.Text = model.getAuthor();
            TextView pageNumber = FindViewById<TextView>(Resource.Id.viewPageNumber);
            pageNumber.Text = model.getPageNumber();
            TextView publishedDate = FindViewById<TextView>(Resource.Id.viewPublishedDate);
            publishedDate.Text = model.getPublishedDate();
            TextView price = FindViewById<TextView>(Resource.Id.viewPrice);
            price.Text = model.getCurrency()+" " +model.getPrice();
            TextView url = FindViewById<TextView>(Resource.Id.viewURL);
            url.Text = model.getUrlDetail();
            #endregion

            #region button
            Button btnRead = FindViewById<Button>(Resource.Id.btnRead);
            Button btnToRead = FindViewById<Button>(Resource.Id.btnToRead);
            Button btnReading = FindViewById<Button>(Resource.Id.btnReading);

            #region collected data
            Book book = new Book();
            book.Title = model.getTitleBook();
            book.ASIN = model.getAsin();
            book.Author = model.getAuthor();
            book.ISBNCode = model.getISBN();
            book.EANCode = model.getEan();
            book.Note = string.Empty;
            book.PageNumber = model.getPageNumber();
            book.Price = model.getPrice();
            book.PublishedDate = model.getPublishedDate();
            book.UrlImage = model.getUrlImage();
            book.Currency = model.getCurrency();
            book.Description = model.getTitleBook();//set title as description
            book.URL = model.getUrlDetail();
            #endregion

            DataOperation dataOperation;
            bool operation = false;
            string message = string.Empty;
            btnRead.Click += (sender, e) =>
            {
                operation = false;

                book.StatusPriority = (int)EnumStatus.Read;
                book.StatusCode = HelperMethod.GetEnumCode((int)EnumStatus.Read);
                book.StatusName = HelperMethod.GetEnumDescription((int)EnumStatus.Read);
                book.Updated = DateTime.Now;

                dataOperation = new DataOperation();
                operation = dataOperation.ExecuteDataOperation(book);

                if (operation)
                    Toast.MakeText(this, book.Title +"\nSuccess save as Read", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "fail to save", ToastLength.Short).Show();
            };
            btnToRead.Click += (sender, e) =>
            {
                operation = false;

                book.StatusPriority = (int)EnumStatus.ToRead;
                book.StatusCode = HelperMethod.GetEnumCode((int)EnumStatus.ToRead);
                book.StatusName = HelperMethod.GetEnumDescription((int)EnumStatus.ToRead);
                book.Updated = DateTime.Now;

                dataOperation = new DataOperation();
                operation = dataOperation.ExecuteDataOperation(book);

                if (operation)
                    Toast.MakeText(this, book.Title + "\nSuccess save as To Read", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "fail to save", ToastLength.Short).Show();
            };
            btnReading.Click += (sender, e) =>
            {
                operation = false;

                book.StatusPriority = (int)EnumStatus.Reading;
                book.StatusCode = HelperMethod.GetEnumCode((int)EnumStatus.Reading);
                book.StatusName = HelperMethod.GetEnumDescription((int)EnumStatus.Reading);
                book.Updated = DateTime.Now;

                dataOperation = new DataOperation();
                operation = dataOperation.ExecuteDataOperation(book);

                if (operation)
                    Toast.MakeText(this, book.Title + "\nSuccess save as Reading", ToastLength.Short).Show();
                else
                    Toast.MakeText(this, "fail to save", ToastLength.Short).Show();
            };
            #endregion



            #region footer
            Button btnHome = FindViewById<Button>(Resource.Id.btnHome);
            Button btnAboutUs = FindViewById<Button>(Resource.Id.btnAboutUs);

            btnHome.Click += (sender, e) =>
            {
                var main = new Intent(this, typeof(MainActivity));
                StartActivity(main);

            };

            btnAboutUs.Click += (sender, e) =>
            {
                FragmentTransaction ft = FragmentManager.BeginTransaction();
                //Remove fragment else it will crash as it is already added to backstack
                Fragment prev = FragmentManager.FindFragmentByTag("dialog");
                if (prev != null)
                {
                    ft.Remove(prev);
                }

                ft.AddToBackStack(null);

                // Create and show the dialog.
                DialogFragmentPop newFragment = DialogFragmentPop.NewInstance(null);

                //Add fragment
                newFragment.Show(ft, "dialog");
            };
            #endregion
        }
    }
}
