using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    public Tree.TreeType treeType;
    public TextMeshProUGUI quantityText;
    public AudioClip spawnSound;

    public void PlantTree()
    {
        if (GameManager.CurrentScene == GameManager.SceneType.Interaction
            && InventoryManager.UseSeeds(treeType))
        {
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            StartCoroutine(UIManager.Instance.showSpawnText());
            int index = InventoryManager.AddTree(treeType, GameManager.targetPos);
            Debug.Log("plant a tree " + InventoryManager.Trees.Count + index);
            Camera.main.GetComponent<InteractSpawner>().SpawnInteractionTree(InventoryManager.Trees[index]);
        }
    }

    void Update()
    {
        quantityText.text = InventoryManager.Seeds[treeType].ToString();
    }
}
