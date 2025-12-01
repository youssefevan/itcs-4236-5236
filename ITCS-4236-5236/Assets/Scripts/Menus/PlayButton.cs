using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Play Button Clicked");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
