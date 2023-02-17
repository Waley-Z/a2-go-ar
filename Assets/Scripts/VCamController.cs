using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamController : MonoBehaviour
{
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    public CinemachineVirtualCamera vcam;

    public float zoomModifierSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {

        //if (Input.touchCount == 2)
        //{
        //    Touch firstTouch = Input.GetTouch(0);
        //    Touch secondTouch = Input.GetTouch(1);

        //    firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        //    secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

        //    touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
        //    touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

        //    zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

        //    if (touchesPrevPosDifference > touchesCurPosDifference)
        //        vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance += zoomModifier;
        //    if (touchesPrevPosDifference < touchesCurPosDifference)
        //        vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance -= zoomModifier;

        //}

        //vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = Mathf.Clamp(vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance, 2f, 10f);
    }
}
