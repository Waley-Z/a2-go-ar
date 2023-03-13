using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class isSquirreal : MonoBehaviour
{
    public Tree tree;
    AbstractMap map;
    public float walkSpeed = 15f;
    float scale = 0f;
    bool isRight = true;
    public Vector3 originalPos;

    void Start()
    {
        map = GameObject.Find("LocationBasedGame").transform.Find("Map").GetComponent<AbstractMap>();
        originalPos = transform.position;
    }

    void Update()
    {
        if (GameManager.CurrentScene == GameManager.SceneType.Interaction)
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
            transform.position = map.GeoToWorldPosition(tree.lat_long_coordinates, queryHeight: false) + new Vector3(1f, 0, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Acorn")
        {
            return;
        }
        SoundManager.Instance.pauseBGM(1f);
        SoundManager.StartBGM(SoundManager.Sound.MusicInteraction);
        GameManager.nearestTree.withSquirrel = false;
        Debug.Log("destroy squirrel");
        foreach (Tree tree in InventoryManager.Trees)
        {
            Debug.Log(tree.withSquirrel);
        }
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
