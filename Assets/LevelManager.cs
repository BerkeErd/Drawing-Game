
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasLoader : MonoBehaviour
{
    public string emptyCanvasSceneName = "EmptyCanvasScene";
    public string drawnCanvasSceneName = "DrawnCanvasScene";

    public void StartGame()
    {
        bool hasPlayedBefore = PlayerPrefs.HasKey("HasPlayedBefore");
        
        if (hasPlayedBefore)
        {
            SceneManager.LoadScene(drawnCanvasSceneName);
        }
        else 
        {
            SceneManager.LoadScene(emptyCanvasSceneName);
            PlayerPrefs.SetInt("HasPlayedBefore", 1);
        }
    }
}
