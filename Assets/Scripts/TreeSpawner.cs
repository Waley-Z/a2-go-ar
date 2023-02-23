using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
public class TreeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tree1prefab;
    public AbstractMap map;
    void Start()
    {
        foreach (TreeDatum tree in TreeDataManager.GetTreeData())
        {
            //Vector3 pos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
            //Debug.Log(pos);
            //Debug.Log(tree.lat_long_coordinates);
            //Debug.Log("spawn");
            Vector3 pos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
            GameObject new_tree = Instantiate(tree1prefab, pos, Quaternion.identity);
            new_tree.GetComponent<isTree>().lat_long_position = tree.lat_long_coordinates;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
