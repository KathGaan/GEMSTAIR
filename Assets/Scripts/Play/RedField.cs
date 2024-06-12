using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedField : DropPlace
{
    protected override bool Task()
    {
        if (GameManager.dragObject.GetComponent<DragObject>().Info.num == 15)
        {
            if (GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.None || GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.Red)
            {
                GameManager.Instance.UsedTaro = true;
                GameManager.Instance.PlayManager.GetTaro(GameManager.dragObject.GetComponent<DragObject>().Info, CardColor.Red);
                return false;
            }
            return false;
        }

        if (GameManager.dragObject.GetComponent<DragObject>().Info.color != CardColor.Red && GameManager.dragObject.GetComponent<DragObject>().Info.color != CardColor.None)
            return false;

        if(GameManager.dragObject.GetComponent<DragObject>().Info.color == CardColor.None)
        {
            UniqueGemFunction.PlacedColor = CardColor.Red;
        }


        if (transform.childCount > 0 && GameManager.Instance.CurrentLevelData.RedField[transform.childCount - 1].num >= GameManager.dragObject.GetComponent<DragObject>().Info.num) 
            return false;

        return true;
    }
}
