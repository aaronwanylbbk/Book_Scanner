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
using Android.Content.PM;
using BookScanner.Droid.Shared;
using BookScanner.Droid.Data;
using BookScanner.Droid.Adapter;
using BookScanner.Droid.FragmentHelper;

namespace BookScanner.Droid
{
    [Activity(Label = "Book Scanner")]
    public class BookListActivity : Activity
    {
        //string[] items;
        List<ModelItem> modelItems = new List<ModelItem>();
        List<Book> BookList = new List<Book>();
        ListView listView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.BookList);

            Button showPopupMenu = FindViewById<Button>(Resource.Id.buttonSort);
            
            #region list view
            listView = FindViewById<ListView>(Resource.Id.List);

            DataOperation dataOperation = new DataOperation();
            BookList = dataOperation.GetAllBookInfos();

           

            string ListString = Intent.GetStringExtra("ListId") ?? string.Empty;
            List<int> newID = new List<int>();
            if (!String.IsNullOrEmpty(ListString))
            {
                string [] listEach = ListString.Split(';');
                foreach (var e in listEach)
                {
                    newID.Add(Convert.ToInt32(e));
                }
                BookList = BookList.Where(e => newID.Contains(e.Id)).ToList();
            }
            listView.Adapter = new BookListAdapter(this, BookList);
            #endregion

            if (BookList.Count == 0)
            {
                showPopupMenu.Visibility = ViewStates.Invisible;
            }

            listView.ItemClick += OnListItemClick;

            #region sorting
            showPopupMenu.Click += (s, arg) => {

                PopupMenu menu = new PopupMenu(this, showPopupMenu);

                // with Android 3 need to use MenuInfater to inflate the menu
                //menu.MenuInflater.Inflate (Resource.Menu.popup_menu, menu.Menu);

                // with Android 4 Inflate can be called directly on the menu
                menu.Inflate(Resource.Menu.popup_menu);

                menu.MenuItemClick += (s1, arg1) => {
                    if (arg1.Item.TitleFormatted.ToString() == "Book Title")
                    {
                        BookList = BookList.OrderBy(e => e.Title).ToList();
                    }
                    else if (arg1.Item.TitleFormatted.ToString() == "Author")
                    {
                        BookList = BookList.OrderBy(e => e.Author).ToList();
                    }
                    else if (arg1.Item.TitleFormatted.ToString() == "Status")
                    {
                        BookList = BookList.OrderBy(e => e.StatusPriority).ToList();
                    }
                    listView.Adapter = new BookListAdapter(this, BookList);
                };

                // Android 4 now has the DismissEvent
                menu.DismissEvent += (s2, arg2) => {
                    Console.WriteLine("menu dismissed");
                };

                menu.Show();
            };
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

        void OnListItemClick(Object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var BookString = BookList[e.Position].ToString();

            var intent = new Intent(this, typeof(UpdateBookActivity));
          
            intent.PutExtra("bookString", BookString);
            StartActivity(intent);
        }




    }
    
}