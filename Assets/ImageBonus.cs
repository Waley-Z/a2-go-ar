using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
[RequireComponent(typeof(ARTrackedImageManager))]

public class ImageBonus : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += oneTimeBonus;
    }

    private void oneTimeBonus(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trakedImage in eventArgs.added)
        {
            if (!InventoryManager.isBonusActivated)
                InventoryManager.Currency += 100;
            InventoryManager.isBonusActivated = true;
        }
    }
}
