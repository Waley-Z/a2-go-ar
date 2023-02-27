using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isShopList : MonoBehaviour
{
    public List<GameObject> shopList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in shopList)
        {
            item.SetActive(InventoryManager.Discovered.ContainsKey(item.GetComponent<isSeedButton>().treeType));
        }
    }
}
