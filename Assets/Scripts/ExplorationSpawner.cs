using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using System;

public class ExplorationSpawner : MonoBehaviour
{
    public AbstractMap map;

    void Start()
    {
        foreach (Tree tree in InventoryManager.Trees)
        {
            Vector3 pos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
            print("GeoToWorldPosition " + pos);
            GameObject new_tree = Instantiate(tree.treePrefab, pos, Quaternion.Euler(90, 0, 0));
            new_tree.GetComponent<IsTree>().tree = tree;
            if (tree.withSquirrel)
            {
                SpawnExplorationSquirrelAtTree(tree);
            }
        }
    }

    public void SpawnExplorationSquirrelAtTree(Tree tree)
    {
        Vector3 pos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
        GameObject new_squirrel = Instantiate(InventoryManager.Instance.SquirrelPrefab,
            pos + new Vector3(1f, 0, 0),
            Quaternion.Euler(-90, 180, 0));
        new_squirrel.GetComponent<isSquirreal>().tree = tree;
        Debug.Log("spawn a squirrel");
    }
}
