using Microsoft.Xna.Framework;

namespace Yetibyte.NinjaGame.SceneManagement
{
    public interface ISceneDelegate
    {
        void OnInitialize();

        void OnUpdate(GameTime gameTime);

        void OnUnload();
    }
}
