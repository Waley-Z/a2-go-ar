using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : Manager<InventoryManager>
{
    public static List<Tree> Trees = new();
    public static float Currency = 30f;
    public static Dictionary<Tree.TreeType, int> Seeds = new();

    void Start()
    {
        foreach (Tree.TreeType tree_type in System.Enum.GetValues(typeof(Tree.TreeType)))
        {
            Seeds.Add(tree_type, 3);
        }
        InvokeRepeating("updateTreeRev", 1.0f, 5f);
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
        // more tree names here
    }

    public TreeType tree_type;
    public float growth_progress = 0.0f;
    public Vector2d lat_long_coordinates;
    public float revenue_per_tick_upon_adulthood = 1.0f;

    public Tree(TreeType _tree_type, Vector2d _lat_long_coord, float _growth_proress, float _rev_per_tick)
    {
        tree_type = _tree_type;
        lat_long_coordinates = _lat_long_coord;
        growth_progress = _growth_proress;
        revenue_per_tick_upon_adulthood = _rev_per_tick;
    }
}
