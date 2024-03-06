using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Draggable droppedObject;

    public delegate void DropEvent(Draggable d);

    public event DropEvent OnDropEvent;

    protected void InvokeDropEvent(Draggable d = null)
    {
        OnDropEvent?.Invoke(d);
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
            InvokeDropEvent(droppedObject);
            return;
        }
        InvokeDropEvent();
    }
}
