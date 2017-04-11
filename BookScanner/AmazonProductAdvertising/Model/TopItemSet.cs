using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    [XmlRoot]
    public class TopItemSet
    {
        public string Type { get; set; }
        [XmlElement("TopItem")]
        public TopItem[] TopItem { get; set; }
    }
}
