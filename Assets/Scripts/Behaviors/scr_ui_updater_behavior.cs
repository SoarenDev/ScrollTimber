using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scr_ui_updater_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Score")]
    public  TextMeshProUGUI            txt_score                   ;
    public  TextMeshProUGUI            txt_combo                   ;
       
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        UpdateScoreUI("0");
        UpdateComboUI("0");
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Updates the player score UI value.
    /// </summary>
    public void UpdateScoreUI(string text)
    {
        txt_score.text = text;

        return;
    }

    /// <summary>
    /// Updates the player combo UI value.
    /// </summary>
    public void UpdateComboUI(string text)
    {
        txt_combo.text = "+ " + text;

        return;
    }

// = = =

}
