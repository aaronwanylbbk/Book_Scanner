using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    [XmlRoot]
    public class BrowseNodeLookupRequest
    {
        public string BrowseNodeId { get; set; }
        public string ResponseGroup { get; set; }
    }
}
