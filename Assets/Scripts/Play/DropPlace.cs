using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropPlace :MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!Task())
            return;

        GameManager.dragObject.transform.SetParent(transform);

        transform.GetComponent<GridField>().GetChilds();

        GameManager.Instance.PlayManager.DropSound();

        AddListData();

        StartCoroutine(GameManager.Instance.PlayManager.CpuTurnStart());
    }

    private void TaskAb()
    {
        if (GameManager.dragObject.GetComponent<DragObject>().Info.ab == true)
        {
            GameManager.Instance.PlayManager.TaroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.PlayerUse, GameManager.dragObject.GetComponent<DragObject>());
        }
    }

    protected abstract bool Task();

    private void AddListData()
    {
        if (GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.None)
        {
            GameManager.Instance.PlayManager.GetColorParent(UniqueGemFunction.PlacedColor).Add(GameManager.dragObject.GetComponent<DragObject>().Info);
        }
        else
        {
            GameManager.Instance.PlayManager.GetColorParent(GameManager.dragObject.GetComponent<DragObject>().Info.color).Add(GameManager.dragObject.GetComponent<DragObject>().Info);
        }

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            if (GameManager.Instance.CurrentLevelData.PlayerCards[i].color == GameManager.dragObject.GetComponent<DragObject>().Info.color)
            {
                if (GameManager.Instance.CurrentLevelData.PlayerCards[i].num == GameManager.dragObject.GetComponent<DragObject>().Info.num)
                {
                    if(GameManager.Instance.CurrentLevelData.PlayerCards[i].color == CardColor.None)
                    {
                        if (GameManager.Instance.CurrentLevelData.PlayerCards[i].abNum == GameManager.dragObject.GetComponent<DragObject>().Info.abNum)
                        {
                            GameManager.Instance.PlayManager.UniqueGemFunction.ActiveFunction(UniqueGemFunction.LoadAt.PlayerUse, GameManager.dragObject.GetComponent<DragObject>());
                            
                            GameManager.Instance.CurrentLevelData.PlayerCards.RemoveAt(i);
                            
                            break;
                        }
                    }
                    else if (GameManager.Instance.CurrentLevelData.PlayerCards[i].ab == GameManager.dragObject.GetComponent<DragObject>().Info.ab && GameManager.dragObject.GetComponent<DragObject>().Info.ab == true)
                    {
                        if (GameManager.Instance.CurrentLevelData.PlayerCards[i].abNum == GameManager.dragObject.GetComponent<DragObject>().Info.abNum)
                        {
                            TaskAb();
                            GameManager.Instance.CurrentLevelData.PlayerCards.RemoveAt(i);
                            break;
                        }
                    }
                    else
                    {
                        GameManager.Instance.CurrentLevelData.PlayerCards.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        GameManager.dragObject.GetComponent<DragObject>().enabled = false;
        GameManager.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
