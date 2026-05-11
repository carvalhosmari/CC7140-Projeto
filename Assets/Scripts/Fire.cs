using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D coll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
        coll = GetComponent<CapsuleCollider2D>(); // Get the CapsuleCollider2D component attached to the fire
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Player hit fire!"); // Log a message when the player hits fire
            animator.SetTrigger("hit"); // Trigger the hit animation when the player collides with the fire

            animator.SetBool("fire", true); // Set the burn parameter to true to start the burning animation
            Invoke("AtivarCollider", 0.5f); // Schedule the AtivarCollider method to be called after 0.5 seconds to enable the collider and allow the player to be affected by the fire
        }
    }

    void AtivarCollider() {
        coll.enabled = true; // Enable the collider to allow the player to be affected by the fire
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Player hit fire!"); // Log a message when the player hits fire
            GameController.instance.GameOver(); // Call the GameOver method in the GameController when hitting fire
            Destroy(collider.gameObject); // Destroy the player game object when hitting fire
        }
    }
}
