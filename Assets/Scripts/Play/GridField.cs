using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    protected List<Transform> objects = new List<Transform>();

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return null;

        GetChilds();
    }

    //GetChilds
    public virtual void GetChilds()
    {
        objects.Clear();

        for(int i = 0; i < transform.childCount; i++)
        {
            objects.Add(transform.GetChild(i));
        }

        SortingObjects();
    }


    //SortSystem

    protected virtual void SortingObjects()
    {
        switch (transform.childCount)
        {
            case 0:
                break;

            case 1:
                Sorting(Vector3.zero);
                break;

            default:
                Sorting(new Vector3(-68 * (transform.childCount - 1), 0, 0));
                break;
        }
    }

    protected virtual void Sorting(Vector3 start)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].localPosition = start;
            start.x += 136;
        }
    }
}
