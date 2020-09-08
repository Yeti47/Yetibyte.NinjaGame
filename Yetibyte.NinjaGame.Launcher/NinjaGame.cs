using Accessibility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Yetibyte.NinjaGame.Audio;
using Yetibyte.NinjaGame.Core;
using Yetibyte.NinjaGame.Entities;
using Yetibyte.NinjaGame.Entities.Players;
using Yetibyte.NinjaGame.Graphics;
using Yetibyte.NinjaGame.Physics;
using Yetibyte.NinjaGame.SceneManagement;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.Launcher
{
    public class NinjaGame : Game, INinjaGame
    {

        Vector2 rayStart = new Vector2(400, 300);
        Vector2 rayEnd = new Vector2(500, 800);

        private Texture2D _debugTexture;
        SpriteBatch _debugSpriteBatch;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EntityManager _entityManager;
        private Player _player;
        private PlayerController _playerController;

        private RectCollider _testCollider;
        private RectCollider _testGround;
        private SceneManager _sceneManager;

        private PolygonCollider _testPolygonCollider;

        public NinjaGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var physicsManager = new PhysicsManager(new Vector2(0, 200), 32, new Vector2(-10000, -10000), new Vector2(10000, 10000));

            Services.AddService<IPhysicsManager>(physicsManager);

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

            var testObj = new TestObject { Position = new Vector2(90, 30) };
            var testGroundObj = new TestObject { Position = new Vector2(10, 300) };
            var testPolygonObj = new TestObject { Position = new Vector2(200, 400) };
            Vector2[] testPolyVerts = new Vector2[] {new Vector2(40, 0), new Vector2(200, -50), new Vector2(200, 180), new Vector2(0, 180) };
            //Vector2[] testPolyVerts = new Vector2[] { new Vector2(0, 0), new Vector2(100, 0), new Vector2(100, 100), new Vector2(0, 100) };

            //testPolyVerts = testPolyVerts.Reverse().ToArray();

            //_testCollider = Services.GetService<IPhysicsManager>().CreateRectCollider(testObj, new Vector2(20, 20), 10);
            //_testGround = Services.GetService<IPhysicsManager>().CreateRectCollider(testGroundObj, new Vector2(400, 60), false);

            //_testPolygonCollider = Services.GetService<IPhysicsManager>()
            //    .CreatePolygonCollider(testPolygonObj, testPolyVerts, 0);

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _entityManager = new EntityManager(GraphicsDevice);

            _sceneManager = new SceneManager(Services, Content, _entityManager);

            Services.AddService<ISceneManager>(_sceneManager);


            // TODO: use this.Content to load your game content here
            Texture2D idleTexture = Content.Load<Texture2D>("Textures/main-hero_idle-unarmed");
            Texture2D idleArmedTexture = Content.Load<Texture2D>("Textures/main-hero_idle-armed");
            Texture2D walkTexture = Content.Load<Texture2D>("Textures/main-hero_walk-unarmed");
            Texture2D walkArmedTexture = Content.Load<Texture2D>("Textures/main-hero_walk-armed");
            Texture2D attackTexture = Content.Load<Texture2D>("Textures/main-hero_attack-sword");

            PlayerTextureContainer container = new PlayerTextureContainer
            {
                Idle = idleTexture,
                IdleArmed = idleArmedTexture,
                Walk = walkTexture,
                WalkArmed = walkArmedTexture,
                Attack = attackTexture
            };

            _player = new Player(this, container, LoadPlayerAttackSounds())
            {
                Position = new Vector2(150, 100)
            };
            _playerController = new PlayerController(_player);

            _debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            _debugTexture.SetData(new[] { Color.White });

            _debugSpriteBatch = new SpriteBatch(this.GraphicsDevice);

            //Services.GetService<IPhysicsManager>().EnableDebugDraw(_spriteBatch, _debugTexture);


            TileSet testTileSet = Content.Load<TileSet>("Test");
            testTileSet.LoadTexture(Content);
            
            TileMap testMap = Content.Load<TileMap>("TileMaps/Ninja_TestMap");
            testMap.LoadTileSets(Content);

            SceneDefinition testSceneDef = new SceneDefinition
            {
                Name = "TestScene",
                SceneId = "TestScene",
                TileMapPath = "TileMaps/Ninja_TestMap"
            };

            _sceneManager.AddSceneDefinition(testSceneDef);
            _sceneManager.LoadScene("TestScene");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            _player.Update(gameTime);
            _playerController.Update(gameTime);

            //_testCollider.Update(gameTime);
            //_testGround.Update(gameTime);

            _entityManager.Update(gameTime);

            _sceneManager?.CurrentScene?.Update(gameTime);

            Services.GetService<IPhysicsManager>().Update(gameTime);

            var raycastResult = Services.GetService<IPhysicsManager>().RayCastSingle(rayStart, rayEnd);
            //var raycastResult = Services.GetService<IPhysicsManager>().RayCast(rayStart, rayEnd, 5);

            if (raycastResult.HasHit)
            {

                rayEnd = raycastResult.HitPoint;

            }
            else
            {
                rayEnd = new Vector2(500, 800);
            }


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();

            _player.Draw(_spriteBatch, gameTime);

            _entityManager.Draw(gameTime);

            //_testCollider.DebugDraw(_spriteBatch, _debugTexture, Color.Red);
            //_testGround.DebugDraw(_spriteBatch, _debugTexture, Color.Green);
            //_testPolygonCollider.DebugDraw(_spriteBatch, _debugTexture, Color.Magenta);

            _sceneManager?.CurrentScene?.Draw(gameTime);




            _spriteBatch.DrawLine(_debugTexture, rayStart, rayEnd, Color.Purple, 3);


            _spriteBatch.End();
        }

        private SoundPool LoadPlayerAttackSounds()
        {
            SoundPool soundPool = new SoundPool();

            for(int i = 1; i <= 10; i++)
            {
                soundPool.Add(Content.Load<SoundEffect>($"Sfx/fighter_1_attack_{i}"));
            }

            return soundPool;
        }

    }
}
