using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using TMPro;

//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
public class PettingFunction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    public PetStatusManager petStatusManager;
    private void Awake()
    {
        GameManager.Instance.canPet = false;
    }
    public void ablePet()
    {
        GameManager.Instance.canPet = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        UnityEngine.Debug.Log("petting up");
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        petStatusManager.PetCat(.1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        GameManager.Instance.isPet = false;
        UnityEngine.Debug.Log("petting up");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        GameManager.Instance.isPet = true;
        UnityEngine.Debug.Log("Starting");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        GameManager.Instance.isPet = false;
        UnityEngine.Debug.Log("Starting");
        GameManager.Instance.canPet = false;
    }
}
