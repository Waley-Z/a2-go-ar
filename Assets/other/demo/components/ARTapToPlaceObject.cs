using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;


    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPostIsValid = false;

    void Start ()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdatePlacementPose ();
        UpdatePlacementIndicator ();

        if(placementPostIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //PlaceObject();
        }
    }

    public void PlaceObject()
    {
        if (placementPostIsValid)
            GameObject.Instantiate(objectToPlace, placementPose.position + Vector3.up * 0.1f, placementPose.rotation);
    }

    public void RequestInfo ()
    {
        Application.OpenURL(@"https://en.wikipedia.org/wiki/Ann_Arbor,_Michigan");
    }

    void UpdatePlacementPose()
    {
        var screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        var hits = new List<ARRaycastHit>();

        arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        placementPostIsValid = hits.Count > 0;
        if(placementPostIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        placementIndicator.SetActive(placementPostIsValid);
        placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
    }
}
