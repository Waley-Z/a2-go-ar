using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public Vector3 relative_rotation;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(relative_rotation * Time.deltaTime);
    }
}
