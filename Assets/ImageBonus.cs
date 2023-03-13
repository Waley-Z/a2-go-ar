using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
[RequireComponent(typeof(ARTrackedImageManager))]

public class ImageBonus : MonoBehaviour
{
    [SerializeField]
    private bool isBonusActivated = false;
    private void Awake()
    {
        isBonusActivated = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if (!isBonusActivated)
            InventoryManager.Currency += 10;
        isBonusActivated = true;
    }
}
