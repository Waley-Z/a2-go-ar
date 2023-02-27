using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryAcorn : MonoBehaviour
{
    public TextMeshProUGUI quantityText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        quantityText.text = InventoryManager.numAcorns.ToString();
    }
}
