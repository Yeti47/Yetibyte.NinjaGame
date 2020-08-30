using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Entities;
using Yetibyte.NinjaGame.Entities.TileMaps;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.Physics;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.SceneManagement
{
    public class Scene
    {
        private const string OBJECT_TYPE_GAME_ENTITY = "GameEntity";
        private const string OBJECT_TYPE_COLLIDER = "Collider";

        private Texture2D _debugTexture;

        private ISceneDelegate _sceneDelegate;
        private GameServiceContainer _gameServices;
        private TileMapEntityFactory _tileMapEntitiyFactory;

        public string Id { get; private set; }
        public string Name { get; set; }

        public EntityManager EntityManager { get; private set; }

        public TileMap TileMap { get; private set; }

        public bool HasTileMap => TileMap != null;


        public Scene(ContentManager content, GameServiceContainer gameServices, string id, string name, EntityManager entityManager, ISceneDelegate sceneDelegate, TileMap tileMap = null)
        {
            _sceneDelegate = sceneDelegate;
            Id = id;
            Name = name;
            EntityManager = entityManager;
            TileMap = tileMap;
            _gameServices = gameServices;

            _tileMapEntitiyFactory = new TileMapEntityFactory(gameServices, content);

            _debugTexture = content.Load<Texture2D>("Textures/white_pixel");
        }


        public void Initialize()
        {
            _sceneDelegate?.OnInitialize();

            //if (TileMap != null)
            //    TileMap.Position = new Vector2(-100, -1000);

            ProcessTileMap();
        }

        public void Update(GameTime gameTime)
        {
            _sceneDelegate?.OnUpdate(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            
        }

        protected virtual void ProcessTileMap()
        {
            if (TileMap == null)
                return;

            int renderLayer = (int)RenderLayer.One;

            foreach(var tileLayer in TileMap.Layers.OfType<TileMapTileLayer>())
            {
                TileMapLayerRenderer renderer = new TileMapLayerRenderer(tileLayer);
                renderer.RenderLayer = (RenderLayer)renderLayer;

                EntityManager.AddEntity(renderer);
                renderLayer++;
            }

            foreach(var tileMapObjLayer in TileMap.Layers.OfType<TileMapObjectLayer>())
            {
                foreach(var tileMapObj in 
                    tileMapObjLayer.Objects
                    .Where(o => o.ObjectType == TileMapObjectType.Point && o.Type == OBJECT_TYPE_GAME_ENTITY))
                {
                    ITileMapPlaceable placeableObj = _tileMapEntitiyFactory.CreateEntity(tileMapObj);

                    EntityManager.AddEntity(placeableObj);
                }
                
                foreach(var tileMapObj in tileMapObjLayer.Objects
                    .Where(o => (o.ObjectType == TileMapObjectType.Polygon || o.ObjectType == TileMapObjectType.Rectangle) && o.Type == OBJECT_TYPE_COLLIDER ))
                {
                    TileMapCollisionObject collisionObject = new TileMapCollisionObject(tileMapObj, _debugTexture);
                    Collider collider = null;

                    if (tileMapObj.ObjectType == TileMapObjectType.Polygon)
                    {
                        collider = _gameServices.GetService<IPhysicsManager>()?.CreatePolygonCollider(collisionObject, ((TileMapPolygonObject)tileMapObj).Vertices, 0);
                    }
                    else
                    {
                        collider = _gameServices.GetService<IPhysicsManager>()?.CreateRectCollider(collisionObject, ((TileMapRectangleObject)tileMapObj).Size, false);
                    }

                    collisionObject.Collider = collider;
                    this.EntityManager.AddEntity(collisionObject);

                }

            }

        }

    }
}
