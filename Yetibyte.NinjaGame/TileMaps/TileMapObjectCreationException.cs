using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapObjectCreationException : Exception
    {
        public TileMapObject Object { get; private set; }

        public TileMapObjectCreationException(TileMapObject tileMapObj, string message, Exception innerException = null) 
            : base(message, innerException)
        {
            Object = tileMapObj;
        }

    }
}
