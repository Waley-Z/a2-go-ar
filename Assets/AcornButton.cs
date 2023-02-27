using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AcornButton : MonoBehaviour
{
    public float cost = 5.0f;
    public TextMeshProUGUI costText;
    // Start is called before the first frame update
    void Start()
    {
        
        costText.text = "$ " + cost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void buyAcorn()
    {
        if (InventoryManager.Currency >= cost)
        {
            InventoryManager.Currency -= cost;
            InventoryManager.numAcorns++;
            Debug.Log("buy one acorn");
        }
    }
}
