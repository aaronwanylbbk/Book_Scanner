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

namespace BookScanner.Droid.FragmentHelper
{

    
    public class DialogFragmentPop : DialogFragment
    {
        public static DialogFragmentPop NewInstance(Bundle bundle)
        {
            DialogFragmentPop fragment = new DialogFragmentPop();
            fragment.Arguments = bundle;
            
            return fragment;
        }

        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            //use this to return your custom view for this fragment
            View view = inflater.Inflate(Resource.Layout.PopUpAboutUs, container, false);

            Button button = view.FindViewById<Button>(Resource.Id.CloseButton);
            
            button.Click += delegate
            {
                Dismiss();
                
            };

            return view;
        }
        
    

    }
}