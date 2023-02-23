using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;

public class InventorySystem : MonoBehaviour
{
    int quantity = 0;
    TextMeshProUGUI quantity_text;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        quantity = Random.Range(1, 9);
        quantity_text = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 
        //Button inventory_button_1 = transform.Find("Viewport").Find("Content").Find("InventoryButton").GetComponent<Button>();
        //TextMeshProUGUI quantity_text = inventory_button_1.transform.Find("quantity_text").GetComponent<TextMeshProUGUI>();
        //quantity_text.text = UnityEngine.Random.Range(1, 9).ToString();
        //inventory_button_1.onClick.AddListener(onPressTree1);
    }

    //void onPressTree1()
    //{
    //    SpawnTree("1");
    //}
    public GameObject tree_prefab;
    public AudioClip spawnSound;
    public void SpawnTree(string tree_type)
    {
        if (SceneManager.GetActiveScene().name == "interaction_scene" && quantity > 0)
        {
            if (tree_type == "1")
            {
            }
            quantity--;
            //print("spawn");
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            //GameObject new_tree = GameObject.Instantiate(tree_prefab, Vector3.zero, Quaternion.identity);
            //new_tree.transform.localScale = new Vector3(12f, 12f, 12f);
            //new_tree.transform.position = GameObject.Find("PlayerTarget").transform.position + (new Vector3(Random.value, Random.value, 0f)).normalized * 1.2f;
            StartCoroutine(showSpawnText());
            //Debug.Log("spawn");
            Debug.Log(GameManager.targetPos);
            Debug.Log(TreeDataManager.GetTreeData());
            TreeDataManager.Spawn("1", 
                (GameManager.targetPos),
                0.0f,
                1.0f);
        }
    }
    public GameObject spawntext;
    IEnumerator showSpawnText()
    {
        spawntext.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        spawntext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        quantity_text.text = quantity.ToString();
    }
    
}
