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

namespace BookScanner.Droid.Adapter
{
    public class ViewHolder : Java.Lang.Object
    {
        //public TextView ASIN; 
        public TextView BookTitle;
        public ImageView BookImage;
    }
}