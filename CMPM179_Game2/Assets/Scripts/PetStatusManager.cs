using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetStatusManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthBar;
    public Slider hungerBar;
    public Slider happinessBar;

    // Temp text reactions for when buttons are pressed
    public TextMeshProUGUI reactionText;

    // Initial levels for the pet's health, hunger, and happiness
    [Header("Status Levels")]
    public float health = 100f;
    public float hunger = 100f;
    public float happiness = 100f;

    // Rates at which health, hunger, and happiness decrease over time
    [Header(" Temp Decay Amount")]
    public float decayAmount;

    // Time interval in seconds for each decay tick
    [Header("Temp Decay Interval")]
    public float decayInterval;
    private float decayTimer;

    void Start()
    {
        decayTimer = decayInterval;
    }

    void Update()
    {
        UpdateStatusBars();
        DecayStatuses();
        CheckForJumpScare();
    }

    // Sets the slider values
    void UpdateStatusBars()
    {
        healthBar.value = health;
        hungerBar.value = hunger;
        happinessBar.value = happiness;
    }

    // Temp decay for the stats, reduces each bar every couple seconds in incremnts of 10
    void DecayStatuses()
    {
        decayTimer -= Time.deltaTime;

        if (decayTimer <= 0)
        {
            health -= decayAmount;
            hunger -= decayAmount;
            happiness -= decayAmount;

            decayTimer = decayInterval;
        }

        health = Mathf.Clamp(health, 0, 100);
        hunger = Mathf.Clamp(hunger, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);
    }

    // Increasing the hunger, happiness & health which you can edit in unity on the obj manager
    public void FeedPet(float amount, bool hasFood)
    {
        if (hasFood) // Check if the player has food
        {
            hunger += amount;
            hunger = Mathf.Clamp(hunger, 0, 100);
            DisplayReaction("Meow, thank you for feeding me!"); 
        }
        else
        {
            DisplayReaction("Meow, I'm hungry but there's no food!"); // No fooood
        }
    }

    public void PetCat(float amount)
    {
        happiness += amount;
        happiness = Mathf.Clamp(happiness, 0, 100);
        DisplayReaction("Meow happy!"); 
    }

    public void ShowerPet(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100);
        DisplayReaction("Meow healthy!"); 
    }

    // Jumpscare stuff not implemented
    public void CheckForJumpScare()
    {
        if (hunger <= 10 || happiness <= 10 || health <= 10) 
        {
            TriggerJumpScare();
        }
    }

    void TriggerJumpScare()
    {
        // Replace with jumpscare video prob
        Debug.Log("Jump Scare Triggered!");
    }

    // Shows the message in the scene for a duration of time
    void DisplayReaction(string message)
    {
        reactionText.text = message;
        Invoke("ClearReaction", 2f); // Clears the message after 2 seconds
    }

    // Clears the reaction message from the UI
    void ClearReaction()
    {
        reactionText.text = "";
    }
}
