using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueField : DropPlace
{
    protected override bool Task()
    {
        if(GameManager.dragObject.GetComponent<DragObject>().Info.num == 15)
        {
            if (GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.None || GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.Blue)
            {
                GameManager.Instance.UsedTaro = true;
                GameManager.Instance.PlayManager.GetTaro(GameManager.dragObject.GetComponent<DragObject>().Info, CardColor.Blue);
                return false;
            }
            return false;
        }

        if (GameManager.dragObject.GetComponent<DragObject>().Info.color != CardColor.Blue)
            return false;

        if (transform.childCount > 0 && GameManager.Instance.CurrentLevelData.BlueField[transform.childCount - 1].num >= GameManager.dragObject.GetComponent<DragObject>().Info.num) 
            return false;

        return true;
    }

}
