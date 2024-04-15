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

    [SerializeField] SoundClip soundClip;

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

        StartCoroutine(GameManager.Instance.PlayManager.SetCanPlace());

        GameManager.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        GameManager.dragObject.transform.SetParent(dragNow);

        SoundManager.Instance.SFXPlay(soundClip.Clips[0]);
    }

    public void OnDrag(PointerEventData eventData)
    {
        GameManager.dragObject.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        GameManager.dragObject = null;

        if (transform.parent == dragNow && !GameManager.Instance.UsedTaro)
        {
            transform.SetParent(playerHand);

            SoundManager.Instance.SFXPlay(soundClip.Clips[1]);
        }
        else
        {
            GameManager.Instance.UsedTaro = false;
        }
    }
}
