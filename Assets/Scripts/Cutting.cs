using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public static Cutting cuttingScript;

    public Vector2 startPos;
    public Vector2 endPos;

    private bool isSwiping;
    private bool canCut;

    Rect topLeft = new Rect(0, 0, Screen.width / 2, Screen.height / 2);
    Rect bottomLeft = new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2);
    Rect topRight = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2);
    Rect bottomRight = new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2);


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

        isSwiping = false;
        canCut = true;
    }


    // Update is called once per frame
    void Update()
    {
        CheckIfSwiping();
        


        if (isSwiping && canCut)
        {
            //if (ConcernedTree.GetComponent<TreeBehaviour>().IsTreeCutable)
            //{
            //    if (GameManager.SwipeData.middlePosition == ConcernedTree.weakPointPosition)
            //    {
            //        StartCoroutine(PerfectCut());
            //    }
            //    else if (GameManager.SwipeData.middlePosition == ConcernedTree.badPointPosition)
            //    {
            //        StartCoroutine(ErrorCut());
            //    }
            //    else
            //    {
            //        StartCoroutine(NormalCut());
            //    }
            //}
            //else
            //{
            //    StartCoroutine(NullCut());
            //}
        }

        
    }

    private bool CheckIfSwiping()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            print(touchPos);
            if (topLeft.Contains(touchPos))
            {
                Debug.Log("topLeft touched");
            }
            if (bottomLeft.Contains(touchPos))
            {
                Debug.Log("bottomLeft touched");
            }
            if (topRight.Contains(touchPos))
            {
                Debug.Log("topRight touched");
            }
            if (bottomRight.Contains(touchPos))
            {
                Debug.Log("bottomRight touched");
            }
        }

        return false;
    }

    private IEnumerator PerfectCut()
    {
        canCut = false;
        // event
        yield return null;
        canCut = true;
    }

    private IEnumerator NormalCut()
    {
        canCut = false;
        // event
        yield return null;
        canCut = true;
    }

    private IEnumerator NullCut()
    {
        canCut = false;
        // event
        yield return null;
        canCut = true;
    }

    private IEnumerator ErrorCut()
    {
        canCut = false;
        // event
        yield return null;
        canCut = true;
    }
}
