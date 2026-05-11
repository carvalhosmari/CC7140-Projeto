using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    public string nextLevelSceneName;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameController.instance != null)
                GameController.instance.LevelCompleted();

            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}
