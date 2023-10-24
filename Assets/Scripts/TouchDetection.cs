using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetection : MonoBehaviour
{
    private int _FingerLeft = -1;      // -1 to cancel the touch event
    private int _FingerRight = -1;
    
    [SerializeField] private Rigidbody2D _lRb;
    [SerializeField] private Rigidbody2D _rRb;

    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _distToDetect;

    [SerializeField] private Transform _Camera;

    private bool _LcanMove;
    private bool _RcanMove;
    
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


        if (_Camera.position.y > _lRb.transform.localPosition.y + 10f && _Camera.transform.position.y > _rRb.transform.localPosition.y + 10f)
        {
            Debug.Log("Loose...");
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

    public void StopMovement()
    {
        _lRb.velocity = Vector2.zero;
        _rRb.velocity = Vector2.zero;
    }
}
