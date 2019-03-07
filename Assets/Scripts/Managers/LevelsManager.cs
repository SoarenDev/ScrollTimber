using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{

    public static LevelsManager instance;

[Space(10)][Header("Runtime")]
                        public      int                                         actual_level                ;

[Space(10)][Header("Data")]
    [SerializeField]    private     List<so_level_data>                         level_data_list             = new List<so_level_data>();       // only used to generate the dictionnary
                        public      Dictionary<int, so_level_data>              level_data_dict             = new Dictionary<int, so_level_data>();

// = = = [ VARIABLES DEFINITION ] = = =

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        InitializeDictionnaries();
    }
       
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =



// = = =

// = = = [ CLASS METHODS ] = = =

    // <summary>
    // Transforms every database's lists into dictionnaries, placing them at their right ID position in the database.
    // </summary>
    void InitializeDictionnaries()
    {
        // LEVEL DATA
        foreach (var item in level_data_list)
		{
			if (item != null) 
			{ 
				level_data_dict.Add(item.id, item); 
			} 
		}

		Debug.Log("level data dictionnary created with " + level_data_dict.Count + " references!");
		// Debug.Log("Test dictionnary reference name : reference" + "0" + " = " + level_data_dict[0].name);

        return;
    }

    // <summary>
    // Resets every game data related to the level the player was currently playing.
    // </summary>
    public void ResetLevelState()
    {
        // reset score
        ScoreManager.instance.ResetScore();
        ScoreManager.instance.ResetCombo();

        // fails count
        GameManager.instance.fails_count = 0;

        // remaining trees
        GameManager.instance.RemainingTrees = -1;      // set to -1 to avoid triggering the LevelSuccess of the propertie

        return;
    }

// = = =

}
