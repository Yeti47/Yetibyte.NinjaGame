using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yetibyte.NinjaGame.Entities;

namespace Yetibyte.NinjaGame.TileMaps
{
    public class TileMapEntityFactory
    {
        private const string CLASS_NAME_PROP_MISSING_ERR_MSG = "ClassName property not found in Custom Properties.";
        private readonly GameServiceContainer _gameServices;
        private readonly ContentManager _content;

        public TileMapEntityFactory(GameServiceContainer gameServices, ContentManager content)
        {
            _gameServices = gameServices;
            this._content = content;
        }

        public ITileMapPlaceable CreateEntity(TileMapObject tileMapObj)
        {
            if (tileMapObj is null)
            {
                throw new ArgumentNullException(nameof(tileMapObj));
            }

            bool success = tileMapObj.CustomProperties.TryGetValue("ClassName", out string className);

            if(!success)
            {
                throw new TileMapObjectCreationException(tileMapObj, CLASS_NAME_PROP_MISSING_ERR_MSG);
            }

            ITileMapPlaceable entity = null;

            try
            {
                entity = (ITileMapPlaceable)Activator.CreateInstance(Type.GetType(className));
                Vector2 position = new Vector2(tileMapObj.X, tileMapObj.Y);
                entity.Initialize(_gameServices, _content, tileMapObj.CustomProperties, position);
            }
            catch(Exception ex)
            {
                throw new TileMapObjectCreationException(tileMapObj, "An error occurred while instantiating the object.", ex);
            }

            return entity;

        }


    }
}
