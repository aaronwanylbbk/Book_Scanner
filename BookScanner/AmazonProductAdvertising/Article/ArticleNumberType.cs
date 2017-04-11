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

namespace AmazonProductAdvertising.Article
{
    public enum ArticleNumberType
    {
        UNKNOWN,
        ASIN, //Amazon Standard Identification Number
        EAN8, //European Article Number
        EAN13, //European Article Number
        GTIN, //Global Trade Item Number (previously EAN - European Article Number)
        ISBN10, //International Standard Book Number
        ISBN13, //International Standard Book Number
        SKU, //Stock keeping unit
        UPC, //Universal Product Code
    }
}