using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryAcorn : MonoBehaviour
{
    public TextMeshProUGUI quantityText;
    public GameObject acorn_prefab;
    public float acorn_launch_velocity = 1.0f;

    // Update is called once per frame
    void Update()
    {
        quantityText.text = InventoryManager.numAcorns.ToString();
    }

    public void launchAcorn()
    {
        if (GameManager.CurrentScene == GameManager.SceneType.Interaction
            && InventoryManager.numAcorns > 0)
        {
            InventoryManager.numAcorns--;
            Vector3 spawn_position = Camera.main.transform.position;
            GameObject new_acorn = Instantiate(acorn_prefab, spawn_position, Quaternion.identity);
            new_acorn.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * acorn_launch_velocity;
            SoundManager.PlaySound(SoundManager.Sound.LaunchAcorn);
        }
    }
}
