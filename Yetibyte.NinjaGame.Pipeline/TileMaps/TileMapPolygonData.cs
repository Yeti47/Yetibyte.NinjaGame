using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Yetibyte.NinjaGame.Pipeline.TileMaps
{
    public class TileMapPolygonData
    {
        [XmlAttribute(AttributeName = "points")]
        public string Points { get; set; }
    }
}
