
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class MainMenu : MonoBehaviour
{


    public GameObject stop;
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayGame()
    {
        GetComponent<MainMenu>().enabled = false;
        SceneManager.LoadScene(1);
    }
}
