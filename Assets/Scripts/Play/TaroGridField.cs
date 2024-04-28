using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroGridField : GridField
{
    protected override void SortingObjects()
    {
        switch (transform.childCount)
        {
            case 0:
                break;

            case 1:
                Sorting(Vector3.zero);
                break;

            case 2:
            case 3:
                Sorting(new Vector3(-34 * (transform.childCount - 1), 0, 0));
                break;
            default:
                SortingDivine(new Vector3(-20 * (transform.childCount - 1), 0, 0));
                break;
        }
    }

    protected override void Sorting(Vector3 start)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].localPosition = start;
            start.x += 68;
        }
    }

    private void SortingDivine(Vector3 start)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].localPosition = start;
            start.x += 40;
        }
    }
}
