
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasLoader : MonoBehaviour
{
    public void StartGame()
    {
      SceneManager.LoadScene("Drawing_Scene");
    }
}
