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

using System.Threading.Tasks;
using System.Net;

namespace BookScanner.Droid.Shared
{
    public static class ApplicationSetting
    {
        private const string applicationName = "BookScanner";
        public static string ApplicationName()
        {
            return applicationName;
        }

        #region Amazon
        private static string searchIndex = "Books";
        private static string browseNode = "1025612";
        private static string associateTag = "nagerat-21";
        private static string responseGroup = "TopSellers";

        private static string accessKey = "AKIAJN2DBGIOIJ7SY7XA";
        private static string secretKey = "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";
        public static string AccessKey()
        {
            return accessKey;
        }
        public static string SecretKey()
        {
            return secretKey;
        }
        public static string SearchIndex()
        {
            return searchIndex;
        }
        public static string BrowseNode()
        {
            return browseNode;
        }

        public static string AssociateTag()
        {
            return associateTag;
        }

        public static string ResponseGroup()
        {
            return responseGroup;
        }
        #endregion

        
        
    }
}
