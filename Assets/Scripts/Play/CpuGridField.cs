using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuGridField : GridField
{

    public override void GetChilds()
    {
        objects.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            objects.Add(transform.GetChild(i));
        }

        SetGemSize();

        SortingObjects();
    }

    protected override void SortingObjects()
    {
        switch (transform.childCount)
        {
            case 0:
                break;

            case 1:
                Sorting(Vector3.zero);
                break;

            default:
                Sorting(new Vector3(-34 * (transform.childCount - 1), 0, 0));
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

    public void SetGemSize()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
