using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public Fighter player;
    public Fighter opponent;

    public Button retryBtn;

    public Button continueBtn;

    public string nextScene;


    void Awake()
    {
        retryBtn.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player.currentHealth <= 0)
        {
            retryBtn.gameObject.SetActive(true);
        }
        else if (opponent.currentHealth <= 0)
        {
            continueBtn.gameObject.SetActive(true);
        }
    }

    public void Next()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

