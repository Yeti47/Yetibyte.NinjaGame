using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yetibyte.NinjaGame.Graphics
{
    public class RenderingStateMachine
    {
        private readonly Dictionary<string, RenderingState> _states = new Dictionary<string, RenderingState>();

        public RenderingState CurrentState { get; private set; } = null;

        public RenderingStateMachine()
        {
        }

        public void SetState(string name)
        {
            CurrentState = GetState(name);
        }

        public RenderingState AddState(string name, IAnimation animation)
        {
            var state = new RenderingState(name, animation);
            _states.Add(name, state);
            return state;
        }

        public RenderingState GetState(string name) => _states[name];

        public void RemoveState(RenderingState state)
        {
            _states.Remove(state.Name);

            if (CurrentState == state)
                CurrentState = null;
        }
        public void RemoveState(string stateName)
        {
            var state = GetState(stateName);

            if (state == null)
                return;

            _states.Remove(stateName);

            if (CurrentState == state)
                CurrentState = null;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            CurrentState?.Animation?.Draw(spriteBatch, position, spriteEffects);
        }

        public void Update(GameTime gameTime)
        {
            CurrentState?.Animation?.Update(gameTime);

        }

    }
}
