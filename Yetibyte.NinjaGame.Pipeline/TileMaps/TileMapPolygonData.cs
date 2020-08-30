using Microsoft.Xna.Framework;
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

        public IEnumerable<Vector2> GetVertices()
        {
            string[] pointString = Points.Split(' ');

            List<Vector2> verts = new List<Vector2>();

            foreach(string point in pointString)
            {
                string[] vertComponents = point.Split(',');

                verts.Add(new Vector2(float.Parse(vertComponents[0]), float.Parse(vertComponents[1])));
            }

            return verts;

        }

    }
}
