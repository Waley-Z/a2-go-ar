using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using UnityEngine.SceneManagement;
public class isSquirreal : MonoBehaviour
{
    public Vector2d lat_long_coordinates;
    AbstractMap map;
    public float walkSpeed = 15f;
    float scale = 0f;
    bool isRight = true;
    public Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("LocationBasedGame").transform.Find("Map").GetComponent<AbstractMap>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.getSceneType(SceneManager.GetActiveScene().name) == GameManager.SceneType.Interaction)
        {
            //Vector3 dirVec = Vector3.right;
            if (scale < -20)
            {
                isRight = true;
            }
            if (scale > 20)
            {
                isRight = false;
            }
            if (isRight)
            {
                scale += walkSpeed * Time.deltaTime;
                transform.position = originalPos + Vector3.right * scale;
            }
            else
            {
                scale -= walkSpeed * Time.deltaTime;
                transform.position = originalPos + Vector3.right * scale;
            }
            //Debug.Log(scale);
        }
        else
        {
            transform.position = map.GeoToWorldPosition(lat_long_coordinates, queryHeight: false) + new Vector3(1f, 0, 0);

        }

    }

    //void walk(Vector3 dirVec)
    //{
    //    if (scale < -20)
    //    {
    //        isRight = true;
    //    }
    //    if(scale > 20)
    //    {
    //        isRight = false;
    //    }
    //    if (isRight)
    //    {
    //        scale += walkSpeed * Time.deltaTime;
    //        transform.position = map.GeoToWorldPosition(lat_long_coordinates, queryHeight: false) + dirVec * scale;
    //    }
    //    else
    //    {
    //        scale -= walkSpeed * Time.deltaTime;
    //        transform.position = map.GeoToWorldPosition(lat_long_coordinates, queryHeight: false) + dirVec * scale;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Acorn")
        {
            return;
            
        }
        SoundManager.Instance.pauseBGM(0);
        SoundManager.StartBGM(SoundManager.Sound.MusicInteraction);
        GameManager.nearestTree.withSquirrel = false;
        Tree tree = InventoryManager.Trees[GameManager.nearestTreeIndex];
        //InventoryManager.Trees[GameManager.nearestTreeIndex].withSquirrel = false;
        InventoryManager.Trees.Add(new Tree(tree.tree_type, tree.lat_long_coordinates, tree.growth_progress, tree.revenue_per_tick_upon_adulthood));
        InventoryManager.Trees.RemoveAt(GameManager.nearestTreeIndex);
        Debug.Log("destroy");
        foreach (Tree _tree in InventoryManager.Trees)
        {
            Debug.Log(_tree.withSquirrel);
        }
        Destroy(collision.gameObject);
        Destroy(gameObject);

    }
}
