using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    public class Argument
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }
}
