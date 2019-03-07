using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tree_behavior : MonoBehaviour
{

    // = = = [ VARIABLES DEFINITION ] = = =

    [Space(10)]
    [Header("Runtime")]
    public Vector3 weak_point_direction;           // actual weak point direction, used to compare with director vector

    [Space(10)]
    [Header("Gameplay")]
    public float weak_point_direction_max = 0.50f;     // maxmimal angular ratio that the cut can reach (negative and positive)
    private GameObject decal_prefab_instance;
    public int[] anglesList;
    public GameObject[] decal_prefabs;

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
        weak_point_direction = GenerateCutDirection();
        // Debug.Log(weak_point_direction);
        // Debug.DrawLine(gameObject.transform.position, gameObject.transform.position+weak_point_direction, Color.yellow, 10.00f);
        return;
    }

    /// <summary>
	/// [OBSOLETE] Initialises the object self destruction after a given amount of time.
	/// </summary>
	public void InitializeSelfDestruction(float delay)
    {
        return;
    }

    /// <summary>
	/// Returns a normalized vector that indicates a direction.
	/// </summary>
	public Vector3 GenerateCutDirection()
    {
        Vector3 direction_vector = new Vector3(0, 0, 0);

        int randomSelectedNumber = Random.Range(0, anglesList.Length);

        decal_prefab_instance = GameObject.Instantiate(decal_prefabs[randomSelectedNumber], this.transform); // Generates the visible cut along with the weak point of the tree.

        float directionAngle = anglesList[randomSelectedNumber]; // Getting one of the stored vector stored in anglesList

        direction_vector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * directionAngle), Mathf.Sin(Mathf.Deg2Rad * directionAngle), 0); //converting the angle to a Vector3

        // normalize combined axis inputs' value
        float normalize_factor;  // factor used to normalise the vector

        normalize_factor = Mathf.Abs(direction_vector.x) + Mathf.Abs(direction_vector.y);


        // return normalized direction vector
        return (direction_vector / normalize_factor);
    }




    /// <summary>
    /// Called by the cutting manager when the tree is cutted by the player.
    /// </summary>
    public void CutTree(cutStateEnum state)
    {
        switch (state)
        {
            case cutStateEnum.Success:
                Debug.Log("SUCCESS");
                CuttingManager.cuttingManagerInstance.UnregisterTree();
                ScoreManager.instance.AddScore(1 + ScoreManager.instance.actual_combo);
                break;

            case cutStateEnum.Perfect:
                Debug.Log("PERFECT");
                CuttingManager.cuttingManagerInstance.UnregisterTree();
                ScoreManager.instance.AddCombo(1);
                ScoreManager.instance.AddScore(1 + ScoreManager.instance.actual_combo);
                break;
        }

        GameManager.instance.RemainingTrees -= 1;

        // remove tree from generated list
        ScrollingManager.instance.level_generation_script_ref.generated_trees.Remove(this.gameObject);

        return;
    }


    // = = =

}
