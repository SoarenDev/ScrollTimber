﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

// = = = [ VARIABLES DEFINITION ] = = =
    
    public  bool        isGamePaused        = false;

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
