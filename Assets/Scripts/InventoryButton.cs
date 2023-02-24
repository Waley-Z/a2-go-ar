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
    int quantity = 0;
    TextMeshProUGUI quantityText;
    public GameObject spawnText;
    public GameObject treePrefab;
    public AudioClip spawnSound;

    // Start is called before the first frame update
    void Start()
    {
        quantity = Random.Range(1, 9);
        quantityText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SpawnTree()
    {
        if (GameManager.getSceneType(SceneManager.GetActiveScene().name) == GameManager.SceneType.Interaction && quantity > 0)
        {
            quantity--;
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            StartCoroutine(showSpawnText());
            Debug.Log(GameManager.targetPos);
            Debug.Log(TreeDataManager.GetTreeData());
            TreeDataManager.Spawn("1", GameManager.targetPos, 0.0f, 1.0f);
        }
    }

    IEnumerator showSpawnText()
    {
        spawnText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        spawnText.SetActive(false);
    }

    void Update()
    {
        quantityText.text = quantity.ToString();
    }
}
