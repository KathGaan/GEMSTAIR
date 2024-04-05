using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueField : DropPlace
{
    protected override bool Task()
    {

        if (GameManager.dragObject.GetComponent<DragObject>().Info.color != CardColor.Blue)
            return false;

        if (GameManager.Instance.CurrentLevelData.BlueField[transform.childCount - 1].num >= GameManager.dragObject.GetComponent<DragObject>().Info.num) 
            return false;

        return true;
    }
    protected override void AddListData()
    {
        GameManager.Instance.CurrentLevelData.BlueField.Add(GameManager.dragObject.GetComponent<DragObject>().Info);
        GameManager.Instance.CurrentLevelData.PlayerCards.Remove(GameManager.dragObject.GetComponent<DragObject>().Info);
        GameManager.dragObject.GetComponent<DragObject>().enabled = false;
    }

}
