using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Player movement speed
    public float jumpForce = 10f; // Force applied when the player jumps
    private Rigidbody2D rb; // Reference to the Rigidbody2D 
    //component
    private Animator animator; // Reference to the Animator component
    public bool isJumping = false; // Flag to check if the player is currently jumping
    public bool doubleJump = false; // Flag to check if the player can perform a double jump
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        // Get input from the horizontal and vertical axes (WASD or arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on the input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player by applying the movement vector multiplied by speed and deltaTime
        transform.position += movement * speed * Time.deltaTime;

        if (moveHorizontal > 0f) {
            animator.SetBool("walk", true); 
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // Face right
        } 
        if (moveHorizontal < 0f) {
            animator.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // Face left
        } 
        if (moveHorizontal == 0f) {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); 
                doubleJump = true; // Allow double jump after the first jump
                animator.SetBool("jump", true); // Set jump animation to true when jumping
            } else 
            {
                if (doubleJump)
                {
                    rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); 
                    doubleJump = false; // Disable double jump after using it
                    animator.SetBool("jump", true); // Set jump animation to true when jumping
                }    
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Spike") {
            Debug.Log("Player hit a spike!"); // Log a message when the player hits a spike
            GameController.instance.GameOver(); // Call the GameOver method in the GameController when hitting a spike
            Destroy(gameObject); // Destroy the player game object when hitting a spike
        }

        if (collider.gameObject.tag == "Saw") {
            GameController.instance.GameOver(); // Call the GameOver method in the GameController when hitting a saw
            Destroy(gameObject); // Destroy the player game object when hitting a saw
        }

        if (collider.gameObject.tag == "Dog") {
            GameController.instance.GameOver(); // Call the GameOver method in the GameController when hitting a dog
            Destroy(gameObject); // Destroy the player game object when hitting a dog
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision logic here (e.g., check for ground to allow jumping)
        if (collision.gameObject.layer == 8)
        {
            // Allow the player to jump again
            isJumping = false;
            animator.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Handle logic for when the player leaves a collision (e.g., stop allowing jumps)
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}

