using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject draggableObject;
    public Slot slot;
    public bool mustFitInSlot;
    CanvasGroup canvasGroup;
    private Vector3 startPosition;
    public bool inSlot;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        inSlot = mustFitInSlot;
    }
    public void OnDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .8f;
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        inSlot = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (mustFitInSlot)
        {
            if(!inSlot)
                transform.position = startPosition;
            inSlot = true;
        }
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}
