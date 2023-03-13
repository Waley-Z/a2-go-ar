using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class IsTree : MonoBehaviour
{
    public Tree tree;
    [SerializeField] Vector3 original_scale;
    [SerializeField] Vector3 offset;
    AbstractMap map;

    void Start()
    {
        if (GameManager.CurrentScene == GameManager.SceneType.Exploration)
        {
            map = GameObject.Find("LocationBasedGame").transform.Find("Map").GetComponent<AbstractMap>();
        }
        offset = new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f).normalized * 10f;
        original_scale = transform.localScale;
    }

    void Update()
    {
        if (map != null)
        {
            transform.position = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false) + offset;
        }
        transform.localScale = original_scale * (0.3f + tree.growth_progress);
    }
}
