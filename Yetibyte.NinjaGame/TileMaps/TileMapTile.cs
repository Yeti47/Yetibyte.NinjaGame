using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapTile
    {
        public int Id { get; private set; }

        public bool IsEmpty => Id <= 0;

        private TileMapTile()
        {

        }

        public TileMapTile(int id)
        {
            Id = id;
        }

    }
}
