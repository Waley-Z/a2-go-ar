using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager<Derived> : MonoBehaviour
where Derived : Manager<Derived>
{
    public static Derived Instance;

    protected bool ShouldDontDestroyOnLoad = true;

    // every time before OnSceneLoaded
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = (Derived)this;
            if (ShouldDontDestroyOnLoad) { DontDestroyOnLoad(gameObject); }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
