using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class isSeedButton : MonoBehaviour
{
    public Tree.TreeType treeType;
    public TextMeshProUGUI costText;
    float cost;

    private void Start()
    {
        cost = InventoryManager.Instance.getTreeConfig(treeType).cost;
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
