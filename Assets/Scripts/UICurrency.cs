using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICurrency : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.RoundToInt(InventoryManager.Currency).ToString() + " g"; 
    }
}
