using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class IsLandmark : MonoBehaviour
{
    public Vector2d lat_long_position;
    AbstractMap map;

    public Tree.TreeType UnlockTreeType;
    public GameObject DetailPage;

    void Start()
    {
        map = GameObject.Find("LocationBasedGame").transform.Find("Map").GetComponent<AbstractMap>();
        float randomRotY = Random.value * 360;
        transform.rotation = Quaternion.Euler(0, randomRotY, 0);
    }

    void Update()
    {
        Vector3 pos = map.GeoToWorldPosition(lat_long_position, queryHeight: false);
        pos.y = 1.5f;
        transform.position = pos;
        transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    public void OnClick()
    {
        InventoryManager.Discovered[UnlockTreeType] = true;
        DetailPage.SetActive(true);
    }
}
