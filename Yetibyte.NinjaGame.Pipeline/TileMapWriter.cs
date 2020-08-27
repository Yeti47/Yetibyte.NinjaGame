using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System.Linq;
using Yetibyte.NinjaGame.ContentReaders;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Pipeline
{
    [ContentTypeWriter]
    public class TileMapWriter : ContentTypeWriter<TileMap>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(TileMapReader).AssemblyQualifiedName;
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(TileMap).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, TileMap value)
        {

            output.Write(value.TileWidth);
            output.Write(value.TileHeight);
            output.Write(value.Width);
            output.Write(value.Height);

            output.Write(value.TileSetReferenceCount);

            foreach (TileSetReference tsRef in value.TileSetReferences)
            {
                WriteTileSetReference(output, tsRef);
            }

            output.Write(value.Layers.Count(l => l.LayerType == TileMapLayerType.Tile));

            foreach (TileMapTileLayer layer in value.Layers.OfType<TileMapTileLayer>())
                WriteTileLayer(output, layer);

        }

        private void WriteTileSetReference(ContentWriter output, TileSetReference tileSetReference)
        {
            output.Write(tileSetReference.FirstGid);
            output.Write(tileSetReference.TileSetPath);
        }

        private void WriteTileLayer(ContentWriter output, TileMapTileLayer layer)
        {
            output.Write(layer.Id);
            output.Write(layer.Name);

            output.Write(layer.Width);
            output.Write(layer.Height);

            foreach(var tile in layer.TileMatrix)
            {
                output.Write(tile?.Id ?? 0);
            }

        }

    }
}
