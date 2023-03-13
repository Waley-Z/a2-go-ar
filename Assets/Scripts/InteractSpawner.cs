using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSpawner : MonoBehaviour
{
    void Start()
    {
        SpawnInteractionTree(GameManager.nearestTree);
    }

    public void SpawnInteractionTree(Tree tree)
    {
        Vector3 posTree = transform.position + 50 * transform.forward;
        if (tree != null)
        {
            GameObject new_tree = Instantiate(tree.treePrefab, posTree - Vector3.up * 5 + new Vector3(0f, 0, 20), Quaternion.Euler(90, 0, 0));
            new_tree.GetComponent<IsTree>().tree = tree;
            Debug.Log("spawn a tree in interactive mode");
            if (tree.withSquirrel)
            {
                GameObject new_squirrel = Instantiate(InventoryManager.Instance.SquirrelPrefab, posTree - Vector3.up * 5 + new Vector3(5f, 0, 0), Quaternion.Euler(-90, 180, 0));
                new_squirrel.GetComponent<isSquirreal>().originalPos = posTree - Vector3.up * 5;
                new_squirrel.GetComponent<isSquirreal>().tree = tree; // should be unnecessary
            }
        }
    }
}