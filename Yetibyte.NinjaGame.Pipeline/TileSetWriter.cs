using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yetibyte.NinjaGame.ContentReaders;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Pipeline
{
    [ContentTypeWriter]
    public class TileSetWriter : ContentTypeWriter<TileSet>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(TileSetReader).AssemblyQualifiedName;
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(TileSet).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, TileSet value)
        {
            output.Write(value.Name);
            output.Write(value.TileWidth);
            output.Write(value.TileHeigth);
            output.Write(value.TexturePath);
            output.Write(value.ColumnCount);
            output.Write(value.TileCount);
          
        }
    }
}
