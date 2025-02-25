using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Monobehaviour Singleton
public class MonoSingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    { get; protected set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)FindFirstObjectByType(typeof(T));
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}

//Common Singleton
public class SingletonManager<T> where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new T();
            }

            return instance;
        }
    }
}