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
using BookScanner.Droid.FragmentHelper;
using BookScanner.Droid.Data;
using BookScanner.Droid.Shared;

namespace BookScanner.Droid
{
    [Activity(Label = "Book Scanner")]
    public class SearchBookActivity : Activity
    {
        EditText _dateDisplay;
        ImageButton _imageButton;
        string textRadio = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SearchBook);

            _dateDisplay = FindViewById<EditText>(Resource.Id.editPublishedDate);

            EditText author = FindViewById<EditText>(Resource.Id.editAuthor);
            EditText title = FindViewById<EditText>(Resource.Id.editTitle);
            EditText pageNumber = FindViewById<EditText>(Resource.Id.editPageNumber);
            EditText publishedDate = FindViewById<EditText>(Resource.Id.editPublishedDate);
            EditText price = FindViewById<EditText>(Resource.Id.editPrice);
            RadioButton radioRead = FindViewById<RadioButton>(Resource.Id.radio_read);
            RadioButton radioToRead = FindViewById<RadioButton>(Resource.Id.radio_ToRead);
            RadioButton radioReading = FindViewById<RadioButton>(Resource.Id.radio_Reading);

            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.radio_group_search);

            Button btnSearch = FindViewById<Button>(Resource.Id.buttonSearch);

            radioRead.Click += RadioButtonUpdateClick;
            radioToRead.Click += RadioButtonUpdateClick;
            radioReading.Click += RadioButtonUpdateClick;

            btnSearch.Click += (sender, e) =>
            {

                DataOperation data = new DataOperation();
                List<Book> booklist = data.GetBookInfo(author.Text, title.Text, pageNumber.Text, publishedDate.Text, price.Text,
                    HelperMethod.GetCodeEnumByDescription(textRadio));

                if (booklist.Count > 0)
                {
                    string listID = string.Empty;
                    foreach (var list in booklist)
                    {
                        listID += list.Id.ToString()+";";
                    }
                    listID = listID.Remove(listID.Length-1);


                    var intent = new Intent(this, typeof(BookListActivity));

                    intent.PutExtra("ListId", listID);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "The query can not found", ToastLength.Short).Show();
                }

            };

            #region DatePicker

            _imageButton = FindViewById<ImageButton>(Resource.Id.imageCalendar);

            _imageButton.Click += (sender, e) =>
              {
                  DateSelect_OnClick(sender, e);
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


        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time) {
                //_dateDisplay.Text = time.ToLongDateString();
                _dateDisplay.Text = time.ToString("yyyy-MM-dd");
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void RadioButtonUpdateClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            textRadio = rb.Text;
        }
    }
}