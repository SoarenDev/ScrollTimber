using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_GameState
{
    startmenu,
    paused,
    running,
    endmenu
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Gameplay")]
    public  int             allowed_fails                       = 1;

[Space(10)][Header("Runtime")]
    public  bool            isGamePaused                        = false;
    public  enum_GameState  game_state                          ;
    public  int             fails_count                         ;
    public  int             remaining_trees                     ;

[Space(10)][Header("References")]
    public  GameObject      scroll_cylinder_parent              ;
    public  GameObject      ui_main_menu_parent                 ;
    public  GameObject      ui_score_container                  ;

// = = =

// = = = [ VARIABLES PROPERTIES ] = = =

    public int FailsCount
    {
        get
        { return fails_count; }
        set
        {
            // set new value
            fails_count = value;

            // launch level failure if too many fails
            if (fails_count > allowed_fails)
            { LevelFailure(); }
        }
    }

    public int RemainingTrees
    {
        get
        { return remaining_trees; }
        set
        {
            // set new value
            remaining_trees = value;

            // launch level failure if too many fails
            if (remaining_trees == 0 && game_state == enum_GameState.running )
            { LevelSuccess(); }
        }
    }

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


// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Called when the player press the "Play" button; launches the actual level.
    /// </summary>
    public void StartGame()
    {
        Debug.LogWarning("<b>!!! START GAME !!!</b>");

        LevelsManager.instance.ResetLevelState();
        ScrollingManager.instance.level_generation_script_ref.StartLevelGeneration();

        // change game state
        game_state = enum_GameState.running;
        ui_score_container.SetActive(true);     // a mettre dans method
        ui_main_menu_parent.SetActive(false);

        return;
    }

    /// <summary>
    /// Called when the player wins the level.
    /// </summary>
    public void LevelSuccess()
    {
        Debug.LogWarning("<b>LEVEL SUCCESS</b>");

        // change game state
        game_state = enum_GameState.endmenu;

        return;
    }

    /// <summary>
    /// Called when the player fails the level.
    /// </summary>
    public void LevelFailure()
    {
        Debug.LogWarning("<b>LEVEL FAILURE</b>");

        // change game state
        game_state = enum_GameState.endmenu;

        return;
    }

    /// <summary>
    /// Called when a tree leaves the detection zone without being cut.
    /// </summary>
    public void OnTreeMissed(GameObject missed_object)
    {
        Debug.LogWarning("Tree missed!");

        FailsCount += 1;
        RemainingTrees -= 1;
        ScoreManager.instance.ResetCombo();
        Destroy(missed_object, 1.0f);

        return;
    }

// = = =

}
