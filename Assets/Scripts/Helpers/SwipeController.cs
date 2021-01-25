﻿using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool isDragging, isMobilePlatform;
    private Vector2 tapPoint, swipeDelta;
    private Vector2 curMousePosition, prevMousePosition = Vector2.zero;

    public delegate void OnSwipeInput(SwipeType type, float delta);
    public static event OnSwipeInput SwipeEvent;

    public enum SwipeType
    {
        LEFT,
        RIGHT
    }

    private void Awake() 
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            isMobilePlatform = false;
        #else
            isMobilePlatform = true;
        #endif
    }

    private void Update()
    {
        if(!isMobilePlatform)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                tapPoint = Input.mousePosition;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if(Input.touchCount > 0)
            {
                if(Input.touches[0].phase == TouchPhase.Began)
                {
                    isDragging = true;
                    tapPoint = Input.touches[0].position; 
                }
                else if(Input.touches[0].phase == TouchPhase.Canceled ||
                        Input.touches[0].phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }
    }

    private void FixedUpdate() {
        CalculateSwipe();
    }

    private void CalculateSwipe()
    {
        curMousePosition = (Vector2)Input.mousePosition;

        swipeDelta = Vector2.zero;

        if(isDragging)
        {
            if(!isMobilePlatform && Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - tapPoint;
            else if(Input.touchCount > 0)
                swipeDelta = Input.touches[0].position - tapPoint;
        }

        if(Player.alive)
        {
            if(swipeDelta.x < 0)
                SwipeEvent(SwipeType.LEFT, (prevMousePosition.x - curMousePosition.x) * Time.fixedDeltaTime * 38);
                
            if(swipeDelta.x > 0) 
                SwipeEvent(SwipeType.RIGHT, (curMousePosition.x - prevMousePosition.x) * Time.fixedDeltaTime * 38);  
        }
        
        prevMousePosition = curMousePosition;  
    }

    private void ResetSwipe()
    {
        isDragging = false;
        tapPoint = swipeDelta = Vector2.zero;
    }

}
