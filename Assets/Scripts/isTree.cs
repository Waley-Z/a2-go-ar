using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class IsTree : MonoBehaviour
{
    public Tree tree;
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
        transform.position = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false) + offset;
        //transform.localScale = tree.growth_progress * transform.localScale; // TODO
    }
}
