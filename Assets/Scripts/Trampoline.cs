using UnityEngine;

public class Trampoline : MonoBehaviour
{

    public float jumpForce;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the trampoline
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            animator.SetTrigger("jump"); // Trigger the bounce animation when the player collides with the trampoline
        }
    }
}
