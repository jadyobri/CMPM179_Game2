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

    public TextMeshProUGUI playerFoodText; // Display for player food count
    public TextMeshProUGUI catFoodText;    // Display for cat food count

    public TextMeshProUGUI shopCatFoodText; // Display
 

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

    [Header("Food and Shop")]
    public int playerFoodCount = 0;  // Food for player
    public int catFoodCount = 0;     // Food for cat
    public int playerFoodPrice = 10; // Price for player food
    public int catFoodPrice = 20;    // Price for cat food
    public GameObject Food;
    private Vector3 startPos;
    public ItemDragnDrop foodDrop;


    public PetStatusManager petStatusManager; // Figured out how to bring another script over!

    void Start()
    {
        mentalHealthBar.value = mentalHealth;
        energyBar.value = energy;
        // GameManager.Instance.foodAmount = foodCount;
        if (Food)
        {
            foodDrop = Food.GetComponent<ItemDragnDrop>();
            startPos = Food.transform.position;
        }
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
        if (playerFoodCount > 0)  // Check if player has food
        {
            playerFoodCount--;  // Decrease player food count
            energy += eatEffect;
            energy = Mathf.Clamp(energy, 0, 100);
            Debug.Log("Player ate, energy restored.");
            
            UpdatePlayerFoodDisplay();  // Update player food text to show the reduced count
        }
        else
        {
            Debug.Log("Buy more food.");
        }
    }


    public void Work()
    {
        if (energy >= -(workEffectEnergy) && mentalHealth >= -(workEffectMentalHealth))
        {
            mentalHealth += workEffectMentalHealth; // Decreases mental health
            energy += workEffectEnergy;             // Decreases energy
            money += workMoneyEarned;               // Adds money when working

            mentalHealth = Mathf.Clamp(mentalHealth, 0, 100);
            energy = Mathf.Clamp(energy, 0, 100);
            UpdateMoneyDisplay();
            Debug.Log("Player worked, mental health and energy decreased, money earned.");
            Debug.Log(energy);
            Debug.Log(workEffectEnergy);
        }
        else
        {
            Debug.Log("Player too tired. No money earned");
        }
    }

    void UpdateMoneyDisplay()
    {
        moneyText.text = "$" + money.ToString("F0"); // Change f0 to f2 if we want decimals
    }

    void UpdateFoodDisplay()
    {
        foodText.text = "Food: " + foodCount.ToString();
        if (Food)
        {
            //foodCount++;
            // Food.gameObject.SetActive(foodCount > 0);
            //foodDrop.OnEndDrag();
           // Food.transform.position = startPos;
            foodDrop.setDraggable(foodCount>0);
            
            
        }

        //}
    }
        public void BuyFood()
    {
        if (money >= foodPrice)  // Check player has enough money for buy food
        {
            money -= foodPrice; 
            foodCount += 1;
            if (Food)
            {
                UnityEngine.Debug.Log("Food Found here");
                
                Food.SetActive(true);
            }
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
        if (catFoodCount > 0)  // Check if there is cat food available to feed the pet
        {
            catFoodCount -= 1;  // Decrease cat food count by 1
            petStatusManager.FeedPet(10f, true);  // Feed the pet, passing hasFood as true

            // Update both shop and pet UI displays
            UpdateCatFoodDisplay();  // Updates both cat food text fields
            Debug.Log("Pet fed. Cat food count: " + catFoodCount);
        }
        else
        {
            Debug.Log("No cat food available to feed the pet.");
            petStatusManager.FeedPet(0f, false);  // Notify pet that there's no food
        }
    }


    // Method to buy player food
    public void BuyPlayerFood()
    {
        if (money >= playerFoodPrice)
        {
            money -= playerFoodPrice;   // Deduct money
            playerFoodCount += 1;       // Increase player food count
            UpdateMoneyDisplay();       // Update UI for money
            UpdatePlayerFoodDisplay();  // Update UI for player food
            Debug.Log("Player food bought. Player food count: " + playerFoodCount);
        }
        else
        {
            Debug.Log("Not enough money to buy player food.");
        }
    }

    // Method to buy cat food
    public void BuyCatFood()
    {
        if (money >= catFoodPrice)
        {
            money -= catFoodPrice;    // Deduct money
            catFoodCount += 1;        // Increase cat food count

            // Update both the shop and pet UI displays
            UpdateMoneyDisplay();     // Update money UI
            UpdateCatFoodDisplay();   // Update both UI displays for cat food
            Debug.Log("Cat food bought. Cat food count: " + catFoodCount);
        }
        else
        {
            Debug.Log("Not enough money to buy cat food.");
        }
    }

    public void UpdatePlayerFoodDisplay()
    {
        playerFoodText.text = "# " + playerFoodCount.ToString();
    }

    public void UpdateCatFoodDisplay()
    {
        // Update both the pet food text and the shop food text
        catFoodText.text = "# " + catFoodCount.ToString();   // Pet UI food count display
        shopCatFoodText.text = "food: " + catFoodCount.ToString();  // Shop UI food count display
    }



}
