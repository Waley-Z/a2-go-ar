using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookinDirectionOfMovement : MonoBehaviour
{
    Vector3 previous_position;
    // Start is called before the first frame update
    void Start()
    {
        previous_position = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 current_position = transform.position;
        if (current_position == previous_position)
            return;
        Vector3 delta = current_position - previous_position;
        delta = delta.normalized;
        transform.forward = delta;
        previous_position = current_position;
    }
}
