using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornSpawner : MonoBehaviour
{
    public GameObject acorn_prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            launchAcorn();
        }
    }

    public float acorn_launch_velocity = 1.0f;

    public void launchAcorn()
    {
        if (InventoryManager.numAcorns > 0)
        {
            InventoryManager.numAcorns--;
            Vector3 spawn_position = Camera.main.gameObject.transform.position;
            GameObject new_acorn = GameObject.Instantiate(acorn_prefab, spawn_position, Quaternion.identity);
            new_acorn.GetComponent<Rigidbody>().velocity = GameObject.Find("Main Camera").gameObject.transform.forward * acorn_launch_velocity;
        }
        
    }
}
