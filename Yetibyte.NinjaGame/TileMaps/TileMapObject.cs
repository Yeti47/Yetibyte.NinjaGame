using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{

    public abstract class TileMapObject
    {

        public abstract TileMapObjectType ObjectType { get; }

        public string Name { get; private set; }
        public int Id { get; private set; }

        public int X { get; set; }
        public int Y { get; set; }

        protected TileMapObject(string name, int id, int x, int y)
        {
            Name = name;
            Id = id;
            X = x;
            Y = y;
        }


    }
}
