using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tree_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    public  float                   weak_point_position                 ;           // actual weak point position in tree's height ratio (runtime generated)

[Space(10)][Header("Gameplay")]
    public  float                   weak_point_spawnRange               = 0.50f;           // range in which the weak point can spawn (ratio of the tree's height)
  
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    void Start()
    {
        GenerateAppearance();
        InitializeWeakPoint();
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Generates the appearance of the tree.
    /// </summary>
    public void GenerateAppearance()
    {

        return;
    }

    /// <summary>
    /// Initializes the tree weakpoint position according to weak_point_spawnRange.
    /// </summary>
    public void InitializeWeakPoint()
    {
        weak_point_position = Random.Range(0, weak_point_spawnRange);
        return;
    }

// = = =

}
