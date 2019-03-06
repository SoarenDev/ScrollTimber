using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingManager : MonoBehaviour
{

    public static TouchingManager touchingManagerInstance;

    private Camera mainCamera;
    private Touch touch;
    private bool canTouch;

    public Vector2 touchingPos;
    public bool publicTouchingBool;
    private bool isTouching;

    public bool isGamePaused = false; // a relinker proprement avec le game manager


    // Start is called before the first frame update
    void Awake()
    {
        if (touchingManagerInstance == null)
        {
            touchingManagerInstance = this;
        }
        else
        {
            Destroy(this);
        }
        canTouch = true;            //sert de sécurité par rapport à la detection de "phase" de touche
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        publicTouchingBool = CheckIfTouching();
        print(publicTouchingBool);
    }


    public bool CheckIfTouching()
    {

        if (Input.touchCount > 0 && !isGamePaused)
        {
            touch = Input.GetTouch(0);    //on récupère l'input du PREMIER doigt qui touche (index 0)
            if (touch.phase == TouchPhase.Began && canTouch)
            {
                touchingPos = touch.position;
                isTouching = true;
                canTouch = false;
            }
            else if (touch.phase == TouchPhase.Ended && !canTouch)
            {
                isTouching = false;
                touchingPos = -Vector2.one;
                canTouch = true ;
            }
        }

        return isTouching;
    }
}
