//using System.Web;

namespace AmazonProductAdvertising.Model
{
    public class ItemLink
    {
        private string _url;

        public string Description { get; set; }
        public string URL
        {
            get { return this._url; }
            //set { this._url = HttpUtility.UrlDecode(value); }
            set { this._url = value; }
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
