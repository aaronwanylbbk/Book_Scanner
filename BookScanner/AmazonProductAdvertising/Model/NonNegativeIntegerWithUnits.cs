﻿using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    public class NonNegativeIntegerWithUnits
    {
        public string Units { get; set; }
        [XmlText(DataType = "nonNegativeInteger")]
        public string Value { get; set; }
    }
}
