using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragnDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        UnityEngine.Debug.Log("starting");


        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
     }
    public void OnDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("dragging");
        //eventData.pointerDrag.transform.position = this.transform.position;
       
        //We set the globalMousePosition using the cursor location
        //followed by converting it into the world coordinates.
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform, eventData.position,
            eventData.pressEventCamera, out globalMousePos))
        {
            rectTransform.position = globalMousePos;
        }

        
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //Change the position of the object while dragging and dropping within the confines of the canvas object.
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("ending");
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("pointer");
    }
}

