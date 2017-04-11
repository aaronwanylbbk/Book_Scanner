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
using Android.Graphics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using Android.Net;

namespace BookScanner.Droid.Shared
{
    public static class HelperMethod
    {
        public static Bitmap GetImageBitmapFromURL(string url)
        {
            Bitmap imageBitmap = null;
            
            using (var webclient = new WebClient())
            {
                try
                {
                    var imageBytes = webclient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                catch (Exception ex)
                {
                    //error in online data
                    return imageBitmap;//return null
                }
            }

            return imageBitmap;
        }

       

        //public static bool CheckForConnection()
        //{
        //    string host = "google.com";
        //    bool result = false;
        //    Ping p = new Ping();
        //    try
        //    {

        //        Ping myPing = new Ping();
        //        byte[] buffer = new byte[32];
        //        int timeout = 10000;
        //        PingOptions pingOptions = new PingOptions();
        //        PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
        //        if (reply.Status == IPStatus.Success)
        //        {
        //            return true;
        //            // presumably online
        //        }
        //    }
        //    catch { }
        //    return result;

        //}





        //public static bool CheckForInternetConnectionRead()
        //{
        //    try
        //    {
        //        //using (var client = new WebClient())
        //        //using (var stream = client.OpenRead("http://www.google.com"))
        //        //{
        //        //    return true;
        //        //}

        //        ConnectivityManager connectivityManager = (ConnectivityManager)
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        public static string FindASIN(string asin)
        {
            string[] lines = asin.Split('/');
            foreach (string line in lines)
            {
                if (line.Length == 10)//asin would be 10 characters
                {
                    Regex regex = new Regex(@"[0-9]");
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        return line;

                    }
                }
            }
            return string.Empty;
        }
        public static int FormatRank(string rank)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(rank))
                result = Convert.ToInt32(rank.Remove(rank.Length - 1));

            return result;
        }

        public static string GetTitle(string text)
        {
            string[] lines = text.Split(new String[] { "\r\n", "\n" }, StringSplitOptions.None);

            if (lines.Length > 0)
            {
                string result = lines[6].Trim();
                if (String.IsNullOrEmpty(result))
                {
                    for (int i = 6; i < 9; i++)
                    {
                        if(!String.IsNullOrEmpty(lines[i].Trim()))
                            return lines[i].Trim();
                    }
                }
                    
                else
                    return result;
            }
                

            return string.Empty;
        }

        public static string FormatString(string text)
        {
            string result = text;
            int init = 35;
            int lengthText = text.Length;
            if (lengthText > init)
            {
                while (lengthText % init > 1)
                {
                    for (int i = init; i < lengthText; i++)
                    {
                        if (result[i - 1].Equals(" "))
                        {
                            result.Insert(lengthText, "\n");
                            lengthText++;
                            init += init;
                            break;
                        }

                    }
                }
            }
            return result;
        }

        public static string GetEnumDescription(int priority)
        {
            string result = string.Empty;
            switch (priority)
            {
                case 1:
                    result = "Read";
                    break;
                case 2:
                    result = "To Read";
                    break;
                case 3:
                    result = "Reading";
                    break;
                default:
                    result = string.Empty;
                break;

            }
            return result;
        }
        public static string GetEnumCode(int priority)
        {
            string result = string.Empty;
            switch (priority)
            {
                case 1:
                    result = "R";
                    break;
                case 2:
                    result = "T";
                    break;
                case 3:
                    result = "I";
                    break;
                default:
                    result = string.Empty;
                    break;

            }
            return result;
        }
        public static string GetCodeEnumByDescription(string desc)
        {
            string result = string.Empty;
            switch (desc)
            {
                case "Read":
                    result = "R";
                    break;
                case "To Read":
                    result = "T";
                    break;
                case "Reading":
                    result = "I";
                    break;
                default:
                    result = string.Empty;
                    break;

            }
            return result;
        }
        public static int GetEnumPriorityByDescription(string desc)
        {
            int result = 0;
            switch (desc)
            {
                case "Read":
                    result = 1;
                    break;
                case "To Read":
                    result = 2;
                    break;
                case "Reading":
                    result = 3;
                    break;
                default:
                    result = 0;
                    break;

            }
            return result;
        }

        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }


    }
}