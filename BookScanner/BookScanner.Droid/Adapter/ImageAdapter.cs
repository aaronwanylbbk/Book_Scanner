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

namespace BookScanner.Droid.Adapter
{
    public class ImageAdapter : BaseAdapter
    {
        Context context;
        public ImageAdapter(Context c)
        {
            this.context = c;
        }
        public override int Count
        {
            get
            {
                return thumbIds.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageView imageView;

            if (convertView == null) //if it's not recycled, initialize some attributes
            {
                imageView = new ImageView(context);
                imageView.LayoutParameters = new GridView.LayoutParams(85, 85);
                imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
                imageView.SetPadding(8, 8, 8, 8);
            }
            else
            {
                imageView = (ImageView)convertView;
            }

            imageView.SetImageResource(thumbIds[position]);
            

            //imageView.SetImageResource(thumbIds[position]);
            return imageView;
        }

        int[] thumbIds = {
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits,
            //Resource.Drawable.icon,Resource.Drawable.Fruits
        };
        

        
    }
}