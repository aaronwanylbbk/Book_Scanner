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
using Java.Lang;
using BookScanner.Droid.Data;

namespace BookScanner.Droid.Adapter
{
    public class GridViewAdapter : BaseAdapter
    {
        Context context;
        private int LayoutResourceId;
        private List<ModelGrid> data = new List<ModelGrid>();
        public GridViewAdapter(Context c, int layoutResourceId, List<ModelGrid> data)
        {
            this.context = c;
            this.LayoutResourceId = layoutResourceId;
            this.data = data;
        }
        public override int Count
        {
            get
            {
                return data.Count;
            }
        }
        public override Java.Lang.Object GetItem(int position)
        {

            return new ModelGrid(data[position].getAsin(), data[position].getTitleBook(), data[position].getImageBook(), data[position].getISBN(),data[position].getEan(), data[position].getAuthor(),
                data[position].getPageNumber(),data[position].getPublishedDate(),data[position].getCurrency(),data[position].getPrice(), data[position].getUrlImage(), data[position].getUrlDetail());

            
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            ViewHolder holder = null;
            if (row == null)
            {
                var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                row = inflater.Inflate(LayoutResourceId, parent, false);
                holder = new ViewHolder();
                //holder.ASIN = row.FindViewById<TextView>(Resource.Id.textASIN);
                holder.BookTitle = row.FindViewById<TextView>(Resource.Id.gridText);
                holder.BookImage = row.FindViewById<ImageView>(Resource.Id.gridImage);

                row.Tag = holder;
            }
            else
            {
                holder = row.Tag as ViewHolder;
            }

            ModelGrid model = data[position];
            //holder.ASIN.Text = model.getAsin();
            holder.BookTitle.Text = model.getTitleBook();
            holder.BookImage.SetImageBitmap(model.getImageBook());


            return row;
        }


    }
}