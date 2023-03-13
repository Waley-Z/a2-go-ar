using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class SwitchModeButton : MonoBehaviour
{
    public GameManager.SceneType NewScene;
    public AbstractMap map;

    public void OnSwitchButtonPressed()
    {
        if (GameManager.CurrentScene == GameManager.SceneType.Exploration)
        {
            if (InventoryManager.Trees.Count != 0)
            {
                int index = FindNearestTree();
                Debug.Log("nearest index " + index);

                GameManager.nearestTreeIndex = index;
            }
        }
        GameManager.Instance.LoadScene(NewScene);
    }

    int FindNearestTree()
    {
        if (InventoryManager.Trees.Count == 0)
        {
            return -1;
        }

        Vector3 playerpos = map.GeoToWorldPosition(GameManager.targetPos, queryHeight: false);
        Vector3 treePos = map.GeoToWorldPosition(InventoryManager.Trees[0].lat_long_coordinates, queryHeight: false);
        int minIndex = 0;
        float minDist = Vector3.Distance(playerpos, treePos);
        for (int i = 0; i < InventoryManager.Trees.Count; i++)
        {
            Tree tree = InventoryManager.Trees[i];
            treePos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
            float dist = Vector3.Distance(playerpos, treePos);
            //Debug.Log(dist + " " + minDist);
            if (dist < minDist)
            {
                minIndex = i;
                minDist = dist;
            }
        }

        if (minDist <= 20)
        {
            return minIndex;
        }
        else
        {
            return -1;
        }
    }
}
