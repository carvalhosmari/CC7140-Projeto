using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score = 0; // Player's score
    public static GameController instance; // Singleton instance of the GameController
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this; // Set the singleton instance to this GameController
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString(); // Update the score text with the current score
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true); // Activate the game over panel
    }

    public void RestartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Reload the specified scene to restart the game
    }
}
