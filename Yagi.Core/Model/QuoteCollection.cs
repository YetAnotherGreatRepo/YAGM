namespace Yagi.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class QuoteCollection
    {
        [XmlArray("Quotes")]
        [XmlArrayItem("Quote", typeof(Quote))]
        public Quote[] Quotes { get; set; }
    }
}