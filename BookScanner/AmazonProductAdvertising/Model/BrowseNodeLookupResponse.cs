using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    [XmlRoot]
    public class BrowseNodeLookupResponse : AmazonResponse
    {
        public BrowseNodes BrowseNodes { get; set; }
    }
}
