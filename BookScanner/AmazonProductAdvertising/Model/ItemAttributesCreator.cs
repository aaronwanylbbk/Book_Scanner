using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    public class ItemAttributesCreator
    {
        [XmlAttribute]
        public string Role { get; set; }
        [XmlText]
        public string Name { get; set; }
    }
}
