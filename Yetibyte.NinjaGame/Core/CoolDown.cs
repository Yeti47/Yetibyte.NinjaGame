using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.Core
{
    public class CoolDown
    {
        private bool _isRunning = true;
        public float Duration { get; }

        public float Progress { get; set; }

        public bool IsReady => Progress <= MathUtil.EPSIOLON;

        public CoolDown(float duration)
        {
            Duration = duration;
            
        }

        public void Update(GameTime gameTime)
        {
            if (!_isRunning)
                return;

            Progress += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Progress > Duration)
            {
                Progress = 0;
                _isRunning = false;
            }
        }

        public bool Do(Action action)
        {
            if (!IsReady)
                return false;

            action?.Invoke();

            _isRunning = true;

            return true;

        }

        

    }
}
