using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class SwitchModeButton : MonoBehaviour
{
    public GameManager.SceneType NewScene;
    public AbstractMap map;
    public void OnSwitchButtonPressed()
    {
        GameManager.Instance.LoadScene(NewScene);
        if (GameManager.getSceneType(SceneManager.GetActiveScene().name) == GameManager.SceneType.Exploration)
        {
            if (InventoryManager.Trees.Count != 0)
            {
                int index = FindNearestTree();
                Debug.Log("nearest index " + index);
                if (index != -1)
                {

                    GameManager.nearestTree = InventoryManager.Trees[index];
                    GameManager.nearestTreeIndex = index;
                    

                }
                else
                {
                    GameManager.nearestTree = null;
                }
            }
        }
    }
    int FindNearestTree()
    {

        Vector3 playerpos = map.GeoToWorldPosition(GameManager.targetPos, queryHeight: false);
        Vector3 treePos = map.GeoToWorldPosition(InventoryManager.Trees[0].lat_long_coordinates, queryHeight: false);
        float minDist = Vector3.Distance(playerpos, treePos);
        Tree nearestTree = InventoryManager.Trees[0];
        int minIndex = 0;
        int index = 0;
        for (int i=0; i < InventoryManager.Trees.Count; i ++)
        {
            Tree tree = InventoryManager.Trees[i];
            treePos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
            float dist = Vector3.Distance(playerpos, treePos);
            //Debug.Log(dist + " " + minDist);
            if (dist < minDist)
            {
                minIndex = i;
                minDist = dist;
                nearestTree = tree;
            }
            index++;
        }
        if (minDist <= 10)
        {
            return minIndex;
        }
        else
        {
            return -1;
        }
    }
}
