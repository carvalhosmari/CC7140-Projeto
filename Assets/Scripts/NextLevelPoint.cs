using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    public string nextLevelSceneName; // Name of the next level scene to load when the player reaches this point

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            // Load the next level when the player collides with this object
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}
