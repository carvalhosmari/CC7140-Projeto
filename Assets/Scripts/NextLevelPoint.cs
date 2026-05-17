using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    public string nextLevelSceneName;

    // Guard to prevent loading the next scene more than once
    // (e.g. if the collider fires multiple times in the same frame).
    private bool hasTriggered = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasTriggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;

            if (GameController.instance != null)
                GameController.instance.LevelCompleted();

            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}
