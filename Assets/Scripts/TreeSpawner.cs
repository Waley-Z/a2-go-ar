using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using static GameManager;
using UnityEngine.SceneManagement;

public class TreeSpawner : MonoBehaviour
{
    public TreePrefab[] TreePrefabs;
    public GameObject SquirrelPrefab;
    public AbstractMap map;

    [System.Serializable]
    public class TreePrefab
    {
        public Tree.TreeType treeType;
        public GameObject treePrefab;
    }

    public GameObject getTreePrefab(Tree.TreeType treeType)
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
            GameObject new_tree = Instantiate(getTreePrefab(tree.tree_type), pos, Quaternion.Euler(90, 0, 0));
            new_tree.GetComponent<IsTree>().tree = tree;
            tree.treePrefab = new_tree;
            tree.original_scale = new_tree.transform.localScale;
            if (tree.withSquirrel)
            {
                GameObject new_squirrel = Instantiate(SquirrelPrefab, pos + new Vector3(1f, 0, 0), Quaternion.Euler(-90, 180, 0));
            }
        }
        
    }
    private void Update()
    {
        
            foreach (Tree tree in InventoryManager.Trees)
            {
                tree.treePrefab.transform.localScale = tree.original_scale * (0.5f + tree.growth_progress / 2);
            }
        
           

    }
}
