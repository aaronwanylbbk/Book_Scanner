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
using AmazonProductAdvertising;
using AmazonProductAdvertising.Model;
using BookScanner.Droid.Shared;
using Android.Util;
using AmazonProductAdvertising.Operation;

namespace BookScanner.Droid.Amazon
{
    public class AmazonOperation
    {
        AmazonAuthentication authentication;

        public AmazonOperation()
        {
            authentication = new AmazonAuthentication();
            authentication.AccessKey = ApplicationSetting.AccessKey();
            authentication.SecretKey = ApplicationSetting.SecretKey();
        }
        public List<ModelAmazon> RetrieveDataBookSeller()
        {
            List<ModelAmazon> listModel = new List<ModelAmazon>();
            try
            {
                var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK);
                
                for (int i = 0; i < 2; i++)
                {
                    var searchOperation = wrapper.ItemSearchOperation(ApplicationSetting.SearchIndex(), AmazonSearchIndex.Books);

                    searchOperation.ResponseGroup(AmazonResponseGroup.SalesRank);
                    searchOperation.Sort(AmazonSearchSort.Salesrank, AmazonSearchSortOrder.Ascending);
                    searchOperation.ParameterDictionary.Add("ItemPage", (i+1).ToString());
                    var webResponse = wrapper.Request(searchOperation);

                    var result = XmlHelper.ParseXml<ItemSearchResponse>(webResponse.Content);

                    foreach (var item in result.Items.Item)
                    {
                        listModel.Add(FindLookup(item.ASIN));
                    }
                }
                return listModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public List<ModelAmazon> GetBestSeller()
        {
            List<ModelAmazon> model = new List<ModelAmazon>();
            
            
            try
            {
                HTMLAmazon html = new HTMLAmazon();

                List<ModelAmazon> results = html.GetBestSeller();

                foreach (var result in results)
                {
                    model.Add(new ModelAmazon
                    {
                        ASIN = result.ASIN,
                        ImageURL = result.ImageURL,
                        Title = result.Title
                    });
                }

                return model;
            }
            catch (Exception ex)
            {
                Log.Info(ApplicationSetting.ApplicationName(), "GetBestSeller "+ex.Message);
                return null;
            }
        }

        public ModelAmazon FindLookup(string item)
        {
            var wrapperLookup = new AmazonWrapper(authentication, AmazonEndpoint.UK);
            

            ModelAmazon amazon = null;
            try
            {
                var resultInit = (wrapperLookup.Lookup(item)).Items;
                if (resultInit != null)
                {
                    var resultItem = resultInit.Item[0];

                    amazon = new ModelAmazon();
                    amazon.ASIN = resultItem.ASIN;
                    amazon.ISBNCode = resultItem.ItemAttributes.ISBN;
                    amazon.Author = ArrayString(resultItem.ItemAttributes.Author);
                    amazon.Title = resultItem.ItemAttributes.Title;
                    amazon.PageNumber = resultItem.ItemAttributes.NumberOfPages;
                    amazon.PublishedDate = resultItem.ItemAttributes.PublicationDate;
                    amazon.ImageURL = resultItem.SmallImage == null ? 
                        (resultItem.MediumImage == null ? (resultItem.LargeImage == null ? string.Empty : resultItem.LargeImage.URL) : resultItem.MediumImage.URL)
                        : resultItem.SmallImage.URL;
                    amazon.EANCode = resultItem.ItemAttributes.EAN;
                    amazon.Price = resultItem.OfferSummary.LowestUsedPrice.FormattedPrice.Remove(0,1);
                    amazon.Currency = resultItem.OfferSummary.LowestUsedPrice.FormattedPrice.Substring(0, 1);
                    amazon.URL = resultItem.DetailPageURL;
                } 
                

                
            }
            catch (Exception ex)
            {
            }
            

            return amazon;
        }

        public ModelAmazon FindByEAN(string item)
        {
            var wrapperLookup = new AmazonWrapper(authentication, AmazonEndpoint.UK);


            ModelAmazon amazon = null;
            try
            {
                var resultItem = (wrapperLookup.Lookup(item, AmazonResponseGroup.Large)).Items.Item;

                foreach (var itin in resultItem)
                {
                    amazon = FindLookup(itin.ASIN);

                    if (amazon != null)
                        return amazon;
                }

              
            }
            catch (Exception ex)
            {
            }


            return amazon;
        }

        public string ArrayString(string[] array)
        {
            string result = string.Empty;
            try
            {
                foreach (string ar in array)
                {
                    result += ar + ",";
                }

                if (!String.IsNullOrEmpty(result))
                    result = result.Remove(result.Length - 1);
            }
            catch (Exception ex)
            { }
           
            

            return result;
        }


        public List<ModelAmazon> SearchItemByRequest(string author, string title)
        {
            var wrapper = new AmazonWrapper(authentication, AmazonEndpoint.UK);
            List<ModelAmazon> model = new List<ModelAmazon>();
            try
            {
                var customOperation = new AmazonOperationBase();
                customOperation.ParameterDictionary.Add("Operation", "ItemSearch");
                if(!String.IsNullOrEmpty(author))
                    customOperation.ParameterDictionary.Add("Author", author);
                if(!String.IsNullOrEmpty(title))
                    customOperation.ParameterDictionary.Add("Title", title);
                customOperation.ParameterDictionary.Add("ItemPage", "1");
                customOperation.ParameterDictionary.Add("AssociateTag", "nagerat-21");

                customOperation.SearchIndex(AmazonSearchIndex.Books);
                
                customOperation.ResponseGroup(AmazonResponseGroup.Large);

                var webResponse = wrapper.Request(customOperation);

                

                var result = XmlHelper.ParseXml<ItemSearchResponse>(webResponse.Content);

                foreach (var resultItem in result.Items.Item)
                {
                    model.Add(new ModelAmazon
                    {
                            ASIN = resultItem.ASIN,
                            ISBNCode = resultItem.ItemAttributes.ISBN,
                            Author = ArrayString(resultItem.ItemAttributes.Author),
                            Title = resultItem.ItemAttributes.Title,
                            PageNumber = resultItem.ItemAttributes.NumberOfPages,
                            PublishedDate = resultItem.ItemAttributes.PublicationDate,
                            ImageURL = resultItem.SmallImage == null ?
                                (resultItem.MediumImage == null ? (resultItem.LargeImage == null ? string.Empty : resultItem.LargeImage.URL) : resultItem.MediumImage.URL)
                                : resultItem.SmallImage.URL,
                            EANCode = resultItem.ItemAttributes.EAN,
                            Price = resultItem.OfferSummary.LowestUsedPrice.FormattedPrice.Remove(0, 1),
                            Currency = resultItem.OfferSummary.LowestUsedPrice.FormattedPrice.Substring(0, 1)
                });
                }
            }
            catch(Exception ex)
            {
            }
            return model;
        }
    }
}