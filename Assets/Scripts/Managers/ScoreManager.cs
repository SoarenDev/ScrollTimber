using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
    public  GameObject                  money_particle_prefab       ;              

[Space(10)][Header("Runtime")]
    public  int                         actual_score                ;
    public  int                         actual_combo                ;           // +1 per perfect (+1 per tree at the 1st perfect, +2 on the 2nd, +3 at the 3rd...)

[Space(10)][Header("References")]
    public  scr_ui_updater_behavior     ui_behavior_ref             ;
    public  GameObject                  ui_money_particle_target    ;

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

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) == true)
        {
            AddScore(10);
        }
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

        // spawn particle
        GameObject instance;
        instance = Instantiate(money_particle_prefab, ui_money_particle_target.transform.position, Quaternion.Euler(-90,0,0));
        instance.transform.SetParent(GameManager.instance.ui_score_container.transform, true);
        Destroy(instance, 2.0f);

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
        GameManager.instance.total_money += actual_score;

        GameManager.instance.ui_behavior_ref.UpdateTotalMoneyUI( GameManager.instance.total_money.ToString() );

        return;
    }


// = = =

}
