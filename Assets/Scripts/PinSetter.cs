using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public int lastStandingCount = -1;
    public Text standingPinsText;
    public float distToRaise = 40f;
    public GameObject pinSet;

    private Ball ball;
    private float lastChangeTime;
    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start ()
    {
        FindStandingPinsText();
        ball = GameObject.FindObjectOfType<Ball>();
	}

    private void FindStandingPinsText()
    {
        if (GameObject.Find("StandingPinsText"))
        {
            standingPinsText = GameObject.Find("StandingPinsText").GetComponent<Text>();
        }
        else
        {
            Debug.Log("Please create a StandingPins Text object");
        }
    }

    // Update is called once per frame
	void Update () {
        standingPinsText.text = CountStanding().ToString();
        if (ballEnteredBox) {
            CheckStanding();
        }
	}

    void CheckStanding() {
        // Update the lastStandingCount
        if (lastStandingCount != CountStanding()) {
            lastStandingCount = CountStanding();
            lastChangeTime = Time.time;
        } else if ((Time.time - lastChangeTime >= 3f)) {
            PinsHaveSettled();
        }
        // Call PinsHaveSettled() when they have

    }

    void PinsHaveSettled() {
        ball.Reset();
        standingPinsText.color = Color.green;
        ballEnteredBox = false;
        lastStandingCount = -1; // Indicates pin have settled, and ball not back in box
    }

    int CountStanding() {
        int standingPins = 0;
        foreach(Pin pin in FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standingPins++;
            }
        }
        return standingPins;
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.GetComponent<Ball>()) {
			standingPinsText.color = Color.red;
            ballEnteredBox = true;
        }
	}

	private void OnTriggerExit(Collider other)
	{
        GameObject thingLeft = other.gameObject;

        if (thingLeft.GetComponent<Pin>()) {
            Destroy(thingLeft);
        }
	}

    public void RaisePins() {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins() {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

	public void RenewPins()
	{
        GameObject pins = Instantiate(pinSet, new Vector3(0, 0, 0), Quaternion.identity);
	}
}
