using Microsoft.Xna.Framework.Graphics;

namespace Yetibyte.NinjaGame.Entities.Players
{
    public struct PlayerTextureContainer
    {
        public Texture2D Walk { get; set; }
        public Texture2D WalkArmed { get; set; }
        public Texture2D Idle { get; set; }
        public Texture2D IdleArmed { get; set; }
        public Texture2D Jump { get; set; }
        public Texture2D JumpArmed { get; set; }
        public Texture2D JumpAttack { get; set; }
        public Texture2D JumpToss { get; set; }
        public Texture2D Toss { get; set; }
        public Texture2D Hurt { get; set; }
        public Texture2D HurtArmed { get; set; }
        public Texture2D Attack { get; set; }
    }
}
