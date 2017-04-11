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
using BookScanner.Droid.Adapter;
using BookScanner.Droid.Data;
using BookScanner.Droid.Shared;
using BookScanner.Droid.Amazon;
using BookScanner.Droid.FragmentHelper;

namespace BookScanner.Droid
{
    [Activity(Label = "Books of the Month")]
    public class BookOfMonthActivity : Activity
    {
        private GridView gridView;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.BookOfTheMonth);

            gridView = FindViewById<GridView>(Resource.Id.gridview);

            
            
            List<ModelGrid> modelGrid = GetListGrid();

            if (modelGrid.Count > 0)
            {
                gridView.Adapter = new GridViewAdapter(this, Resource.Layout.GridLayout, modelGrid);

                gridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
                {
                    var grid = (ModelGrid)args.Parent.GetItemAtPosition(args.Position);

                    var intent = new Intent(this, typeof(ViewBookActivity));

                    intent.PutExtra("imageBook", grid.getImageBook());
                    intent.PutExtra("ASIN", grid.getAsin());
                    intent.PutExtra("titleBook", grid.getTitleBook());
                    intent.PutExtra("isbn", grid.getISBN());
                    intent.PutExtra("urlImage", grid.getUrlImage());
                    intent.PutExtra("IsScan", false);
                    //intent.PutExtra("author", grid.getAuthor());
                    //intent.PutExtra("pageNumber", grid.getPageNumber());
                    //intent.PutExtra("publishedDate", grid.getPublishedDate());
                    //intent.PutExtra("currency", grid.getCurrency());
                    //intent.PutExtra("price", grid.getPrice());

                    StartActivity(intent);
                };
            }
            else
            {
                Toast.MakeText(this, "Sorry, problem occured ", ToastLength.Short).Show();
            }

            #region button
            Button btnRead = FindViewById<Button>(Resource.Id.btnRead);
            Button btnToRead = FindViewById<Button>(Resource.Id.btnToRead);
            Button btnReading = FindViewById<Button>(Resource.Id.btnReading);
            
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

        private List<ModelGrid> GetListGrid()
        {
            List<ModelGrid> model = new List<ModelGrid>();
            AmazonOperation amazon = new AmazonOperation();
            var result = amazon.GetBestSeller();

            if (result == null)
                return model;

            foreach (var item in result)
            {
                model.Add(new ModelGrid(item.ASIN, item.Title, HelperMethod.GetImageBitmapFromURL(item.ImageURL),item.ISBNCode,item.EANCode,
                    item.Author,item.PageNumber,item.PublishedDate, item.Currency, item.Price, item.ImageURL, item.URL
                    ));
            }

            return model;
        }

        public List<ModelGrid> Dummy()
        {
            List<ModelGrid> model = new List<ModelGrid>();
            AmazonOperation amazon = new AmazonOperation();
            var result = amazon.RetrieveDataBookSeller();
            //for (int i=0;i<20;i++)
            //{
            //    model.Add(new ModelGrid(i.ToString(), "Test" + i+1, 
            //        Convert()));
            //}

            return model;
        }

        public Android.Graphics.Bitmap Convert()
        {
            return Android.Graphics.BitmapFactory.DecodeResource(Resources,Resource.Drawable.Bulbs);
        }

        
    }
}