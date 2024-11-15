using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetStatusManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider healthBar;
    public Slider hungerBar;
    public Slider happinessBar;

    public TextMeshProUGUI reactionText;

    [Header("Status Levels")]
    public float health = 100f;
    public float hunger = 100f;
    public float happiness = 100f;

    [Header("Temp Decay Amount")]
    public float decayAmount;

    [Header("Temp Decay Interval")]
    public float decayInterval;
    private float decayTimer;

    // Reference to PlayerStatusManager
    public PlayerStatusManager playerStatusManager;

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

    void UpdateStatusBars()
    {
        healthBar.value = health;
        hungerBar.value = hunger;
        happinessBar.value = happiness;
    }

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
            DisplayReaction("Meow, I'm hungry but there's no food!");
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

    public void CheckForJumpScare()
    {
        if (hunger <= 10 || happiness <= 10 || health <= 10) 
        {
            TriggerJumpScare();
        }
    }

    void TriggerJumpScare()
    {
        Debug.Log("Jump Scare Triggered!");
    }

    void DisplayReaction(string message)
    {
        reactionText.text = message;
        Invoke("ClearReaction", 2f);
    }

    void ClearReaction()
    {
        reactionText.text = "";
    }
}
