using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ground_behavior : MonoBehaviour
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Gameplay")]
    public  float       scroll_speed            ;
       
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // parent cylinder scrolling
        if (ScrollingManager.instance.is_scrolling_disabled == false)
        {
            ScrollCylinder(scroll_speed*Time.deltaTime);
        }
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    /// <summary>
    /// Rotates the parent cylinder gameobject to represent the scrolling of the ground.
    /// </summary>
    public void ScrollCylinder(float value)
    {
        ScrollingManager.instance.parent_scroll_cylinder_ref.transform.Rotate(-value, 0, 0);
        return;
    }

// = = =

}
