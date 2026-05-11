using UnityEngine;

public class Dog : MonoBehaviour
{
    public float speed; // Speed of the saw's movement
    public float moveTime; // Time to move in one direction before switching

    private bool movingRight = true; // Direction of movement
    private float timer = 0f; // Timer to track movement time


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // Face right
        }
        else
        {
            transform.Translate((-1) * Vector2.left * speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // Face left
        }

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            movingRight = !movingRight; // Switch direction
            timer = 0f; // Reset timer
        }
    }
}
