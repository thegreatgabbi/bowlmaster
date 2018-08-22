using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Vector3 launchVelocity;

    private Rigidbody rigidbody;
    private AudioSource audioSource;
    public bool inPlay = false;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rigidbody.useGravity = false;
    }

    public void Launch(Vector3 launchVelocity)
    {
        rigidbody.velocity = launchVelocity;
        rigidbody.useGravity = true;
        audioSource.Play();
    }

    // Update is called once per frame
	void Update () {
		
	}
}
