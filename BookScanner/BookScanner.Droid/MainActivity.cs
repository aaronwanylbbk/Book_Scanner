using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content.PM;
using Android.Content;
using BookScanner.Droid.FragmentHelper;
using ZXing.Mobile;
using System.Collections.Generic;
using BookScanner.Droid.Shared;
using Android.Net;
using Android.Graphics;
using ZXing;
using System;
using System.Threading;

namespace BookScanner.Droid
{
    [Activity(Label = "Book Scanner", Icon = "@drawable/book_icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        

        ZXing.Mobile.MobileBarcodeScanner scanner;
        FrameLayout frame;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            #region get UI layout
            Button btnScan = FindViewById<Button>(Resource.Id.btnScan);
            Button btnMyBook = FindViewById<Button>(Resource.Id.btnMyBook);
            Button btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            Button btnBooksOfTheMonth = FindViewById<Button>(Resource.Id.btnBooksOfTheMonth);
            #endregion

            #region menu button
            //btnScan.Click += (sender, e) =>
            //{
            //    var viewBook = new Intent(this, typeof(ViewBookActivity));
            //    StartActivity(viewBook);

            //};

            //initialize the scanner first so we can track the current context
            MobileBarcodeScanner.Initialize(Application);
            scanner = new MobileBarcodeScanner();
            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                TryHarder = true
            };

            
            btnScan.Click += async delegate
            {



                PreScan(scanner);

                scanner.UseCustomOverlay = false;
                scanner.FlashButtonText = "Flash";
                scanner.TopText = "Hold the camera upt to the barcode\nAbout 6 inches away";
                scanner.BottomText = "Wait for the barcode to automatically scan";


                var result = await scanner.Scan(options);
                if (IsOnline())
                {
                    DisplayResult(result);
                }
                else
                {
                    this.RunOnUiThread(() => Toast.MakeText(this, "Internet connection problem", ToastLength.Short).Show());
                }


            };
            btnMyBook.Click += (sender, e) =>
            {
                var myBook = new Intent(this, typeof(BookListActivity));
                StartActivity(myBook);
            };

            btnSearch.Click += (sender, e) =>
            {
                var search = new Intent(this, typeof(SearchBookActivity));
                StartActivity(search);
            };
            
            

            btnBooksOfTheMonth.Click += (sender, e) =>
            {
                if (IsOnline())
                {
                    var bookMonth = new Intent(this, typeof(BookOfMonthActivity));
                    StartActivity(bookMonth);
                }
                else
                    this.RunOnUiThread(() => Toast.MakeText(this, "Internet connection problem", ToastLength.Short).Show());

            };


            #endregion

            #region footer
            //var imageView = FindViewById<ImageView>(Resource.Id.logoImageView);
            //imageView.SetImageResource(Resource.Drawable.scanner);

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
        #region scan
        protected delegate void ScanningDoneDelegate();
        protected event ScanningDoneDelegate ScanningDone;

        void PreScan(ZXing.Mobile.MobileBarcodeScanner scanner)
        {


            // Stop timer when done
            //bool blnScanningDone = false;
            //ScanningDoneDelegate sdd = () => { blnScanningDone = true; };
            //ScanningDone += sdd;

            // Start timer
            //TimeSpan ts = new TimeSpan(0, 0, 0, 3, 0);
            //StartTimer(ts, () => {
            //    //if (scanner == null || blnScanningDone)
            //    //{
            //    //    ScanningDone -= sdd;
            //    //    return false; // returning false stops the timer
            //    //}
            //    //else
            //    //    scanner.AutoFocus();

            //    //return true; // returning true makes for recurring timer
            //    if (scanner == null)
            //    {
            //        scanner.AutoFocus();
            //        if (scanner.IsTorchOn)
            //        {
            //            scanner.Torch(true);
            //        }
            //        return true;
            //    }
            //    return false;
            //});

            var t = new Thread(() =>
            {
                while(scanner == null)
                {
                    scanner.AutoFocus();
                    if (scanner.IsTorchOn)
                    {
                        scanner.Torch(true);
                    }
                }
            });
            t.Start();
            Thread.Sleep(1000);
        }

        private void StartTimer(TimeSpan ts, Func<bool> p)
        {
        }

        


        void DisplayResult(ZXing.Result result)
        {
            string resultScan = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                if (IsOnline())
                {
                    resultScan = result.Text;
                   
                    Amazon.AmazonOperation amazon = new Amazon.AmazonOperation();
                    Amazon.ModelAmazon lookup = amazon.FindLookup(resultScan);

                    if (lookup == null)
                    {
                        this.RunOnUiThread(() => Toast.MakeText(this, "Book is not available in Amazon", ToastLength.Short).Show());
                    }
                    else
                    {
                        var intent = new Intent(this, typeof(ViewBookActivity));
                        intent.PutExtra("IsScan", true);
                        intent.PutExtra("lookupString", lookup.ToString());
                        StartActivity(intent);
                    }    
                }
                    

            }
            else
                this.RunOnUiThread(() => Toast.MakeText(this, "Could not scan", ToastLength.Short).Show());
        }

        #endregion

        private Bitmap GetQRCode()
        {
            var writer = new ZXing.Mobile.BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 600,
                    Width = 600
                }
            };
            
            return writer.Write("Varun Rathore");
        }

        public bool IsOnline()
        {
            ConnectivityManager cm = (Android.Net.ConnectivityManager)GetSystemService(Context.ConnectivityService);

            NetworkInfo netInfo = cm.ActiveNetworkInfo;

            return netInfo != null && netInfo.IsConnected;
        }
    }
}

