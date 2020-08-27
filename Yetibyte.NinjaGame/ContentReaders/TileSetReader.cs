using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.ContentReaders
{
    public class TileSetReader : ContentTypeReader<TileSet>
    {
        protected override TileSet Read(ContentReader input, TileSet existingInstance)
        {
            if (existingInstance != null)
                return existingInstance;

            string name = input.ReadString();
            int tileWidth = input.ReadInt32();
            int tileHeight = input.ReadInt32();
            string texturePath = input.ReadString();
            int colCount = input.ReadInt32();
            int tileCount = input.ReadInt32();

            return new TileSet(name, tileWidth, tileHeight, texturePath, colCount, tileCount);

        }
    }
}
