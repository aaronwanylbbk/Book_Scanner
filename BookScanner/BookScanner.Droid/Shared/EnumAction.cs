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
using System.ComponentModel;

namespace BookScanner.Droid.Shared
{
    public class EnumAction
    {
        public enum EnumStatus
        {
            
            Read = 1,
            
            ToRead = 2,
            
            Reading = 3
        }
    }
}