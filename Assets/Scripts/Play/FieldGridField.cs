using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldGridField : GridField
{
    protected override void SortingObjects()
    {
        Sorting(new Vector3(-782, 0, 0));
    }

    protected override void Sorting(Vector3 start)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].localPosition = start;
            start.x += 136;
        }
    }

    public void SetGemSize()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].localScale = new Vector3(1, 1, 1);
        }
    }
}
