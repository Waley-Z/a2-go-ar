using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class isSeedButton : MonoBehaviour
{
    public Tree.TreeType treeType;
    public float cost = 5.0f;
    public TextMeshProUGUI costText;

    private void Start()
    {
        costText.text = "$ " + cost;
    }
    public void pressed()
    {
        if (InventoryManager.Currency >= cost)
        {
            InventoryManager.Currency -= cost;
            InventoryManager.Seeds[treeType]++;
            Debug.Log("buy one seed");
        }
    }
        
    
}
