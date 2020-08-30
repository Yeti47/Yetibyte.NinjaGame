using System;
using System.Text;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.TileMaps
{

    public abstract class TileMapObject
    {

        public abstract TileMapObjectType ObjectType { get; }

        public string Name { get; private set; }

        public string Type { get; private set; }
        public int Id { get; private set; }

        public float X { get; set; }
        public float Y { get; set; }

        public PropertyMap CustomProperties { get; private set; }

        protected TileMapObject(string name, string type, int id, float x, float y)
        {
            Name = name;
            Id = id;
            X = x;
            Y = y;
            Type = type;

            CustomProperties = new PropertyMap();

        }


    }
}
