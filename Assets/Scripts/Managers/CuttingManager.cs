using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cutStateEnum
{
    Failed,
    Success,
    Perfect        
}

public class CuttingManager : MonoBehaviour
{
    public static CuttingManager cuttingManagerInstance;

    Rect bottomLeft = new Rect(0, 0, Screen.width / 2, Screen.height / 2);
    Rect topLeft = new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2);
    Rect bottomRight = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2);
    Rect topRight = new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2);


    public Texture tex;
    public GameObject pointingTool;
    private Camera mainCamera;
    private Touch touch;

    public float swipeWindowTime = 0.5f; // The time you have to trace a swipe
    public float minimumSwipeLenght = 200f; // The minimum length (en pixels) of the vector made with swipe needed to be valid
    public float timeBetweenSwipes = 0.2f;
    public float vectorComparisonOffset = 10;
    public float minimumDeltaSwipe = 30f;
    

    bool canSwipe;
    bool isPlayingCoroutine;
    Vector2 actualSwipe;
    bool hasAStartPosition;
    bool hasAnActualSwipe;
    public bool treeIsInZone;

    public Vector2 startSwipePosition;
    public Vector2 endSwipePosition;
    private Vector2 swipeDirection;
    public Vector2 publicSwipeVectorDirection;
    public scr_tree_behavior detectedTree;
    public Vector2 treeVector;

    private Vector2 swipeVector;

    public bool isGamePaused = false;       // a relinker proprement avec le game manager

    // = = = [ VARIABLES DEFINITION ] = = =


    // = = =

    // = = = [ MONOBEHAVIOR METHODS ] = = =

    void Awake()
    {
        if (cuttingManagerInstance == null)
        {
            cuttingManagerInstance = this;
        }
        else
        {
            Destroy(this);
        }
        canSwipe = true;
        mainCamera = Camera.main;
        isPlayingCoroutine = false;
    }

    private void OnGUI()
    {
        //foreach (Touch touch in Input.touches)
        //{
        //    string message = "";
        //    message += "ID: " + touch.fingerId + "\n";
        //    message += "Phase: " + touch.phase.ToString() + "\n";
        //    message += "TapCount: " + touch.tapCount + "\n";
        //    message += "Pos X: " + touch.position.x + "\n";
        //    message += "Pos Y: " + touch.position.y + "\n";

        //    int num = touch.fingerId;
        //    GUI.Label(new Rect(0 + 130 * num, 0, 120, 100), message);
        //}

    }

    private void Update()
    {
        if (treeIsInZone)
        {
            treeVector = detectedTree.weak_point_direction;
            if (hasAnActualSwipe)
            {
                detectedTree.CutTree(ComparingSwipeAndTree());
            }
        }

        Debug.DrawRay(Vector2.zero, treeVector * 5, Color.green);

        publicSwipeVectorDirection = GetSwipingDirection();

    }

    // = = =

    // = = = [ CLASS METHODS ] = = =


    public cutStateEnum ComparingSwipeAndTree()
    {
        float playerSwipeAngle = Vector2.SignedAngle(Vector2.right, publicSwipeVectorDirection);
        float treeSwipeAngle = Vector2.SignedAngle(Vector2.right, treeVector);

        print("Comparing " + playerSwipeAngle + " with " + treeSwipeAngle);

        if (playerSwipeAngle > (treeSwipeAngle - vectorComparisonOffset))
        {
            if (playerSwipeAngle < (treeSwipeAngle + vectorComparisonOffset))
            {
                if (treeVector.y < 0)
                {
                    //print("Y is negative, and angle is valid");
                    return cutStateEnum.Perfect;
                }
                else if (treeVector.y >= 0)
                {
                    //print("Y is positive, and angle valid");
                    return cutStateEnum.Perfect;
                }
            }
        }

        return cutStateEnum.Success; // si on a pas eu un autre return, on joue celui-ci de base
    }


    public Vector2 GetSwipingDirection()
    {
        if (Input.touchCount > 0 && !isGamePaused)
        {
            touch = Input.GetTouch(0);    //on récupère l'input du PREMIER doigt qui touche (index 0)
            if (!hasAnActualSwipe)
            {
                actualSwipe = SwipingWithTime();

                if (!hasAnActualSwipe && touch.phase == TouchPhase.Ended || touch.deltaPosition.magnitude < minimumDeltaSwipe)
                {
                    endSwipePosition = touch.position;
                    swipeVector = endSwipePosition - startSwipePosition;

                    //print("length = " + swipeVector.magnitude);

                    if (swipeVector.magnitude >= minimumSwipeLenght)    // si le vector créé est assez long pour être considéré valide
                    {
                        swipeDirection = DirectionConverter(swipeVector.normalized);
                        Debug.DrawRay(Vector3.zero, swipeDirection, Color.red, 1f);

                        hasAnActualSwipe = true;
                        isPlayingCoroutine = false;
                        StartCoroutine(NewSwipeDelay());
                    }
                }
            }
            else
            {
                StartCoroutine(NewSwipeDelay());
            }
        }
        else
        {
            hasAStartPosition = false;
            
            isPlayingCoroutine = false;
        }

        return swipeDirection;
    }

    private void PositionTouching()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            //print(touchPos);
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
        return;
    }

    private Vector2 SwipingWithTime()
    {
        if (!isPlayingCoroutine)
        {
            StartCoroutine(TimeToSwipe());
            isPlayingCoroutine = true;
        }

        Vector2 touchPos = touch.position;
        if (!hasAStartPosition)
        {
            startSwipePosition = touchPos;
            hasAStartPosition = true;
        }

        if (canSwipe)                            //Pendant un temps donné (cf TimeToSwipe) on va récupérer la position du doigt sur l'écran pour en faire le point de fin du swipe
        {

            //Vector2 transformedPos = new Vector2(touchPos.x / 180, touchPos.y / 170); // position du doigt en coordonnées monde (puisque le capté de base est en pixel)
            //print("touchPos : " + touchPos + "Which becomes : " + transformedPos);
            //pointingTool.transform.position = transformedPos;                         // La position du debugPositionTool est suit celle du doigt

            Vector2 tempEndSwipePosition = touch.position;

            Vector2 tempSwipeVector = (tempEndSwipePosition - startSwipePosition);      //vector entre le point de départ et la position du doigt actuelle

            if (tempSwipeVector.magnitude > swipeVector.magnitude)                      // si jamais le vector est entre le point de départ et l'actuelle position du doigt est plus grand que le plus grand vector storé jusque-là
            {
                endSwipePosition = tempEndSwipePosition;                                //la position du doigt devient la nouvelle fin de swipe
                swipeVector = (endSwipePosition - startSwipePosition);
            }

            
        }
        else if (!canSwipe)
        {
            if (swipeVector.magnitude >= minimumSwipeLenght)                        // si le vector créé est assez long pour être considéré valide
            {
                swipeDirection = DirectionConverter(swipeVector.normalized);        // la direction du swipe normalisée et orientée
                //Debug.DrawRay(startSwipePosition, swipeDirection * 3, Color.red, 1f);
                hasAnActualSwipe = true;
                isPlayingCoroutine = false;

                return swipeDirection;
            }
        }
        return Vector2.zero;
    }


    private IEnumerator TimeToSwipe()
    {
        canSwipe = true;
        //print("step1");

        yield return new WaitForSeconds(swipeWindowTime);

        //print("step2");
        canSwipe = false;
    }

    private IEnumerator NewSwipeDelay()
    {
        //print("waiting");
        yield return new WaitForSeconds(timeBetweenSwipes);
        startSwipePosition = Vector2.zero;
        endSwipePosition = Vector2.zero;
        hasAnActualSwipe = false;
        hasAStartPosition = false;
    }

    private Vector2 DirectionConverter(Vector2 retrievedDirection)
    {
        Vector2 convertedVectorDirection = Vector2.zero;
        float tempY = 0;
        float tempX = 0;
        //print("retrievedDirection :" + retrievedDirection);
        if (retrievedDirection.x < 0)
        {
            tempX = -retrievedDirection.x;
            tempY = -retrievedDirection.y;
        }
        else
        {
            tempX = retrievedDirection.x;
            tempY = retrievedDirection.y;
        }
        convertedVectorDirection = new Vector2(tempX, tempY);
        //print(convertedVectorDirection);
        return convertedVectorDirection;
    }

    public void UnregisterTree()
    {
        detectedTree = null;
        treeIsInZone = false;
    }

    // = = =
}
