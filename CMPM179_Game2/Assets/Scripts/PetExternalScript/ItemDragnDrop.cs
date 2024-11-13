using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

//Multiple Handlers needed to allow Unity style dragging
public class ItemDragnDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public PlayerStatusManager playerStatusManager;
    private Vector3 startPos;
    private bool isDraggable;//drag toggle

    private void Awake()
    {
        isDraggable = true;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();//used to block raycasts during the drag.  Needed if prevent blocking pointer events
        startPos = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;
        UnityEngine.Debug.Log("starting");
        if (gameObject.CompareTag("Shower")) GameManager.Instance.startWater = true;

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
        startPos = transform.position;
    }
    public void setDraggable(bool draggable)
    {
        isDraggable = draggable;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;
        UnityEngine.Debug.Log("dragging");
       
        //We set the globalMousePosition using the cursor location
        //followed by converting it into the world coordinates.
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform, eventData.position,
            eventData.pressEventCamera, out globalMousePos))
        {
            rectTransform.position = globalMousePos;
        }
        UnityEngine.Debug.Log(globalMousePos);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // transform.position = startPos;
        // {
        //    transform.position = startPos;
        //    return;
        //}
        if (gameObject.CompareTag("Shower")) GameManager.Instance.startWater = false;
        UnityEngine.Debug.Log("ending");
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
        transform.position = startPos;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            //run this if for food to tell the player
            if (gameObject.CompareTag("Food"))
            {
                playerStatusManager.FeedPet();
            }
            return;
        }
        if (gameObject.CompareTag("Shower")) GameManager.Instance.startWater = true;
        GameManager.Instance.isPet = true;
        UnityEngine.Debug.Log("pointer");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDraggable) return;
        if (gameObject.CompareTag("Shower")) GameManager.Instance.startWater = false;
        GameManager.Instance.isPet = false;
        UnityEngine.Debug.Log("Starting");
    }
}

