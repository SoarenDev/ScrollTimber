using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

public static GameManager instance;

// = = = [ VARIABLES DEFINITION ] = = =


       
// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

    void Awake()
    {
        if ( instance == null )
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
