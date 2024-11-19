using UnityEngine;
using UnityEngine.EventSystems;

public class FoodLogic : MonoBehaviour, IDropHandler
{

    public PetStatusManager petStatusManager;
    public PlayerStatusManager playerStatusManager;
    private ItemDragnDrop Food;

    public void OnDrop(PointerEventData eventData)
    {
        
        GameObject droppedObject = eventData.pointerDrag;
        if (playerStatusManager.catFoodCount == 0) {

            eventData.pointerDrag = null;
            return; }
        if (droppedObject.CompareTag("Food"))
         {
            GameManager.Instance.audioPlay.PlayOneShot(GameManager.Instance.catFood);

            playerStatusManager.FeedPet();


        }
    }
}
