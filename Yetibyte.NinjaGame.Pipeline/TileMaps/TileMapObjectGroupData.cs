using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yetibyte.NinjaGame.Pipeline.TileMaps
{
    public class TileMapObjectGroupData
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "object")]
        public List<TileMapObjectData> Objects { get; set; }
    }
}
