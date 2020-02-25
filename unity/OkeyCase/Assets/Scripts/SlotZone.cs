using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotZone : MonoBehaviour, IDropHandler
{
    public int id;
    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard card = eventData.pointerDrag.GetComponent<DraggableCard>();
        if( card!=null)
        {
            OkeyController.instance?.Move(card.id , id , card.targetSlot);
        }
    }

}
