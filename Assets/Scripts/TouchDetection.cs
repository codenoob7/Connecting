using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    private int _FingerLeft = -1;      // -1 to cancel the touch event
    private int _FingerRight = -1;
    
    [SerializeField] private Rigidbody2D _lRb;
    [SerializeField] private Rigidbody2D _rRb;

    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _distToDetect;

    private bool _LcanMove;
    private bool _RcanMove;
    private bool _IsSwiped;
    
    private Vector2 _leftLastPos;
    private Vector2 _rightLastPos;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //First check count of touch
        if (Input.touchCount > 0) 
        {
            foreach (Touch touch in Input.touches) 
            {
                //For left half screen
                if (touch.phase  == TouchPhase.Began && touch.position.x <= Screen.width/2f && _FingerLeft == -1) 
                {
                    Debug.Log("Left Touch....");
                    _LcanMove = true;

                    _leftLastPos = touch.position;
                    _FingerLeft = touch.fingerId; //store Id finger
                }
                else if (touch.phase == TouchPhase.Moved && touch.position.x <= Screen.width/2f)
                {
                    float dist = Mathf.Abs(touch.position.x - _leftLastPos.x);

                    if (dist > _distToDetect && !_IsSwiped)
                    {
                        Debug.Log("Swiped Left...");
                        _IsSwiped = true;
                    }
                }
                
                //For right half screen
                if (touch.phase  == TouchPhase.Began && touch.position.x > Screen.width/2f && _FingerRight == -1) 
                {
                    Debug.Log("Right Touch...");
                    _RcanMove = true;
                    _FingerRight = touch.fingerId;
                }
                
                //correct end of touch
                if (touch.phase == TouchPhase.Ended) 
                { 
                    //check id finger for end touch
                    if(touch.fingerId == _FingerLeft) 
                    { 
                        Debug.Log("Left Touch Ended...");
                        _LcanMove = false;
                        _IsSwiped = false;
                        _FingerLeft = -1;
                    } 
                    else if(touch.fingerId == _FingerRight) 
                    {
                        Debug.Log(("Right Touch Ended"));
                        _RcanMove = false;
                        _FingerRight = -1;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_LcanMove)
        {
            Move(_lRb);
        }
        else
        {
            _lRb.velocity = Vector2.zero;
        }
        
        if (_RcanMove)
        {
            Move(_rRb);
        }
        else
        {
            _rRb.velocity = Vector2.zero;
        }
    }


    private void Move(Rigidbody2D rb)
    {
        rb.velocity = new Vector2(0, _MoveSpeed);
        //_Connector.Translate(0,_MoveSpeed * Time.deltaTime,0);
    }
}
