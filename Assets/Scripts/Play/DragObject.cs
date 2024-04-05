using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour , IDragHandler , IEndDragHandler, IBeginDragHandler
{
    private Transform dragNow;

    private Transform playerHand;

    [SerializeField] Card info;

    public Card Info
    {
        set { info = value; }
        get { return info; }
    }
    
    //Start
    private void Start()
    {
        playerHand = transform.parent;

        if (GameManager.Instance.PlayManager.PlayerHand != playerHand && GameManager.Instance.PlayManager.TaroHand != playerHand)
        {
            gameObject.GetComponent<DragObject>().enabled = false;
        }

        dragNow = GameManager.Instance.PlayManager.DragNow;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameManager.dragObject = gameObject;

        GameManager.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        GameManager.dragObject.transform.SetParent(dragNow);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameManager.dragObject.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        GameManager.dragObject = null;

        if (transform.parent == dragNow)
        {
            transform.SetParent(playerHand);
        }
    }
}
