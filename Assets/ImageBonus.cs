using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
[RequireComponent(typeof(ARTrackedImageManager))]

public class ImageBonus : MonoBehaviour
{
    [SerializeField]
    
    private void Awake()
    {
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
        if (!InventoryManager.isBonusActivated)
            InventoryManager.Currency += 10;
        InventoryManager.isBonusActivated = true;
    }
}
