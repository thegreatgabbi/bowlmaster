using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Ball ball;
    private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
        if (FindObjectOfType<Ball>()) {
            ball = FindObjectOfType<Ball>();
        } else {
            Debug.LogError("Please create a Ball object");
        }

        cameraOffset = transform.position - ball.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        // stop camera at headpin
        if (ball.transform.position.z <= 1829f) {
			transform.position = ball.transform.position + cameraOffset;
        }
	}
}
