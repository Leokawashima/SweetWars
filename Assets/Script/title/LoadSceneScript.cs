using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void Go_TitleScene()
    {
        SceneManager.LoadScene(Name.SceneName_Int(Name.Scene.Title));
    }

    public static void Go_GameScene()
    {
        SceneManager.LoadScene(Name.SceneName_Int(Name.Scene.Game));
    }

    public static void Go_LoadScene()
    {
        SceneManager.LoadScene(Name.SceneName_Int(Name.Scene.Load));
    }

    public static void AppExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
