using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBehaviour : MonoBehaviour
{
    bool localTreeIsInZone;
    Vector2 localTreeVector;
    GameObject localDetectedTree;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            print("dodo");
            localDetectedTree = other.gameObject;
            localTreeIsInZone = true;

            CuttingManager.cuttingManagerInstance.detectedTree = other.gameObject.GetComponent<scr_tree_behavior>();
            CuttingManager.cuttingManagerInstance.treeIsInZone = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (localDetectedTree != null && other.gameObject == localDetectedTree)
        {
            print("dada");
            localDetectedTree = null;
            localTreeIsInZone = false;

            CuttingManager.cuttingManagerInstance.detectedTree = null;
            CuttingManager.cuttingManagerInstance.treeIsInZone = false;
        }
    }
}
