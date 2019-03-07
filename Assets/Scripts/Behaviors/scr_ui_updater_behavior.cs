using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scr_ui_updater_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Score")]
    public  TextMeshProUGUI            txt_score                        ;
    public  Text                       txt_combo                        ;
    public  TextMeshProUGUI            txt_actual_level_name            ;
    public  TextMeshProUGUI            txt_actual_level_index           ;
    public  TextMeshProUGUI            txt_total_money                  ;

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        UpdateScoreUI("+0");
        UpdateComboUI("0");
        UpdateActualLevelLabelUI();
        UpdateTotalMoneyUI(GameManager.instance.total_money.ToString());
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Updates the player score UI value.
    /// </summary>
    public void UpdateScoreUI(string text)
    {
        txt_score.text = "+" + text;

        return;
    }

    /// <summary>
    /// Updates the player combo UI value.
    /// </summary>
    public void UpdateComboUI(string text)
    {
        if (ScoreManager.instance.actual_combo > 0)
        {
            txt_combo.enabled = true;
            txt_combo.text = "COMBO: " + text + "!";  
        }
        else
        {
            txt_combo.enabled = false;
        }

        return;
    }

    /// <summary>
    /// Updates the main menu's actual level's index and name UI value.
    /// </summary>
    public void UpdateActualLevelLabelUI()
    {
        txt_actual_level_index.text = "Level " + LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].id.ToString("00");
        txt_actual_level_name.text = LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].label.ToString();
        return;
    }

    /// <summary>
    /// Updates the player score UI value.
    /// </summary>
    public void UpdateTotalMoneyUI(string text)
    {
        txt_total_money.text = text;

        return;
    }

// = = =

}
