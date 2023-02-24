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
            InventoryManager.SpawnTree(Tree.TreeType.T1, GameManager.targetPos, 0.0f, 1.0f);
        }
    }

    void Update()
    {
        quantityText.text = InventoryManager.Seeds[treeType].ToString();
    }
}
