using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Animator anim;
    public int id;
    internal int targetSlot;
    public Image color;
    public Text number;

    internal bool onDrag = false;
    internal Image image;

    bool reverse = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public void Toogle()
    {
        if (reverse)
            ShowFront();
        else
            ShowBack();
    }

    public void ShowFront()
    {
        if (!reverse) return;
        reverse = false;
        if (gameObject.activeInHierarchy)
            anim.SetTrigger("playFront");

    }
    public void ShowBack()
    {
        if (reverse) return;
        reverse = true;
        if (gameObject.activeInHierarchy)
            anim.SetTrigger("playBack");

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onDrag = true;
        OkeyController.instance?.OnCardDragBegin();
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onDrag = false;
        OkeyController.instance?.OnCardDragEnd();

    }

    private bool isPointerDown = false;
    private bool longPressTriggered = false;
    private float timePressStarted;

    public float durationThreshold = 1.0f;

    private void Update()
    {
        if (isPointerDown && !longPressTriggered)
        {
            if (Time.time - timePressStarted > durationThreshold)
            {
                longPressTriggered = true;
                Toogle();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        timePressStarted = Time.time;
        isPointerDown = true;
        longPressTriggered = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerDown = false;
    }
}
