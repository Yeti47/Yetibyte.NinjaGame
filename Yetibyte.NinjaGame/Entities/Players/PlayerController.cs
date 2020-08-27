using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Yetibyte.NinjaGame.Core;

namespace Yetibyte.NinjaGame.Entities.Players
{
    public class PlayerController : Core.IUpdateable
    {
        private readonly Player _player;

        private KeyboardState _previousKeyboardState;
        private MouseState _previousMouseState;

        public PlayerController(Player player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void Update(GameTime gameTime)
        {
            ProcessControls(gameTime);
        }

        private void ProcessControls(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.D) && _player.CanWalk)
            {
                _player.Move(true);
            }
            else if(keyboardState.IsKeyDown(Keys.A) && _player.CanWalk)
            {
                _player.Move(false);
            }
            else if (!_player.Velocity.X.IsRoughlyZero())
            {
                _player.Halt();
            }

            if(!_previousKeyboardState.IsKeyDown(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
            {
                _player.Jump();
            }

            var mouseState = Mouse.GetState();
            
            if(mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
            {
                _player.Attack();
            }

            _previousKeyboardState = keyboardState;
            _previousMouseState = mouseState;
        }

    }
}
