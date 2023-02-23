using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;

public class TreeDataManager : MonoBehaviour
{
    static List<TreeDatum> all_trees = new List<TreeDatum>();
    private void Start()
    {
    }
    public static List<TreeDatum> GetTreeData()
    {
        return all_trees;

    }
    public static void Spawn(string _tree_type, Vector2d _lat_long_coord, float _growth_proress, float _rev_per_tick)
    {
        all_trees.Add(new TreeDatum(_tree_type, _lat_long_coord, _growth_proress, _rev_per_tick));
    }
    public static void AdvanceTreeGrowth()
    {
        foreach(TreeDatum tree in all_trees)
        {
            tree.growth_progress += 0.1f;
            if(tree.growth_progress >= 1.0f)
            {
                tree.growth_progress = 1.0f;
                //PlayerPrefs.currency += tree.revenuew_per_tick_upon_adulthood;
            }
        }
    }
}
public class TreeDatum
{
    public string tree_type;
    public float growth_progress = 0.0f;
    public Vector2d lat_long_coordinates;
    public float revenuew_per_tick_upon_adulthood = 1.0f;
    public TreeDatum(string _tree_type, Vector2d _lat_long_coord, float _growth_proress, float _rev_per_tick)
    {
        tree_type = _tree_type;
        lat_long_coordinates = _lat_long_coord;
        growth_progress = _growth_proress;
        revenuew_per_tick_upon_adulthood = _rev_per_tick;
            ;
    }
}
