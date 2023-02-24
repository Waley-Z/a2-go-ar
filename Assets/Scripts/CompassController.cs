using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 northDir = Quaternion.Inverse(Camera.main.transform.rotation) * Camera.main.transform.forward;
        transform.rotation = Quaternion.LookRotation(northDir, Vector3.up);
    }
}
