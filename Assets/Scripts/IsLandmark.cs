using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class IsLandmark : MonoBehaviour
{
    public Vector2d lat_long_position;
    public AbstractMap map;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = map.GeoToWorldPosition(lat_long_position, queryHeight: false);
    }
}
