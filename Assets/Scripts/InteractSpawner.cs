using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSpawner : MonoBehaviour
{
    public Transform cursor;
    GameObject new_tree;

    private void Update()
    {
        if (new_tree == null && cursor.position != Vector3.zero)
        {
            SpawnInteractionTree(GameManager.nearestTree);
        }
    }

    public void SpawnInteractionTree(Tree tree)
    {
        if (tree != null)
        {
            new_tree = Instantiate(tree.treePrefab);
            new_tree.transform.SetPositionAndRotation(cursor.position, cursor.rotation);
            new_tree.GetComponent<IsTree>().tree = tree;
            new_tree.transform.localScale *= 0.1f;

            if (tree.withSquirrel)
            {
                GameObject new_squirrel = Instantiate(InventoryManager.Instance.SquirrelPrefab);
                new_squirrel.transform.SetPositionAndRotation(cursor.position + new Vector3(0, 0, 2.5f), cursor.rotation);
                new_squirrel.transform.localScale *= 0.08f;

                new_squirrel.GetComponent<isSquirreal>().tree = tree; // should be unnecessary
            }
        }
    }
}