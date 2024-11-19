using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // Movement speed
    private Animator animator;     // Animator component (if you want to add animation back in)
    private Vector3 targetPosition; // The target position the cat is moving to
    private bool isWalking;        // Whether the cat is currently walking

    public GameObject shower; // shower gameobject/item
    public TextMeshProUGUI meow;
    private RectTransform textRectTransform;
    private RectTransform showerForm;
    public ParticleSystem water;
    public float minX = -2f;
    public float maxX = 2f;
    private Vector3 runAway;
    void Start()
    {
        animator = GetComponent<Animator>(); // get animator
        GameManager.Instance.doneMoving = animator;
        targetPosition = transform.position;  // Initially, target is where the cat is
        GameManager.Instance.isPet = false;
        if (shower)
            showerForm = shower.GetComponent<RectTransform>();
        //runAwayDirection = Vector3.right
        runAway = Vector3.zero;
        //meow text contains rectTransform
        textRectTransform = meow.GetComponent<RectTransform>();
    }
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GameManager.Instance.startWater == true)
        {
            //isWalking = true;

            float step = moveSpeed * Time.deltaTime;
            if (shower)
            {
                if (transform.position.x < maxX && transform.position.x > minX)
                {
                    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[water.particleCount];
                    int particleCount = water.GetParticles(particles);

                    if (particleCount > 0)
                    {
                        //UnityEngine.Debug.Log("X position: " + transform.position.x);
                        
                        if (runAway == Vector3.zero)
                        {
                            // Calculate the average position of active particles
                            Vector3 averagePosition = Vector3.zero;
                            for (int i = 0; i < particleCount; i++)
                            {
                                averagePosition += particles[i].position;
                            }
                            averagePosition /= particleCount;

                            // Calculate direction away from the average position of particles
                            runAway = new Vector3((transform.position.x - averagePosition.x), 0, 0).normalized;

                            // Move the object in the calculated direction

                            animator.SetBool("isWalking", false);

                        }
                        else
                        {
                            animator.SetBool("isWalking", true);
                        }
                        
                        transform.position += runAway * step * 2;
                        
                        transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y,
                    transform.position.z
                                        );
                        

                        // Flip the sprite based on direction
                        FlipSprite(runAway.x);
                    }
                    else
                    {
                        animator.SetBool("isWalking", false);
                        runAway = Vector3.zero;
                        isWalking = false;
                    }

                }
                else
                {
                    animator.SetBool("isWalking", false);
                    isWalking = false;
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else if (!GameManager.Instance.isPet && mousePosition.y < 1)
        {
            animator.SetBool("isWalking", false);
            // Check if any mouse button is clicked
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                // Convert mouse position to world position
                // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Ensure the z-coordinate remains the same for 2D movement

                targetPosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);  // Set the target position to the mouse position


                // Flip the cat sprite based on the target position
                if (targetPosition.x < transform.position.x)  // If the mouse is to the left
                {
                    float xset = -1 * transform.localScale.x; // storing the current size, but putting it in a variable
                    if (xset > transform.localScale.x)
                    {
                        xset *= -1;// used to prevent flipping mid movement
                    }
                    transform.localScale = new Vector3(xset, transform.localScale.y, transform.localScale.z); // Flip the spriteF
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

                isWalking = true;  // Start walking
            }
            // Move the cat towards the target position if it's walking
            if (isWalking)
            {

                animator.SetBool("isWalking", true);
                float step = moveSpeed * Time.deltaTime;
                // Move the cat towards the target position
                // Vector3 position = transform.position;
                //position.x += speed * Time.deltaTime; // Change 'speed' to control how fast it moves
                //transform.position = position;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                //uiElement.anchoredPosition = canvasPosition;

                // Stop walking if the cat is close to the target
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isWalking = false;  // Stop walking

                    // Uncomment if using animation
                    animator.SetBool("isWalking", false);
                }
            }
        }

            // Get Screen position of text and moves it
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            textRectTransform.position = new Vector3(screenPosition.x, textRectTransform.position.y, textRectTransform.position.z);
        }
    void FlipSprite(float direction)
    {
        if (((transform.localScale.x < 0) && (direction > 0)) || ((transform.localScale.x > 0) && (direction < 0)))
        {
            float xset = -1 * transform.localScale.x;
            transform.localScale = new Vector3(xset, transform.localScale.y, transform.localScale.z);
        }
    }
}
