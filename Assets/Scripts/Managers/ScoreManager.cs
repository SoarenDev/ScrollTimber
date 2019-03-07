using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    public  int                         actual_score                ;
    public  int                         actual_combo                ;           // +1 per perfect (+1 per tree at the 1st perfect, +2 on the 2nd, +3 at the 3rd...)

[Space(10)][Header("References")]
    public  scr_ui_updater_behavior     ui_behavior_ref             ;

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

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
        // Check references
        if (ui_behavior_ref == null)
        { Debug.LogError("ERROR: UI BEHAVIOR REFERENCE MISSING! All UI updates will return an error!"); }

    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Adds the given value to the actual player score.
    /// </summary>
    public void AddScore(int value)
    {
        actual_score += value;
        ui_behavior_ref.UpdateScoreUI( actual_score.ToString() );

        return;
    }

    /// <summary>
    /// Resets the current player score.
    /// </summary>
    public void ResetScore()
    {
        actual_score = 0;
        ui_behavior_ref.UpdateScoreUI( actual_score.ToString() );

        return;
    }

    /// <summary>
    /// Adds the given value to the actual combo value.
    /// </summary>
    public void AddCombo(int value)
    {
        actual_combo += value;
        ui_behavior_ref.UpdateComboUI( actual_combo.ToString() );

        return;
    }

    /// <summary>
    /// Resets the current player combo.
    /// </summary>
    public void ResetCombo()
    {
        actual_combo = 0;
        ui_behavior_ref.UpdateComboUI( actual_combo.ToString() );

        return;
    }


    /// <summary>
    /// Registers the actual score as the score obtained in the actual level, setting it as actual level's best_score if it's the case.
    /// </summary>
    public void RegisterScore()
    {

        return;
    }

    /// <summary>
    /// Adds the actual score/money to the total money of the player.
    /// </summary>
    public void AddScoreToTotalMoney()
    {
        GameManager.instance.actual_money += actual_score;
        return;
    }


// = = =

}
