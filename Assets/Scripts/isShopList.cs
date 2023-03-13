using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isShopList : MonoBehaviour
{
    public List<GameObject> shopList;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in shopList)
        {
            item.GetComponent<Button>().interactable = InventoryManager.Discovered[item.GetComponent<isSeedButton>().treeType];
        }
    }
}
