namespace Yetibyte.NinjaGame.SceneManagement
{
    public interface ISceneManager
    {
        Scene CurrentScene { get; }
        string SceneDefinitionRootFolder { get; set; }

        void AddSceneDefinition(SceneDefinition sceneDefinition);
        Scene LoadScene(string sceneId);
        int LoadSceneDefinitions();
    }
}