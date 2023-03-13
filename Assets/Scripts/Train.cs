using Mapbox.Map;
using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;

public class Train : MonoBehaviour
{
    public AbstractMap map;
    public TextAsset jsonFile;
    List<Node> nodes = new();

    void Start()
    {
        Elements nodesJson = JsonUtility.FromJson<Elements>(jsonFile.text);

        foreach (int id in nodesJson.nodesOrder)
        {
            foreach (Node node in nodesJson.nodes)
            {
                if (node.id == id)
                {
                    nodes.Add(node);
                    break;
                }
            }
        }

        print("load " + nodes.Count);
    }

    public float MoveSpeed = 10f; // distance per second
    float Timer;
    float TotalTime;
    [SerializeField] Vector3 Target;
    int CurrentNode;
    [SerializeField] Vector3 startPosition;
    int direction = 1;

    void NextNode(Node nextNode)
    {
        Timer = 0;
        startPosition = transform.position;
        Target = nodePos(nextNode);
        TotalTime = Vector3.Distance(Target, transform.position) / MoveSpeed;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, Target - startPosition);
    }

    void Update()
    {
        if (transform.position == Vector3.zero || transform.position.y == 1000)
        {
            // init
            int randomIdx = Random.Range(0, nodes.Count - 1);
            Vector3 pos = nodePos(nodes[randomIdx]);
            if (pos == Vector3.zero)
            {
                transform.position = new Vector3(0, 1000, 0);
                return;
            }
            pos.y = 0;
            transform.position = pos;
            NextNode(nodes[randomIdx + 1]);
        }

        if (GameManager.CurrentScene == GameManager.SceneType.Exploration &&
            Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 100f)
        {
            SoundManager.PlaySound(SoundManager.Sound.TrainHorn);
        }

        Timer += Time.deltaTime;

        if (Timer < TotalTime)
        {
            transform.position = Vector3.Lerp(startPosition, Target, Timer / TotalTime);
            return;
        }
        if (CurrentNode + direction == nodes.Count || CurrentNode + direction == -1)
        {
            direction *= -1;
        }
        CurrentNode += direction;
        NextNode(nodes[CurrentNode]);
    }

    Vector3 nodePos(Node node)
    {
        return map.GeoToWorldPosition(new Vector2d(node.lat, node.lon), queryHeight: false);
    }


    [System.Serializable]
    public class Node
    {
        public string type;
        public int id;
        public float lat;
        public float lon;
    }

    [System.Serializable]
    public class Elements
    {
        public int[] nodesOrder;
        public Node[] nodes;
    }
}