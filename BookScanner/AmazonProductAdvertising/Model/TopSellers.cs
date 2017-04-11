using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    [XmlRoot]
    public class TopSellers
    {
        [XmlElement("TopSeller")]
        public TopSeller[] TopSeller { get; set; }
    }
}
