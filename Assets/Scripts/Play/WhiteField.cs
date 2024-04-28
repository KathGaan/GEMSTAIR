using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteField : DropPlace
{
    protected override bool Task()
    {
        if (GameManager.dragObject.GetComponent<DragObject>().Info.num == 15)
        {
            if (GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.None || GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.White)
            {
                GameManager.Instance.UsedTaro = true;
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

}
