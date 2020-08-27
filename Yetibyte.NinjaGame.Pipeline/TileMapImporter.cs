using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Yetibyte.NinjaGame.Pipeline.TileMaps;
using System.Xml;
using System.Xml.Serialization;

namespace Yetibyte.NinjaGame.Pipeline
{
    [ContentImporter(".tmx", DisplayName = "TileMap Importer", DefaultProcessor = "TileMapProcessor")]
    public class TileMapImporter : ContentImporter<TileMapData>
    {
        public override TileMapData Import(string filename, ContentImporterContext context)
        {
            using(FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TileMapData));

                TileMapData tileMapData = xmlSerializer.Deserialize(fileStream) as TileMapData;

                return tileMapData;
            }

        }
    }
}
