﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {
        print(name + ": " + IsStanding());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsStanding()
    {
        print(name + ": " + transform.rotation.eulerAngles);

        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.x);

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}