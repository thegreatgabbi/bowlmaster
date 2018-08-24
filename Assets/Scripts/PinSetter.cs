using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public float distToRaise = 40f;
    public GameObject pinSet;

    private int lastStandingCount = -1;
    private Text standingPinsText;
    private bool ballOutOfPlay = false;
    private int lastSettledCount = 10;
    private Ball ball;
    private float lastChangeTime;

    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        FindStandingPinsText();
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
	}

    private void FindStandingPinsText()
    {
        if (GameObject.Find("StandingPinsText"))
        {
            standingPinsText = GameObject.Find("StandingPinsText").GetComponent<Text>();
        }
        else
        {
            Debug.LogError("Please create a StandingPins Text object");
        }
    }

    // Update is called once per frame
	void Update () {
        standingPinsText.text = CountStanding().ToString();
        if (ballOutOfPlay) {
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
        ActionMaster actionMaster = new ActionMaster();

        int pinFall = lastSettledCount - CountStanding();
        lastSettledCount = CountStanding();
        ActionMaster.Action action = actionMaster.Bowl(pinFall);

        // do stuff with swiper
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        } else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        } else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end game yet.");
        }

        // to start another bowl
        ball.Reset();
        standingPinsText.color = Color.green;
        ballOutOfPlay = false;
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
        Instantiate(pinSet, new Vector3(0, 0, 0), Quaternion.identity);
	}

    public void SetBallOutPlay() {
        ballOutOfPlay = true;
    }
}
