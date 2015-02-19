namespace Yagi.Core.Model
{
    using System;

    [Serializable]
    public class Quote
    {
        [System.Xml.Serialization.XmlElement("Text")]
        public string Text { get; set; }

        [System.Xml.Serialization.XmlElement("Author")]
        public string Author { get; set; }
    }
}