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

    // Guard to prevent multiple TakeDamage calls before the scene reloads.
    private bool isDead = false;
    private bool isFlying = false; // Flag to check if the player is flying 
    

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

        rb.linearVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);

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
        if (Input.GetButtonDown("Jump") && !isFlying)
        {
            if (!isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;  // Mark as airborne immediately to prevent instant double-jump
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead) return;

        if (collider.gameObject.CompareTag("Spike") ||
            collider.gameObject.CompareTag("Saw")   ||
            collider.gameObject.CompareTag("Dog"))
        {
            isDead = true;
            AudioManager.Instance?.Play(AudioManager.Instance.clipDamage);
            GameController.instance.TakeDamage();
        }
    }

    /// <summary>Resets physics and animation state after a respawn.</summary>
    public void ResetState()
    {
        rb.linearVelocity = Vector2.zero;
        isJumping = false;
        doubleJump = false;
        animator.SetBool("walk", false);
        animator.SetBool("jump", false);
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

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fan"))
        {
            isFlying = true; // Set flying flag to true when on a flying platform
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fan"))
        {
            isFlying = false; // Set flying flag to false when leaving a flying platform
        }
    }
}

