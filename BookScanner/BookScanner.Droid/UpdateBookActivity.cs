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
using BookScanner.Droid.Data;
using BookScanner.Droid.FragmentHelper;
using BookScanner.Droid.Shared;

namespace BookScanner.Droid
{
    [Activity(Label = "Book Scanner")]
    public class UpdateBookActivity : Activity
    {
        string initStatusCode = string.Empty;
        int initStatusPriority;
        string initStatusName = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UpdateBook);

            ImageView updateImage = FindViewById<ImageView>(Resource.Id.UpdateImage);
            TextView textIsbnCode = FindViewById<TextView>(Resource.Id.textIsbn);
            TextView textAuthor = FindViewById<TextView>(Resource.Id.textAuthor);
            TextView textTitle = FindViewById<TextView>(Resource.Id.textTitle);
            TextView textPageNumber = FindViewById<TextView>(Resource.Id.textPageNumber);
            TextView textPublishedDate = FindViewById<TextView>(Resource.Id.textPublishedDate);
            TextView textPrice = FindViewById<TextView>(Resource.Id.textPrice);
            TextView textDescription = FindViewById<TextView>(Resource.Id.textDescription);

            RadioButton radioRead = FindViewById<RadioButton>(Resource.Id.update_radio_read);
            RadioButton radioToRead = FindViewById<RadioButton>(Resource.Id.update_radio_ToRead);
            RadioButton radioReading = FindViewById<RadioButton>(Resource.Id.update_radio_Reading);

            RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.updateRadioGroup);

            TextView textNote = FindViewById<TextView>(Resource.Id.textNote);
            string BookString = Intent.GetStringExtra("bookString") ?? string.Empty;

            radioRead.Click += RadioButtonClick;
            radioToRead.Click += RadioButtonClick;
            radioReading.Click += RadioButtonClick;


            Book book = null;
            if (!String.IsNullOrEmpty(BookString))
            {
                book = new Book();
                string[] bookArray = BookString.Split(';');
                
                string [] bookId = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Id)).FirstOrDefault()).Split('=');
                book.Id = Convert.ToInt32(bookId[1]);

                string[] aSIN = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.ASIN)).FirstOrDefault()).Split('=');
                book.ASIN = aSIN[1];

                string[] iSBNCode = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.ISBNCode)).FirstOrDefault()).Split('=');
                book.ISBNCode = iSBNCode[1];

                string[] author = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Author)).FirstOrDefault()).Split('=');
                book.Author = author[1];

                string[] title = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Title)).FirstOrDefault()).Split('=');
                book.Title = title[1];

                string[] pageNumber = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.PageNumber)).FirstOrDefault()).Split('=');
                book.PageNumber = pageNumber[1];

                string[] publishedDate = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.PublishedDate)).FirstOrDefault()).Split('=');
                book.PublishedDate = publishedDate[1];

                string[] currency = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Currency)).FirstOrDefault()).Split('=');
                book.Currency = currency[1];

                string[] price = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Price)).FirstOrDefault()).Split('=');
                book.Price = price[1];

                string[] urlImage = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.UrlImage)).FirstOrDefault()).Split('=');
                book.UrlImage = urlImage[1];

                string[] statusPriority = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.StatusPriority)).FirstOrDefault()).Split('=');
                book.StatusPriority = Convert.ToInt32(statusPriority[1]);

                string[] statusCode = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.StatusCode)).FirstOrDefault()).Split('=');
                book.StatusCode = statusCode[1];

                string[] statusName = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.StatusName)).FirstOrDefault()).Split('=');
                book.StatusName = statusName[1];

                string[] note = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Note)).FirstOrDefault()).Split('=');
                book.Note = note[1];

                string[] description = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.Description)).FirstOrDefault()).Split('=');
                book.Description = description[1];

                string[] urlDetail = (bookArray.Where(e => e.ToString().Split('=')[0].Trim() == HelperMethod.GetMemberName((Book b) => b.URL)).FirstOrDefault()).Split('=');
                book.URL = urlDetail[1];

                updateImage.SetImageBitmap(HelperMethod.GetImageBitmapFromURL(book.UrlImage));
                textIsbnCode.Text = book.ISBNCode;
                textAuthor.Text = book.Author;
                textTitle.Text = book.Title;
                textPageNumber.Text = book.PageNumber;
                textPublishedDate.Text = book.PublishedDate;
                textPrice.Text = book.Price;
                textDescription.Text = book.Description;

                if (book.StatusCode == "R")
                    radioGroup.Check(radioRead.Id);
                else if (book.StatusCode == "T")
                    radioGroup.Check(radioToRead.Id);
                else if (book.StatusCode == "I")
                    radioGroup.Check(radioReading.Id);

                textNote.Text = book.Note;


                //initialize
                initStatusCode = book.StatusCode;
                initStatusPriority = book.StatusPriority;
                initStatusName = book.StatusName;
            }

            


            #region toolbar
            var toolbarBottom = FindViewById<Toolbar>(Resource.Id.toolbar_bottom);

            toolbarBottom.Title = "Update Book";
            toolbarBottom.InflateMenu(Resource.Menu.ToolbarMenu);
            toolbarBottom.MenuItemClick += (sender, e) =>
            {
                if (book == null)
                    Toast.MakeText(this, "Data value is null", ToastLength.Short).Show();
                else
                {
                    //update note and status
                    book.Note = textNote.Text;
                    book.StatusCode = initStatusCode;
                    book.StatusName = initStatusName;
                    book.StatusPriority = initStatusPriority;
                    book.Updated = DateTime.Now;

                    DataOperation operation = new DataOperation();
                    bool update = operation.UpdateData(book);

                    if(update)
                        Toast.MakeText(this, book.Title+" Saved successfully", ToastLength.Short).Show();
                    else
                        Toast.MakeText(this, "Failed to update", ToastLength.Short).Show();
                }
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

        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            initStatusCode = HelperMethod.GetCodeEnumByDescription(rb.Text);
            initStatusPriority = HelperMethod.GetEnumPriorityByDescription(rb.Text);
            initStatusName = rb.Text;
        }
    }
}