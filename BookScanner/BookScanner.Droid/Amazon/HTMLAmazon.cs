using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;
using System.IO;
using System.Net;
using BookScanner.Droid.Shared;
using Android.Util;

namespace BookScanner.Droid.Amazon
{
    public class HTMLAmazon
    {
        const string urlAddress = "https://www.amazon.co.uk/gp/bestsellers/books/ref=sv_b_1";
        public HTMLAmazon()
        {
        }

        public List<ModelAmazon> GetBestSeller()
        {
            List<ModelAmazon> model = new List<ModelAmazon>();
            try
            {

                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(getHTML());

                var finalHtml = htmlDoc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("zg_itemImmersion"));


                
                HtmlDocument childDoc;

                foreach (var load in finalHtml)
                {
                    childDoc = new HtmlDocument();
                    childDoc.LoadHtml(load.InnerHtml);

                    HtmlNode text = childDoc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"].Value == "zg_rankDiv").FirstOrDefault().ChildNodes.FirstOrDefault(e => e.Name == "span");

                    var urlTitle = childDoc.DocumentNode.Descendants("div").Where(x => x.Attributes["class"].Value == "zg_itemWrapper").Select(e => e.InnerText);

                    var images = childDoc.DocumentNode.Descendants("img").Select(e => e.GetAttributeValue("src", null)).Where(c => !String.IsNullOrEmpty(c));

                    var urlHref = childDoc.DocumentNode.Descendants("a").Where(e => e.Attributes["class"].Value == "a-link-normal").Select(e => e.GetAttributeValue("href", null)).Where(c => !String.IsNullOrEmpty(c)).FirstOrDefault();


                    model.Add(new ModelAmazon
                    {
                        Rank = HelperMethod.FormatRank(text.InnerText.Trim()),
                        ImageURL = String.Join("", images),
                        Title = HelperMethod.GetTitle(String.Join("", urlTitle)),
                        ASIN = HelperMethod.FindASIN(urlHref.ToString())

                    });
                    
                }

                return model.OrderBy(e => e.Rank).ToList();
            }
            catch (Exception ex)
            {
                Log.Info(ApplicationSetting.ApplicationName(), "HTMLAmazon, GetBestSeller " + ex.Message);
                return null;
            }

        }

        public static string getHTML()
        {
            string data = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();



                response.Close();
                readStream.Close();
            }
            return data;
        }
    }
}