using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallDragLaunch : MonoBehaviour
{

    private Ball ball;

    private Vector3 startPos, endPos;
    private float startTime, endTime;

    // Use this for initialization
    void Start()
    {
        ball = GetComponent<Ball>();
    }

    public void DragStart()
    {
        if (!ball.inPlay)
        {
            startPos = Input.mousePosition;
            startTime = Time.time;
        }
    }

    public void DragEnd()
    {
        if (!ball.inPlay) {
            endPos = Input.mousePosition;
            endTime = Time.time;
            
            float dragDuration = endTime - startTime;
            
            float launchSpeedX = (endPos.x - startPos.x) / dragDuration;
            float launchSpeedZ = (endPos.y - startPos.y) / dragDuration;
            
            Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
            
            ball.Launch(launchVelocity);
        }
    }

    public void MoveStart(float amount)
    {
        if (!ball.inPlay) {
			ball.transform.Translate(new Vector3(amount, 0, 0));
        }

    }
}
