using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    public Vector3 movement_relative_to_object;

    void Update ()
    {
        transform.Translate(movement_relative_to_object * Time.deltaTime);
    }
}
