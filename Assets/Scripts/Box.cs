using UnityEngine;

public class Box : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private BoxCollider2D collider; // Reference to the Collider2D component
    public float jumpForce;
    private Animator animator;
    public bool isUp;
    public int health = 5; // Amount of damage the box will deal to the player
    public GameObject wall; // Reference to the wall GameObject
    public GameObject collected;
    public GameObject arrow; // Reference to the explosion effect GameObject
    public bool hasWall;
    public bool hasArrow;

    // Guard to prevent the destruction block from running more than once.
    private bool isDestroyed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hasWall) {
            wall.SetActive(true); // Ensure the wall is active at the start if it exists
        } else {
            wall.SetActive(false); // Ensure the wall is inactive at the start if it doesn't exist
        }

        if (hasArrow) {
            arrow.SetActive(true); // Ensure the arrow is active at the start if it exists
        } else {
            arrow.SetActive(false); // Ensure the arrow is inactive at the start if it doesn't exist
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>(); // Get the Animator component attached to the box
        collected.SetActive(false); // Ensure the collected box effect is hidden at the start        
    }

    // Update is called once per frame
    void Update()
    {   
        if (health <= 0 && !isDestroyed) 
        {
            if (!hasWall) {
                wall.SetActive(true); // Deactivate the wall when the box is destroyed  
                Debug.Log("Wall activated!");            
            } else {
                wall.SetActive(false); // Activate the wall when the box is destroyed
            }

            if (hasArrow) {
                arrow.SetActive(false); // Deactivate the arrow when the box is destroyed
            } 
            
            isDestroyed = true;
            spriteRenderer.enabled = false; // Hide the box's sprite when its health reaches 0
            collider.enabled = false; // Disable the box's collider when its health reaches 0
            collected.SetActive(true); // Show the collected box effect

            AudioManager.Instance?.Play(AudioManager.Instance.clipExplosion);
            Destroy(gameObject, .5f); // Destroy the box when its health reaches 0                        
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isUp)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            } else 
            {
                 collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
            }

            health--; // Decrease the box's health by 1 when the player collides with it
            animator.SetTrigger("hit"); // Trigger the hit animation when the player collides with the box

            AudioManager.Instance?.Play(AudioManager.Instance.clipHit);
        }
    }
}
