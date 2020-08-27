using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.Xml.Serialization;
using Yetibyte.NinjaGame.Pipeline.TileSets;

namespace Yetibyte.NinjaGame.Pipeline
{
    [ContentImporter(".tsx", DisplayName = "TileSet Importer", DefaultProcessor =  "TileSetProcessor")]
    public class TileSetImporter : ContentImporter<TileSetData>
    {
        public override TileSetData Import(string filename, ContentImporterContext context)
        {
            
            using(FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TileSetData));

                TileSetData data = xmlSerializer.Deserialize(fileStream) as TileSetData;

                context.Logger.LogMessage(data.ToString());

                return data;
            }

        }
    }
}
