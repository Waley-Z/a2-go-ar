using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCameraController : MonoBehaviour
{
    float touchesPrevPosDifference, touchesCurPosDifference;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    public CinemachineFreeLook fcam;

    public float ZoomModifierSpeed = 0.001f;
    public float MinDistance = 50f;
    public float MaxDistance = 500f;
    public float XAxisModifierSpeed = 0.1f;
    public float YAxisModifierSpeed = 0.001f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            float delta = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * ZoomModifierSpeed;

            if (touchesPrevPosDifference < touchesCurPosDifference)
                delta *= -1;

            for (int i = 0; i < fcam.m_Orbits.Length; i++)
            {
                float currentSphereRadius = Mathf.Sqrt(Mathf.Pow(fcam.m_Orbits[i].m_Radius, 2) + Mathf.Pow(fcam.m_Orbits[i].m_Height, 2));

                if ((currentSphereRadius < MinDistance && delta < 0) || (currentSphereRadius > MaxDistance && delta > 0))
                    break;

                fcam.m_Orbits[i].m_Radius *= 1 + delta / currentSphereRadius;
                fcam.m_Orbits[i].m_Height *= 1 + delta / currentSphereRadius;
            }

        }

        if (Input.touchCount == 1)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            fcam.m_XAxis.Value += touchDeltaPosition.x * XAxisModifierSpeed;
            fcam.m_YAxis.Value = Mathf.Clamp(fcam.m_YAxis.Value - touchDeltaPosition.y * YAxisModifierSpeed, 0, 1);
        }
    }
}
