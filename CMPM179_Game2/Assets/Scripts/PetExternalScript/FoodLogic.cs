using UnityEngine;
using UnityEngine.EventSystems;

public class FoodLogic : MonoBehaviour, IDropHandler
{

    public PetStatusManager petStatusManager;
    public PlayerStatusManager playerStatusManager;
    public void OnDrop(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("Dropped");
        //UnityEngine.Debug.Log("Item Dropped on: " + gameObject.tag);
        GameObject droppedObject = eventData.pointerDrag;
        
         if (droppedObject.CompareTag("Food"))
         {
            UnityEngine.Debug.Log("Item Dropped on: " + droppedObject.tag);
            UnityEngine.Debug.Log("Food count: " + playerStatusManager.foodCount);
            if(playerStatusManager.foodCount != 0)
            UnityEngine.Debug.Log("true here");

            playerStatusManager.FeedPet();
         }
    }
}
