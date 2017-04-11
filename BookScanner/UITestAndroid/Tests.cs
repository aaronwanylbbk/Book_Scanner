using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using BookScanner.Droid;
using BookScanner.Droid.Amazon;
using AmazonProductAdvertising.Operation;
using System.Collections.Generic;
using AmazonProductAdvertising;
using BookScanner.Droid.Shared;

namespace UITestAndroid
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            //app = ConfigureApp.Android.StartApp();
            // TODO: Update this path to point to your Android app and uncomment the
            // code if the app is not included in the solution.
            //.ApkFile ("../../../Android/bin/Debug/UITestsAndroid.apk")

        }

        [Test]
        public void AppLaunches()
        {
            //app.Screenshot("First screen.");
        }

        [Test]
        public void TestAmazon()
        {
            //AmazonOperation amazon = new AmazonOperation();

            //amazon.FindLookup("978-1411686915");

        }

        //[Test]
        //public List<ModelAmazon> SearchItemByRequest(string author, string title)
        //{
        //    AmazonAuthentication authentication = new AmazonAuthentication();
        //    authentication = new AmazonAuthentication();
        //    authentication.AccessKey = ApplicationSetting.AccessKey();
        //    authentication.SecretKey = ApplicationSetting.SecretKey();
        //    var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK);
        //    List<ModelAmazon> model = new List<ModelAmazon>();
        //    try
        //    {
        //        var customOperation = new AmazonOperationBase();
        //        customOperation.ParameterDictionary.Add("Operation", "ItemSearch");
        //        customOperation.ParameterDictionary.Add("Author", author);
        //        customOperation.ParameterDictionary.Add("Title", title);
        //        customOperation.ParameterDictionary.Add("ItemPage", "1");
        //        customOperation.ParameterDictionary.Add("AssociateTag", "nagerat-21");

        //        customOperation.SearchIndex(AmazonSearchIndex.Books);

        //        customOperation.ResponseGroup(AmazonResponseGroup.Large);
        //    }
        //    catch
        //    {
        //    }
        //    return model;
        //}

        [Test]
        public void search()
        {
            
            AmazonOperation amazon = new AmazonOperation();
            amazon.RetrieveDataBookSeller();
            //amazon.SearchItemByRequest("joe", "");
        }
    }
}

