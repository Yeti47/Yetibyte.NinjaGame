using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yetibyte.NinjaGame.Entities;
using Yetibyte.NinjaGame.Entities.TileMaps;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.SceneManagement
{
    public class Scene
    {
        private ISceneDelegate _sceneDelegate;

        public string Id { get; private set; }
        public string Name { get; set; }

        public EntityManager EntityManager { get; private set; }

        public TileMap TileMap { get; private set; }

        public bool HasTileMap => TileMap != null;


        public Scene(string id, string name, EntityManager entityManager, ISceneDelegate sceneDelegate, TileMap tileMap = null)
        {
            _sceneDelegate = sceneDelegate;
            Id = id;
            Name = name;
            EntityManager = entityManager;
            TileMap = tileMap;
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
        }

    }
}
