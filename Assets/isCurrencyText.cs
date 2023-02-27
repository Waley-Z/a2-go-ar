using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class isCurrencyText : MonoBehaviour
{
    public TextMeshProUGUI tmpUI;
    // Start is called before the first frame update
    void Start()
    {
        tmpUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpUI.text = "Currency: $ " + InventoryManager.Currency;
    }
}
