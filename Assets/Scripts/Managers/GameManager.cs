using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Rect bottomLeft = new Rect(0, 0, Screen.width / 2, Screen.height / 2);
    Rect topLeft = new Rect(0, Screen.height / 2, Screen.width / 2, Screen.height / 2);
    Rect bottomRight = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height / 2);
    Rect topRight = new Rect(Screen.width / 2, Screen.height / 2, Screen.width / 2, Screen.height / 2);

    Rect leftCheckZone = new Rect((Screen.width / 9) * 6, (Screen.height / 16) * 7, (Screen.width / 9) * 1, (Screen.height / 16) * 4); //pour la pos de départ, on divise la largeur et la hauteur selon le ratio 16:9
    Rect rightCheckZone = new Rect((Screen.width / 9) * 8, (Screen.height / 16) * 7, (Screen.width / 9) * 1, (Screen.height / 16) * 4);


    public Texture tex;

    public Vector2 startSwipePosition;
    private bool touchedLeftZone;
    private bool touchedRightZone;

    public  bool        isGamePaused        = false;

    // = = = [ VARIABLES DEFINITION ] = = =

    public struct SwipeData
    {
        Vector2 startPosition;
        Vector2 endPosition;
        Vector2 middlePosition;
    }

    public SwipeData lastSwipedata;

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

        GUI.color = Color.red;
        GUI.DrawTexture(leftCheckZone, tex);
        GUI.DrawTexture(rightCheckZone, tex);
    }

    private void Update()
    {
        CheckIfSwiping();
    }

    // = = =

    // = = = [ CLASS METHODS ] = = =

    private void PositionTouching()
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

        return;
    }

    private bool CheckIfSwiping()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            if (leftCheckZone.Contains(touchPos))
            {
                startSwipePosition = touchPos; // on update la position de départ jusqu'à ce qu'elle quitte la zone de détection
                touchedLeftZone = true;
                Debug.Log("left zone touched");
            }
            else if (!leftCheckZone.Contains(touchPos) && touchedLeftZone)
            {

                Debug.Log("left zone quitted");
            }

            if (rightCheckZone.Contains(touchPos))
            {
                startSwipePosition = touchPos; // on update la position de départ jusqu'à ce qu'elle quitte la zone de détection
                touchedRightZone = true;
                Debug.Log("right zone touched");
            }
            else if (!rightCheckZone.Contains(touchPos) && touchedRightZone)
            {

                Debug.Log("right zone quitted");
            }
        }
        return false;
    }

    // = = =

}
