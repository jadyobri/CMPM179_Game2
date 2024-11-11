using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
public class PettingFunction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public PetStatusManager petStatusManager;

    public void OnBeginDrag(PointerEventData eventData)
    {

        UnityEngine.Debug.Log("petting up");
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        petStatusManager.PetCat(.1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("petting up");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UnityEngine.Debug.Log("Starting");
    }
}
