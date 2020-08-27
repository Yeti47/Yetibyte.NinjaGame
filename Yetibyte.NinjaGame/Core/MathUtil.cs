using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.Core
{
    public static class MathUtil
    {
        public const float EPSIOLON = 0.0001f;

        public static bool IsRoughlyZero(this float val) => Math.Abs(val) < EPSIOLON;

        public static int RoundToInt(float val) => (int)Math.Round(val);

    }
}
