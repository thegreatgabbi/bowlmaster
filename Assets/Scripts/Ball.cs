using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public Vector3 launchVelocity;
    public bool inPlay = false;

    private Rigidbody myRigidbody;
    private AudioSource audioSource;
    private Vector3 initialPos;


	// Use this for initialization
	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        myRigidbody.useGravity = false;
        initialPos = transform.position;
    }

    public void Launch(Vector3 launchVelocity)
    {
        inPlay = true;
        myRigidbody.velocity = launchVelocity;
        myRigidbody.useGravity = true;
        audioSource.Play();
    }

	public void Reset()
	{
        transform.position = initialPos;
        transform.rotation = Quaternion.identity; // to reset the ball's rotation
        myRigidbody.velocity = new Vector3 (0, 0, 0);
        myRigidbody.angularVelocity = new Vector3(0, 0, 0);
        myRigidbody.useGravity = false;
        inPlay = false;
	}


}
