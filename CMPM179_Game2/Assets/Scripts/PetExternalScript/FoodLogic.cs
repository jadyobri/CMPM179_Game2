using UnityEngine;
using UnityEngine.EventSystems;

public class FoodLogic : MonoBehaviour, IDropHandler
{

    public PetStatusManager petStatusManager;
    public PlayerStatusManager playerStatusManager;
    private ItemDragnDrop Food;

    public void OnDrop(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("Dropped");
        //UnityEngine.Debug.Log("Item Dropped on: " + gameObject.tag);
        
        GameObject droppedObject = eventData.pointerDrag;
        if (playerStatusManager.foodCount == 0) return;
        if (droppedObject.CompareTag("Food"))
         {
            

            playerStatusManager.FeedPet();
            //food = droppedObject.GetComponent<GameObject>();
           // if (playerStatusManager.foodCount == 0)
             //   droppedObject.SetActive(false);


        }
    }
}
