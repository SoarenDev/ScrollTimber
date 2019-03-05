using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    public static ScrollingManager instance;
    
// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    public bool             is_scrolling_disabled           = false;

[Space(10)][Header("References")]
    public GameObject       parent_scroll_cylinder_ref      ;

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



// = = =

}
