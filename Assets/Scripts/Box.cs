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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>(); // Get the Animator component attached to the box
        wall.SetActive(true);
        collected.SetActive(false); // Ensure the collected box effect is hidden at the start
    }

    // Update is called once per frame
    void Update()
    {   
        if (health <= 0) 
        {
            spriteRenderer.enabled = false; // Hide the box's sprite when its health reaches 0
            collider.enabled = false; // Disable the box's collider when its health reaches 0
            collected.SetActive(true); // Show the collected box effect
            Destroy(gameObject, .5f); // Destroy the box when its health reaches 0
            wall.SetActive(false); // Deactivate the wall when the box is destroyed 
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

        }
    }
}
