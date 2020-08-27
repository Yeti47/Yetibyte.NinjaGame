using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Graphics;

namespace Yetibyte.NinjaGame.Entities
{
    public class EntityManager : Yetibyte.NinjaGame.Core.IUpdateable
    {
        private readonly List<IGameEntity> _entities = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();

        private readonly Dictionary<RenderLayer, SpriteBatch> _spriteBatches = new Dictionary<RenderLayer, SpriteBatch>();

        public EntityManager(GraphicsDevice graphicsDevice)
        {
            InitializeRenderLayers(graphicsDevice);
        }

        private void InitializeRenderLayers(GraphicsDevice graphicsDevice)
        {
            foreach(RenderLayer layer in Enum.GetValues(typeof(RenderLayer)))
            {
                _spriteBatches.Add(layer, new SpriteBatch(graphicsDevice));
            }
        }

        public bool AddEntity(IGameEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (HasEntity(entity))
                return false;

            _entitiesToAdd.Add(entity);

            return true;
        }

        public bool RemoveEntity(IGameEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!HasEntity(entity))
                return false;

            _entitiesToRemove.Add(entity);

            return true;

        }

        public bool HasEntity(IGameEntity entity) => _entities.Contains(entity) || _entitiesToAdd.Contains(entity) || _entitiesToRemove.Contains(entity);

        public void Update(GameTime gameTime)
        {
            foreach(IGameEntity entity in _entities.OrderBy(e => e.UpdateOrder))
            {
                entity.Update(gameTime);
            }

            foreach (IGameEntity entity in _entitiesToAdd)
                _entities.Add(entity);

            foreach (IGameEntity entity in _entitiesToRemove)
                _entities.Remove(entity);

            this._entitiesToAdd.Clear();
            this._entitiesToRemove.Clear();
        }

        public void Draw(GameTime gameTime)
        {
            foreach(var renderGroup in _entities.GroupBy(e => e.RenderLayer).OrderBy(g => g.Key))
            {
                SpriteBatch spriteBatch = _spriteBatches[renderGroup.Key];

                spriteBatch.Begin(SpriteSortMode.Deferred);

                foreach (var entity in renderGroup.OrderBy(e => e.DrawOrder))
                {
                    entity.Draw(spriteBatch, gameTime);
                }

                spriteBatch.End();
            }
        }


    }
}
