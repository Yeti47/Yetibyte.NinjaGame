using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Yetibyte.NinjaGame.Entities;
using Yetibyte.NinjaGame.TileMaps;

namespace Yetibyte.NinjaGame.SceneManagement
{
    public class SceneManager : ISceneManager
    {
        private readonly List<SceneDefinition> _sceneDefinitions = new List<SceneDefinition>();
        private EntityManager _entityManager;
        private ContentManager _contentManager;

        public string SceneDefinitionRootFolder { get; set; }

        public Scene CurrentScene { get; private set; }

        public SceneManager(ContentManager content, EntityManager entityManager, string sceneDefRootFolder = "SceneDefinitions")
        {
            SceneDefinitionRootFolder = sceneDefRootFolder;
            _entityManager = entityManager;
            _contentManager = content;
        }

        public void AddSceneDefinition(SceneDefinition sceneDefinition)
        {
            if (sceneDefinition is null)
            {
                throw new ArgumentNullException(nameof(sceneDefinition));
            }

            if (_sceneDefinitions.Any(s => s.SceneId == sceneDefinition.SceneId))
                throw new ArgumentException($"A scene definition with ID {sceneDefinition.SceneId} already exists.");

            _sceneDefinitions.Add(sceneDefinition);

        }

        public int LoadSceneDefinitions()
        {
            string contentRoot = _contentManager.RootDirectory;

            string sceneDefDir = Path.Combine(contentRoot, SceneDefinitionRootFolder);

            var fileNames = Directory.EnumerateFiles(sceneDefDir);

            int count = 0;

            foreach (var fileName in fileNames)
            {
                string fileWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                var sceneDef = _contentManager.Load<SceneDefinition>(fileWithoutExt);

                if (sceneDef != null)
                {
                    _sceneDefinitions.Add(sceneDef);
                    count++;
                }

            }

            return count;

        }

        public Scene LoadScene(string sceneId)
        {
            if (string.IsNullOrWhiteSpace(sceneId))
            {
                throw new ArgumentException("The scene ID must be specified.", nameof(sceneId));
            }

            var sceneDef = _sceneDefinitions.FirstOrDefault(s => s.SceneId == sceneId);

            if (sceneDef == null)
                return null;

            ISceneDelegate sceneDelegate = CreateSceneDelegate(sceneDef.SceneDelegateClassName?.Trim());
            TileMap tileMap = null;

            if (!string.IsNullOrWhiteSpace(sceneDef.TileMapPath?.Trim()))
            {
                tileMap = _contentManager.Load<TileMap>(sceneDef.TileMapPath?.Trim());
                tileMap.LoadTileSets(_contentManager);
            }

            Scene scene = new Scene(sceneDef.SceneId, sceneDef.Name, _entityManager, sceneDelegate, tileMap);

            CurrentScene = scene;

            CurrentScene?.Initialize();

            return scene;

        }

        private ISceneDelegate CreateSceneDelegate(string sceneDelegateClassName)
        {
            if (string.IsNullOrWhiteSpace(sceneDelegateClassName))
                return null;

            ISceneDelegate sceneDelegate = null;

            try
            {
                Type sceneDelType = Type.GetType(sceneDelegateClassName);

                sceneDelegate = Activator.CreateInstance(sceneDelType) as ISceneDelegate;
            }
            catch (Exception ex)
            {
                throw new SceneDelegateException($"Could not create instance of the specified SceneDelegate '{sceneDelegateClassName}'", ex);
            }

            return sceneDelegate;

        }

    }
}
