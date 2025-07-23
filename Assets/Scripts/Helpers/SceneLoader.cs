using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene targetScene;

    public static void LoadScene(Scene scene)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
        }

        targetScene = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() => SceneManager.LoadScene(targetScene.ToString());
}
