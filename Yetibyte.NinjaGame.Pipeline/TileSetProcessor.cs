using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Yetibyte.NinjaGame.Pipeline.TileSets;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Pipeline
{
    [ContentProcessor(DisplayName = "TileSet Processor")]
    public class TileSetProcessor : ContentProcessor<TileSetData, TileSet>
    {
        private const string TEXTURE_ROOT_PATH = "/Textures/TileSets";

        public override TileSet Process(TileSetData input, ContentProcessorContext context)
        {
            string tileSetPath = Path.GetFileNameWithoutExtension(input.Image.Source);

            tileSetPath = Path.Combine(TEXTURE_ROOT_PATH, tileSetPath).Replace("\\", "/");
     
            TileSet tileSet = new TileSet(input.Name, input.TileWidth, input.TileHeight, tileSetPath, input.Columns, input.TileCount);

            return tileSet;

        }
    }
}
