using UnityEngine;

public class Fish : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private CircleCollider2D circle; // Reference to the Collider2D component 
    public GameObject collected;
    public int scoreValue = 10; // The score value of the fruit

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        collected.SetActive(false); // Ensure the collected effect is hidden at the start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Frida collected a fish!");
        
            spriteRenderer.enabled = false; // Hide the fish's sprite
            circle.enabled = false; // Disable the fish's collider

            collected.SetActive(true); // Show the collected fish effect

            GameController.instance.score += scoreValue; // Increase the player's score by the fish's value

            GameController.instance.UpdateScoreText(); // Update the score text in the UI

            AudioManager.Instance?.Play(AudioManager.Instance.clipCoin);

            Destroy(gameObject, 0.5f); // Destroy the fish after a short delay to allow the effect to play
        }
    }
}
