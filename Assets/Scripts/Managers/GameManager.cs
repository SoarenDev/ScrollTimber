using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_GameState
{
    startmenu,
    paused,
    running,
    successmenu,
    failuremenu
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Global variables")]
    public  int                         total_money                         = 0;

[Space(10)][Header("Gameplay")]
    public  int                         allowed_fails                       = 1;

[Space(10)][Header("Runtime")]
    public  enum_GameState              game_state                          ;
    public  int                         fails_count                         ;
    public  int                         remaining_trees                     ;

[Space(10)][Header("References")]
    public  scr_ui_updater_behavior     ui_behavior_ref                     ;
    public  Animator                    car_animator_ref                    ;
    public  GameObject                  scroll_cylinder_parent              ;
    public  GameObject                  ui_main_menu_parent                 ;
    public  GameObject                  ui_success_menu_parent              ;
    public  GameObject                  ui_failure_menu_parent              ;
    public  GameObject                  ui_score_container                  ;
    public  GameObject                  ui_total_money_container            ;

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
    /// Changes the actual game state and reverberates the effects to the script.
    /// </summary>
    public void ChangeGameState(enum_GameState new_state)
    {
        // update game_state variable
        game_state = new_state;

        // reverberate needed changes
        switch (new_state)
        {
            case enum_GameState.startmenu:
                
                // update ui visibility
                ui_score_container.SetActive(false); 
                ui_main_menu_parent.SetActive(true);
                ui_success_menu_parent.SetActive(false);
                ui_failure_menu_parent.SetActive(false);
                ui_total_money_container.SetActive(true);

                // change car anim state
                car_animator_ref.SetBool("isMoving", true);

                // update ui values
                ui_behavior_ref.UpdateActualLevelLabelUI();

            break;

            case enum_GameState.running:

                // change ui visibility
                ui_score_container.SetActive(true); 
                ui_main_menu_parent.SetActive(false);
                ui_success_menu_parent.SetActive(false);
                ui_failure_menu_parent.SetActive(false);
                ui_total_money_container.SetActive(false);

                // change car anim state
                car_animator_ref.SetBool("isMoving", true);

            break;

            case enum_GameState.successmenu:

                // update ui visibility
                ui_success_menu_parent.SetActive(true);
                ui_total_money_container.SetActive(true);

                // change car anim state
                car_animator_ref.SetBool("isMoving", false);

            break;

            case enum_GameState.failuremenu:

                // update ui visibility
                ui_failure_menu_parent.SetActive(true);
                ui_total_money_container.SetActive(true);

                // change car anim state
                car_animator_ref.SetBool("isMoving", false);

            break;

            case enum_GameState.paused:
                ui_total_money_container.SetActive(true);
                
            break;
        }

        return;
    }


    /// <summary>
    /// Called when the player press the "Play" button; launches the actual level.
    /// </summary>
    public void StartGame()
    {
        Debug.LogWarning("<b>!!! START GAME !!!</b>");

        LevelsManager.instance.ResetLevelState();
        ScrollingManager.instance.level_generation_script_ref.StartLevelGeneration();

        // change game state
        ChangeGameState(enum_GameState.running);
        
        return;
    }

    /// <summary>
    /// .
    /// </summary>
    public void ReturnToMenu()
    {
        // ensure the stopping of actual level generation
        ScrollingManager.instance.level_generation_script_ref.ResetLevelGeneration();

        // change game state
        ChangeGameState(enum_GameState.startmenu);

        return;
    }

    /// <summary>
    /// Called when the player wins the level.
    /// </summary>
    public void LevelSuccess()
    {
        Debug.LogWarning("<b>LEVEL SUCCESS</b>");

        // change game state
        ChangeGameState(enum_GameState.successmenu);

        // register and cash money
        ScoreManager.instance.AddScoreToTotalMoney();

        // reach next level
        LevelsManager.instance.MoveToNextLevel();

        return;
    }

    /// <summary>
    /// Called when the player fails the level.
    /// </summary>
    public void LevelFailure()
    {
        Debug.LogWarning("<b>LEVEL FAILURE</b>");

        // change game state
        ChangeGameState(enum_GameState.failuremenu);

        // change car anim state
        car_animator_ref.SetBool("isMoving", false);

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

        // remove tree from generated list
        ScrollingManager.instance.level_generation_script_ref.generated_trees.Remove(missed_object);

        Destroy(missed_object, 1.0f);

        return;
    }

// = = =

}
