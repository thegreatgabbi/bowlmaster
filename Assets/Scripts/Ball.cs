using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Vector3 launchVelocity;

    private Rigidbody rigidbody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        Launch();
    }

    private void Launch()
    {
        rigidbody.velocity = launchVelocity;
        audioSource.Play();
    }

    // Update is called once per frame
	void Update () {
		
	}
}
