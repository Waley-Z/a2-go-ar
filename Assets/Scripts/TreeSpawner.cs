using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using static GameManager;

public class TreeSpawner : MonoBehaviour
{
    public TreePrefab[] TreePrefabs;
    AbstractMap map;

    [System.Serializable]
    public class TreePrefab
    {
        public Tree.TreeType treeType;
        public GameObject treePrefab;
    }

    GameObject getTreePrefab(Tree.TreeType treeType)
    {
        foreach (TreePrefab tp in TreePrefabs)
        {
            if (tp.treeType == treeType)
            {
                return tp.treePrefab;
            }
        }
        Debug.LogError($"Prefab not found: {treeType}");
        return TreePrefabs[0].treePrefab;
    }

    void Start()
    {
        foreach (Tree tree in InventoryManager.Trees)
        {
            Vector3 pos = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false);
            GameObject new_tree = Instantiate(getTreePrefab(tree.tree_type), pos, Quaternion.identity);
            new_tree.GetComponent<IsTree>().tree = tree;
        }
    }
}
