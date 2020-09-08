using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Yetibyte.NinjaGame.Audio;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.Physics;

namespace Yetibyte.NinjaGame.Entities.Players
{

    public class Player : IGameEntity, ITransformable
    {
        private const int WALK_ANIM_SPRITE_COUNT = 4;
        private const int ATTACK_ANIM_SPRITE_COUNT = 6;
        private const int SPRITE_SIZE = 64;
        private const int ATTACK_SPRITE_WIDTH = 96;

        private const float ATTACK_ANIM_FPS = 20f;

        private readonly RenderingStateMachine _renderStateMachine = new RenderingStateMachine();

        private SoundPool _attackSoundPool;
        private CoolDown _attackCoolDown = new CoolDown(1);
        private Vector2 _position;

        public RectCollider Collider { get; private set; }
        public bool CanWalk
        {
            get
            {
                return _renderStateMachine.CurrentState.Name != nameof(PlayerTextureContainer.Attack);
            }
        }

        public Vector2 Velocity => Collider?.Velocity ?? Vector2.Zero;

        public RenderLayer RenderLayer { get; set; } = RenderLayer.Two;

        public int DrawOrder { get; set; }

        public int UpdateOrder { get; set; }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                Collider?.MoveTo(value);
            }
        }

        public float Speed { get; set; } = 200f;
        public float JumpPower { get; set; } = 150;

        public bool IsFacingRight { get; set; } = true;

        public Player(INinjaGame game, PlayerTextureContainer textureContainer, SoundPool attackSounds)
        {
            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Walk),
                new SpriteAnimation(textureContainer.Walk, WALK_ANIM_SPRITE_COUNT, SPRITE_SIZE, SPRITE_SIZE));

            _renderStateMachine.AddState(nameof(PlayerTextureContainer.WalkArmed),
                new SpriteAnimation(textureContainer.WalkArmed, WALK_ANIM_SPRITE_COUNT, SPRITE_SIZE, SPRITE_SIZE));

            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Idle),
                new SpriteAnimation(textureContainer.Idle, WALK_ANIM_SPRITE_COUNT, SPRITE_SIZE, SPRITE_SIZE));

            _renderStateMachine.AddState(nameof(PlayerTextureContainer.IdleArmed),
                new SpriteAnimation(textureContainer.IdleArmed, WALK_ANIM_SPRITE_COUNT, SPRITE_SIZE, SPRITE_SIZE));

            var attackAnim = new SpriteAnimation(textureContainer.Attack, ATTACK_ANIM_SPRITE_COUNT, ATTACK_SPRITE_WIDTH, SPRITE_SIZE)
            {
                Fps = ATTACK_ANIM_FPS
            };

            _renderStateMachine.AddState(nameof(PlayerTextureContainer.Attack), attackAnim);

            attackAnim.AnimationCompleted += AttackAnim_AnimationCompleted;

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));

            _renderStateMachine.CurrentState?.Animation?.Play();

            _attackSoundPool = attackSounds;

            Collider = game.Services.GetService<IPhysicsManager>().CreateRectCollider(this, new Vector2(20, 64), 60f);

        }

        private void AttackAnim_AnimationCompleted(object sender, AnimationCompletedEventArgs e)
        {
            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _renderStateMachine.Draw(spriteBatch, Position, IsFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
        }

        public void Update(GameTime gameTime)
        {
            if (Collider?.Velocity.Length() > MathUtil.EPSIOLON)
            {

            }

            _renderStateMachine.Update(gameTime);
            _attackCoolDown.Update(gameTime);

            if(Collider != null)
                Position = Collider.Position;
        }

        public void Halt()
        {
            //if(Collider != null)
            //    Collider.Velocity = new Vector2(0, Collider.Velocity.Y);

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Idle));
            _renderStateMachine.CurrentState.Animation.Play();
        }

        public void Move(bool right = true)
        {
            int direction = right ? 1 : -1;

            IsFacingRight = right;

            _renderStateMachine.SetState(nameof(PlayerTextureContainer.Walk));
            _renderStateMachine.CurrentState.Animation.Play();

            if(Collider != null)
            {
                //Collider.Velocity = new Vector2(Speed * direction, Velocity.Y);

                //Collider.ApplyImpulse(new Vector2(direction * Speed, 0));

                float targetVelo = direction * Speed;
                float veloDelta = targetVelo - Velocity.X;

                Collider.ApplyForce(new Vector2(veloDelta * Collider.Mass, 0));
            }


        }

        public bool Attack()
        {
            if (_renderStateMachine.CurrentState?.Name == nameof(PlayerTextureContainer.Attack))
                return false;

            return _attackCoolDown.Do(() =>
            {
                _attackSoundPool.PlayRandom();

                if(Collider != null)
                    Collider.Velocity = new Vector2(0, Collider.Velocity.Y);

                _renderStateMachine.SetState(nameof(PlayerTextureContainer.Attack));
                _renderStateMachine.CurrentState.Animation.Play();
            });
        }

        public bool Jump()
        {
            if (Collider == null)
                return false;

            float impulse = Collider.Mass * JumpPower;
            Collider.ApplyImpulse(-Vector2.UnitY * impulse);

            return true;

        }

        public IGameEntity GetGameEntity() => this;
    }
}
