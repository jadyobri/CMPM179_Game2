using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

//Multiple Handlers needed to allow Unity style dragging
public class ItemDragnDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();//used to block raycasts during the drag.  Needed if prevent blocking pointer events
        startPos = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        UnityEngine.Debug.Log("starting");


        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
        startPos = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
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

        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("ending");
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
        transform.position = startPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("pointer");
    }
}

