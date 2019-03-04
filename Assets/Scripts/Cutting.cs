using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public static Cutting cuttingScript;

    // Start is called before the first frame update
    void Awake()
    {
        if (cuttingScript == null)
        {
            cuttingScript = this;
        }
        else
        {
            Destroy(this);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
