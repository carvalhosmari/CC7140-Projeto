using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    float fallingTime = .75f; // Time before the platform starts falling

    private TargetJoint2D targetJoint; // Reference to the TargetJoint2D component
    private BoxCollider2D boxCollider; // Reference to the BoxCollider2D component

    // Guard to prevent multiple Invoke calls from stacking if the player lands more than once.
    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetJoint = GetComponent<TargetJoint2D>(); // Get the TargetJoint2D component attached to this GameObject
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component attached to this GameObject   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isFalling)
        {
            isFalling = true;
            Invoke("Fall", fallingTime); // Schedule the Fall method to be called after fallingTime seconds
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Destroy(gameObject); // Destroy the platform if it enters a trigger with an object on layer 7 (e.g., a hazard or boundary)
        }
    }

    void Fall()
    {
        targetJoint.enabled = false; // Disable the TargetJoint2D to allow the platform to fall
        boxCollider.isTrigger = true; // Enable the BoxCollider2D as a trigger to prevent further collisions
    }
}
