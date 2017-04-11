using System.Collections.Generic;
using Android.Widget;
using BookScanner.Droid.Data;
using Android.App;
using Android.Views;
using System;
using BookScanner.Droid.Shared;

namespace BookScanner.Droid.Adapter
{
    public class BookListAdapter : BaseAdapter<Book>
    {
        List<Book> bookList;
        Activity context;


        public BookListAdapter(Activity context, List<Book> bookList)
        {
            this.context = context;
            this.bookList = bookList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count
        {
            get
            {
                return bookList.Count;
            }
        }
        public override Book this[int position]
        {
            get
            {
                return bookList[position];
            }
        }

       

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var items = bookList[position];

            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);



            //view.FindViewById<TextView>(Resource.Id.Text1).Text = items.Heading;
            
            view.FindViewById<TextView>(Resource.Id.customTitle).Text = items.Title;
            view.FindViewById<ImageView>(Resource.Id.customImage).SetImageBitmap(HelperMethod.GetImageBitmapFromURL(items.UrlImage));
            view.FindViewById<TextView>(Resource.Id.customAuthor).Text = items.Author;
            view.FindViewById<TextView>(Resource.Id.customStatus).Text = "["+items.StatusCode+"]";



            return view;

        }


        
    }
}