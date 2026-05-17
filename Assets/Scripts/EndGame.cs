using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameController.instance == null)
        {
            var go = new GameObject("GameController");
            go.AddComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        GameController.instance.RestartGame();
    }
}
