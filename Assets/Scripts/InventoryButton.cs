using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;

public class InventoryButton : MonoBehaviour
{
    public Tree.TreeType treeType;
    public TextMeshProUGUI quantityText;
    public AudioClip spawnSound;

    public void PlantTree()
    {
        if (GameManager.getSceneType(SceneManager.GetActiveScene().name) == GameManager.SceneType.Interaction
            && InventoryManager.UseSeeds(treeType))
        {
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            StartCoroutine(UIManager.Instance.showSpawnText());
            int index = InventoryManager.SpawnTree(treeType, GameManager.targetPos, 0.0f, 1.0f);
            Debug.Log("plant a tree " + InventoryManager.Trees.Count + index);
            GameObject.Find("Main Camera").GetComponent<InteractSpawner>().spawnObjects(InventoryManager.Trees[index]);
        }
    }

    void Update()
    {
        quantityText.text = InventoryManager.Seeds[treeType].ToString();
    }
}
