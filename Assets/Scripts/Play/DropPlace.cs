using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropPlace :MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!Task()) return;

        GameManager.dragObject.transform.SetParent(transform);

        AddListData();

        StartCoroutine(GameManager.Instance.PlayManager.CpuTurnStart());
    }

    protected abstract bool Task();

    protected abstract void AddListData();
}
