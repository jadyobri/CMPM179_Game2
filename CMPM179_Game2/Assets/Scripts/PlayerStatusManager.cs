using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.Diagnostics;

public class PlayerStatusManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider mentalHealthBar;
    public Slider energyBar;
    public TextMeshProUGUI moneyText;    
    public TextMeshProUGUI foodText;    

    [Header("Status Levels")]
    public float mentalHealth = 100f;
    public float energy = 100f;
    public float money = 0f;  
    public int foodCount = 0; 

    [Header("Action Effects")]
    public float sleepEffect;  // Mental health increase when sleeping
    public float eatEffect;    // Energy increase when eating
    public float workEffectMentalHealth; // Mental health decrease when working
    public float workEffectEnergy;       // Energy decrease when working
    public float workMoneyEarned;         // Money earned when working
    public int foodPrice;  

    public PetStatusManager petStatusManager; // Figured out how to bring another script over!

    void Start()
    {
        mentalHealthBar.value = mentalHealth;
        energyBar.value = energy;
       // GameManager.Instance.foodAmount = foodCount;
        UpdateMoneyDisplay();
        UpdateFoodDisplay();
    }

    void Update()
    {
        UpdateStatusBars();
    }

    void UpdateStatusBars()
    {
        mentalHealthBar.value = mentalHealth;
        energyBar.value = energy;
    }

    public void Sleep()
    {
        mentalHealth += sleepEffect;
        mentalHealth = Mathf.Clamp(mentalHealth, 0, 100); 
        Debug.Log("Player slept, mental health restored.");
    }

    public void Eat()
    {
        if (foodCount != 0)//if food count is 0, no more food
        {
            foodCount--;
            //foodCount;
            energy += eatEffect;
            energy = Mathf.Clamp(energy, 0, 100);
            Debug.Log("Player ate, energy restored.");
            UpdateFoodDisplay();
        }
        else
        {
            Debug.Log("Buy more food.");
        }
    }

    public void Work()
    {
        mentalHealth += workEffectMentalHealth; // Decreases mental health
        energy += workEffectEnergy;             // Decreases energy
        money += workMoneyEarned;               // Adds money when working

        mentalHealth = Mathf.Clamp(mentalHealth, 0, 100);
        energy = Mathf.Clamp(energy, 0, 100);
        UpdateMoneyDisplay(); 
        Debug.Log("Player worked, mental health and energy decreased, money earned.");
    }

    void UpdateMoneyDisplay()
    {
        moneyText.text = "$" + money.ToString("F0"); // Change f0 to f2 if we want decimals
    }

    void UpdateFoodDisplay()
    {
        foodText.text = "Food: " + foodCount.ToString(); 
    }

    public void BuyFood()
    {
        if (money >= foodPrice)  // Check player has enough money for buy food
        {
            money -= foodPrice; 
            foodCount += 1; 
            UpdateMoneyDisplay(); 
            UpdateFoodDisplay();  
            Debug.Log("Food bought. Current food count: " + foodCount);
        }
        else
        {
            Debug.Log("Not enough money to buy food.");
        }
    }

    public void FeedPet()
    {
        bool hasFood = foodCount > 0; // Check if there is food for pet
        
        if (hasFood)
        {
            foodCount -= 1; 
            // Feed the pet and pass the "hasFood" check
            petStatusManager.FeedPet(10f, hasFood);  
            UpdateFoodDisplay();  
            Debug.Log("Pet fed. Food count: " + foodCount);
        }
        else
        {
            petStatusManager.FeedPet(0f, hasFood); // No food, hunger does not change
            Debug.Log("No food available to feed the pet.");
        }
    }

}
