using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public string nextLevel;
    public void Next()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
