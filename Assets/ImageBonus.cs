using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
[RequireComponent(typeof(ARTrackedImageManager))]

public class ImageBonus : MonoBehaviour
{
    [SerializeField]
    
    private void OnEnable()
    {
        if (!InventoryManager.isBonusActivated)
            InventoryManager.Currency += 100;
        InventoryManager.isBonusActivated = true;
    }
}
