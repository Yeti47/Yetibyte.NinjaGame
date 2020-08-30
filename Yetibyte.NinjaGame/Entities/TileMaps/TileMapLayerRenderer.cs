using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Entities.TileMaps
{

    public class TileMapLayerRenderer : IGameEntity
    {
        public RenderLayer RenderLayer { get; set; }

        public int DrawOrder { get; set; }

        public int UpdateOrder { get; set; }

        public TileMapTileLayer TileMapTileLayer { get; }

        public TileMapLayerRenderer(TileMapTileLayer tileMapTileLayer)
        {
            TileMapTileLayer = tileMapTileLayer;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            TileMapTileLayer?.TileMatrix?.ForEachTile((x, y) =>
            {
                var tile = TileMapTileLayer.TileMatrix.GetTile(x, y);
                var tileSetRef = TileMapTileLayer.ParentTileMap.GetTileSetByGlobalTileId(tile?.Id ?? 0);

                if (tileSetRef?.TileSet is null)
                    return;

                Vector2 pos = new Vector2(x * tileSetRef.TileSet.TileWidth, y * tileSetRef.TileSet.TileHeigth);

                var tileBounds = tileSetRef.TileSet.GetTileBoundsByLocalId(tileSetRef.GetLocalTileId(tile.Id));

                spriteBatch.Draw(tileSetRef.TileSet.Texture, TileMapTileLayer.ParentTileMap.Position + pos, tileBounds, Color.White);

            });
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
