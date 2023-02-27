using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSpawner : MonoBehaviour
{
    public TreePrefab[] TreePrefabs;
    public GameObject SquirrelPrefab;

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
        spawnObjects(GameManager.nearestTree);
    }

    public void spawnObjects(Tree tree)
    {
        Vector3 posTree = transform.position + 50 * transform.forward;
        if (tree != null)
        {

            Instantiate(getTreePrefab(tree.tree_type), posTree - Vector3.up * 5 + new Vector3(0f, 0, 20), Quaternion.Euler(90, 0, 0));
            Debug.Log("spawn a tree in interactive mode");
            if (tree.withSquirrel)
            {
                GameObject new_squirrel = GameObject.Instantiate(SquirrelPrefab, posTree - Vector3.up * 5 + new Vector3(5f, 0, 0), Quaternion.Euler(-90, 180, 0));
                new_squirrel.GetComponent<isSquirreal>().originalPos = posTree - Vector3.up * 5;
            }
        }
        
    }

    private void Update()
    {


    }
}