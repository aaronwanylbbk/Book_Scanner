﻿using System.Xml.Serialization;

namespace AmazonProductAdvertising.Model
{
    [XmlRoot]
    public class BrowseNodes
    {
        public BaseBrowseNodeLookupRequest Request { get; set; }
        public BrowseNode BrowseNode { get; set; }
    }
}
