using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mapbox.Unity.Map;

public class InventoryManager : Manager<InventoryManager>
{
    public static List<Tree> Trees = new();
    public static Dictionary<Tree.TreeType, bool> Discovered;
    public static float Currency = 30f;
    public static Dictionary<Tree.TreeType, int> Seeds = new();
    public static int numAcorns = 0;
    public float time = 0;
    public float squirrelSpawnTime = 10f;
    AbstractMap map;
    public GameObject squirrelPrefab;
    public float squirrelTimer = 0;

    void Start()
    {
        Discovered = new Dictionary<Tree.TreeType, bool>();
        Discovered[Tree.TreeType.T1] = true;
        foreach (Tree.TreeType tree_type in System.Enum.GetValues(typeof(Tree.TreeType)))
        {
            Seeds.Add(tree_type, 3);
        }
        InvokeRepeating("updateTreeRev", 1.0f, 5f);
    }

    
    private void Update()
    {
        map = GameObject.Find("LocationBasedGame").transform.Find("Map").GetComponent<AbstractMap>();
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            AdvanceTreeGrowth();
            updateTreeRev();

        }
        squirrelTimer += Time.deltaTime;
        //spawn a squirrel every squirrelSpawnTime s
        if (squirrelTimer >= squirrelSpawnTime)
        {
            squirrelTimer = 0;
            spawnSquirrel();
        }
        
    }

    void spawnSquirrel()
    {
        int index = Mathf.RoundToInt(Random.value * (Trees.Count - 1));
        Trees[index].withSquirrel = true;
        Vector3 posTree = map.GeoToWorldPosition(Trees[index].lat_long_coordinates, queryHeight: false);
        GameObject new_squirrel = Instantiate(squirrelPrefab, posTree + new Vector3(5f, 0, 0), Quaternion.Euler(-90, 0, 0));
        new_squirrel.GetComponent<isSquirreal>().lat_long_coordinates = Trees[index].lat_long_coordinates;
        Debug.Log("spawn a squirrel  "+index);
    }

    // Seed methods
    public static bool UseSeeds(Tree.TreeType treeType, int exspense = 1)
    {
        if (exspense <= Seeds[treeType])
        {
            Seeds[treeType] -= exspense;
            return true;
        }
        else
        {
            return false;
        }
    }

    // Tree methods
    void updateTreeRev()
    {
        foreach (Tree tree in Trees)
        {
            if (tree.growth_progress == 1)
                Currency += tree.revenue_per_tick_upon_adulthood;
        }
    }

    public static int SpawnTree(Tree.TreeType _tree_type, Vector2d _lat_long_coord, float _growth_proress, float _rev_per_tick)
    {
        Trees.Add(new Tree(_tree_type, _lat_long_coord, _growth_proress, _rev_per_tick));
        return Trees.Count - 1;
    }

    public static void AdvanceTreeGrowth()
    {
        foreach (Tree tree in Trees)
        {
            tree.growth_progress += 0.1f;
            if (tree.growth_progress >= 1.0f)
            {
                tree.growth_progress = 1.0f;
            }
        }
    }
}

public class Tree
{
    public enum TreeType
    {
        T1, // placeholder
        T2,
        T3,
        T4,
        T5,
        T6
        // more tree names here
    }

    public TreeType tree_type;
    public float growth_progress = 0.0f;
    public Vector2d lat_long_coordinates;
    public float revenue_per_tick_upon_adulthood = 0.1f;
    public float cost = 5.0f;
    public Vector3 original_scale;
    public GameObject treePrefab;
    public bool withSquirrel = false;


    public Tree(TreeType _tree_type, Vector2d _lat_long_coord, float _growth_proress, float _rev_per_tick)
    {
        tree_type = _tree_type;
        lat_long_coordinates = _lat_long_coord;
        growth_progress = _growth_proress;
        revenue_per_tick_upon_adulthood = _rev_per_tick;
    }
}
