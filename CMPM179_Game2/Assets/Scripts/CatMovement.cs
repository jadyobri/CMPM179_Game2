using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // Movement speed
    private Animator animator;     // Animator component (if you want to add animation back in)
    private Vector3 targetPosition; // The target position the cat is moving to
    private bool isWalking;        // Whether the cat is currently walking

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;  // Initially, target is where the cat is
    }

    void Update()
    {
        // Check if any mouse button is clicked
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            // Convert mouse position to world position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the z-coordinate remains the same for 2D movement

            targetPosition = mousePosition;  // Set the target position to the mouse position

            Debug.Log("Target Position: " + targetPosition);

            // Flip the cat sprite based on the target position
            if (targetPosition.x < transform.position.x)  // If the mouse is to the left
            {
                float xset = -1 * transform.localScale.x;
                if (xset > transform.localScale.x)
                {
                    xset *= -1;
                }
                transform.localScale = new Vector3(xset, transform.localScale.y, transform.localScale.z); // Flip the sprite
            }
            else if (targetPosition.x > transform.position.x)  // If the mouse is to the right
            {
                float xset = -1 * transform.localScale.x;
                if (xset < transform.localScale.x)
                {
                    xset *= -1;
                }
                transform.localScale = new Vector3(xset, transform.localScale.y, transform.localScale.z); // Restore the original scale
            }

            // Uncomment if using animation
            animator.SetBool("isWalking", true);
            isWalking = true;  // Start walking
        }

        // Move the cat towards the target position if it's walking
        if (isWalking)
        {
            // Calculate the step size for this frame
            float step = moveSpeed * Time.deltaTime;

            // Move the cat towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // Stop walking if the cat is close to the target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isWalking = false;  // Stop walking

                // Uncomment if using animation
                animator.SetBool("isWalking", false);
            }
        }
    }
}