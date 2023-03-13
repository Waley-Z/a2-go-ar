using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class InventoryManager : Manager<InventoryManager>
{
    public static List<Tree> Trees = new();
    public static Dictionary<Tree.TreeType, bool> Discovered = new();
    public static Dictionary<Tree.TreeType, int> Seeds = new();
    public static bool isBonusActivated = false;
    public TreeConfig[] TreeConfigs;
 
    public static float Currency = 30f;
    public static int numAcorns = 0;
    public float squirrelSpawnTime = 60f;
    public GameObject SquirrelPrefab;

    void Start()
    {
        foreach (Tree.TreeType tree_type in System.Enum.GetValues(typeof(Tree.TreeType)))
        {
            Seeds.Add(tree_type, 0);
            Discovered[tree_type] = false;
        }
        Seeds[Tree.TreeType.T1] = 3;
        Discovered[Tree.TreeType.T1] = true;
        InvokeRepeating("AdvanceTreeGrowth", 0, 5f);
        InvokeRepeating("spawnSquirrel", 0, squirrelSpawnTime);
    }

    private void Update()
    {
        foreach (Tree tree in Trees)
        {
            if (tree.growth_progress >= 1f)
            {
                float scale = tree.withSquirrel ? 0.3f : 1f;
                Currency += tree.revenue_per_tick_upon_adulthood * scale;
            }
        }
    }

    void spawnSquirrel()
    {
        if (Trees.Count == 0)
            return;
        int index = Mathf.RoundToInt(Random.value * (Trees.Count - 1));
        if (Trees[index].withSquirrel)
            return;
        Trees[index].withSquirrel = true;

        GameObject ExplorationSpawnerGO = GameObject.Find("ExplorationSpawner");
        if (ExplorationSpawnerGO != null)
        {
            ExplorationSpawner es = ExplorationSpawnerGO.GetComponent<ExplorationSpawner>();
            es.SpawnExplorationSquirrelAtTree(Trees[index]);
        }
    }

    // Seed methods
    public static bool UseSeeds(Tree.TreeType treeType, int expense = 1)
    {
        if (expense <= Seeds[treeType])
        {
            Seeds[treeType] -= expense;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int AddTree(Tree.TreeType _tree_type, Vector2d _lat_long_coord)
    {
        print("plant tree at " + _lat_long_coord);
        Trees.Add(new Tree(_tree_type, _lat_long_coord));
        return Trees.Count - 1;
    }

    public void AdvanceTreeGrowth()
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

    public TreeConfig getTreeConfig(Tree.TreeType treeType)
    {
        foreach (TreeConfig tc in TreeConfigs)
        {
            if (tc.treeType == treeType)
            {
                return tc;
            }
        }
        Debug.LogError($"Config not found: {treeType}");
        return TreeConfigs[0];
    }
}

public class Tree
{
    public enum TreeType
    {
        T1,
        T2,
        T3,
        T4,
        T5,
        T6
        // more tree names here
    }

    public TreeType tree_type;
    public Vector2d lat_long_coordinates;

    public float revenue_per_tick_upon_adulthood;
    public GameObject treePrefab;
    public float cost;
 
    public float growth_progress = 0.0f;
    public bool withSquirrel = false;

    public Tree(TreeType _tree_type, Vector2d _lat_long_coord)
    {
        tree_type = _tree_type;
        lat_long_coordinates = _lat_long_coord;
        TreeConfig tc = InventoryManager.Instance.getTreeConfig(tree_type);
        revenue_per_tick_upon_adulthood = tc.revenue_per_tick_upon_adulthood;
        treePrefab = tc.treePrefab;
        cost = tc.cost;
    }
}

[System.Serializable]
public class TreeConfig
{
    public Tree.TreeType treeType;
    public GameObject treePrefab;
    public float revenue_per_tick_upon_adulthood = 0.1f;
    public float cost = 5.0f;
}
