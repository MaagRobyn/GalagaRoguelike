using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Draggable droppedObject;

    public delegate void DropEvent();

    public event DropEvent OnDropEvent;

    protected void InvokeDropEvent()
    {
        OnDropEvent?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.position = transform.position;
        if (eventData.pointerDrag.TryGetComponent(out droppedObject))
        {
            if (droppedObject.slot != null)
                droppedObject.slot.droppedObject = null;
            droppedObject.inSlot = true;
            droppedObject.slot = this;
        }
    }
}
