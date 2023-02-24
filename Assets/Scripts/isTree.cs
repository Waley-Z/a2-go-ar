using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
public class IsTree : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2d lat_long_position;
    Vector3 offset;
    AbstractMap map;
    void Start()
    {
        map = GameObject.Find("LocationBasedGame").transform.Find("Map").GetComponent<AbstractMap>();
        offset = new Vector3(Random.value-0.5f, 0f, Random.value-0.5f).normalized * 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = map.GeoToWorldPosition(lat_long_position, queryHeight: false) + offset;
    }
}
