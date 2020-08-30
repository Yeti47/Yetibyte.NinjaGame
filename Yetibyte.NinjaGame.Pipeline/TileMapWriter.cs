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

            output.Write(value.Layers.Count(l => l.LayerType == TileMapLayerType.Object));

            foreach (TileMapObjectLayer objLayer in value.Layers.OfType<TileMapObjectLayer>())
                WriteObjectLayer(output, objLayer);

        }

        private void WriteTileSetReference(ContentWriter output, TileSetReference tileSetReference)
        {
            output.Write(tileSetReference.FirstGid);
            output.Write(tileSetReference.TileSetPath);
        }

        private void WriteTileLayer(ContentWriter output, TileMapTileLayer layer)
        {
            output.Write(layer.Id);
            output.Write(layer.Name ?? string.Empty);

            output.Write(layer.Width);
            output.Write(layer.Height);

            foreach (var tile in layer.TileMatrix)
            {
                output.Write(tile?.Id ?? 0);
            }

        }

        private void WriteObjectLayer(ContentWriter output, TileMapObjectLayer layer)
        {
            output.Write(layer.Id);
            output.Write(layer.Name ?? string.Empty);

            output.Write(layer.Objects.Count(o => o.ObjectType == TileMapObjectType.Point));

            foreach(var pointObject in layer.Objects.OfType<TileMapPointObject>())
            {
                WriteSharedObjectData(output, pointObject);
            }

            output.Write(layer.Objects.Count(o => o.ObjectType == TileMapObjectType.Polygon));

            foreach (var polygonObj in layer.Objects.OfType<TileMapPolygonObject>())
            {
                WriteSharedObjectData(output, polygonObj);

                output.Write(polygonObj.VertexCount);

                foreach(var vert in polygonObj.Vertices)
                {
                    output.Write(vert);
                }

            }

            output.Write(layer.Objects.Count(o => o.ObjectType == TileMapObjectType.Rectangle));

            foreach (var rectObj in layer.Objects.OfType<TileMapRectangleObject>())
            {
                WriteSharedObjectData(output, rectObj);

                output.Write(rectObj.Width);
                output.Write(rectObj.Height);
            }

        }

        private void WriteSharedObjectData(ContentWriter output, TileMapObject obj)
        {

            output.Write(obj.Id);
            output.Write(obj.Name ?? string.Empty);
            output.Write(obj.Type ?? string.Empty);

            output.Write(obj.X);
            output.Write(obj.Y);

            output.Write(obj.CustomProperties.Count);

            foreach (var keyValuePair in obj.CustomProperties)
            {
                output.Write(keyValuePair.Key);
                output.Write(keyValuePair.Value);
            }

        }

    }
}
