using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMarkManager : MonoBehaviour
{
    void Update()
    {
        if (GameManager.CurrentScene == GameManager.SceneType.Exploration && Input.GetMouseButtonDown(0))
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("Raycast hit object " + hitInfo.transform.name + " at the position of " + hitInfo.transform.position);
                IsLandmark lm = hitInfo.transform.GetComponent<IsLandmark>();
                if (lm != null)
                {
                    lm.OnClick();
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }
}
