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
        GameManager.Instance.audioPlay.PlayOneShot(GameManager.Instance.playTime);
        petStatusManager.DisplayReaction("Click and drag on me to me pet!");
        GameManager.Instance.canPet = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        GameManager.Instance.audioPlay.PlayOneShot(GameManager.Instance.happyCat);
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        GameManager.Instance.isPet = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPet) return;
        GameManager.Instance.audioPlay.PlayOneShot(GameManager.Instance.thankMeow);
        GameManager.Instance.isPet = false;
        GameManager.Instance.canPet = false;
    }
}
