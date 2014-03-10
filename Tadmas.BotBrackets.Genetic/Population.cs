using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tadmas.BotBrackets.Genetic
{
    public class Population
    {
        public int Generation { get; set; }

        private List<Genome> _Members;
        public List<Genome> Members
        {
            get
            {
                if (_Members == null)
                    _Members = new List<Genome>();

                return _Members;
            }
        }

        public string Serialize()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                XmlSerializer serializer = new XmlSerializer(typeof(Population));
                serializer.Serialize(new StringWriter(sb), this);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static Population Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Population));
            return serializer.Deserialize(new StringReader(xml)) as Population;
        }
    }
}
