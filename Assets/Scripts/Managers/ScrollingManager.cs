using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    public static ScrollingManager instance;
    
// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    public bool                                 is_scrolling_disabled           = false;

[Space(10)][Header("References")]
    public GameObject                           parent_scroll_cylinder_ref      ;
    public scr_level_generation_behavior        level_generation_script_ref     ;

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
        if (level_generation_script_ref != null)
        {  }
        else
        { Debug.LogError("<b>Level generation script</b> not binded; unable to initialize level!"); }
    }

// = = =

// = = = [ CLASS METHODS ] = = =



// = = =

}
