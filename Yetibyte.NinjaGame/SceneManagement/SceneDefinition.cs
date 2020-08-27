using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.SceneManagement
{
    public class SceneDefinition
    {
        public string SceneId { get; set; }
        public string Name { get; set; }
        public string TileMapPath { get; set; }
        public string SceneDelegateClassName { get; set; }

        public override string ToString()
        {
            return $"ID: {SceneId} | Name: {Name} | TileMap: {TileMapPath} | Delegate: {SceneDelegateClassName}";
        }

    }

}
