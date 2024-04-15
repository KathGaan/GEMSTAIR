using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteField : DropPlace
{
    protected override bool Task()
    {
        if (GameManager.dragObject.GetComponent<DragObject>().Info.num == 15)
        {
            GameManager.Instance.UsedTaro = true;

            if (GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.None || GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.White)
            {
                GameManager.Instance.PlayManager.GetTaro(GameManager.dragObject.GetComponent<DragObject>().Info, CardColor.White);
                return false;
            }
            return false;
        }

        if (GameManager.dragObject.GetComponent<DragObject>().Info.color != CardColor.White)
            return false;

        if (transform.childCount > 0 && GameManager.Instance.CurrentLevelData.WhiteField[transform.childCount - 1].num >= GameManager.dragObject.GetComponent<DragObject>().Info.num)
            return false;

        return true;
    }
    protected override void AddListData()
    {
        GameManager.Instance.CurrentLevelData.WhiteField.Add(GameManager.dragObject.GetComponent<DragObject>().Info);
        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            if (GameManager.Instance.CurrentLevelData.PlayerCards[i].color == GameManager.dragObject.GetComponent<DragObject>().Info.color)
                if (GameManager.Instance.CurrentLevelData.PlayerCards[i].num == GameManager.dragObject.GetComponent<DragObject>().Info.num)
                {
                    GameManager.Instance.CurrentLevelData.PlayerCards.RemoveAt(i);
                    break;
                }
        }
        GameManager.dragObject.GetComponent<DragObject>().enabled = false;
    }

}
