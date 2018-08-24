using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

   
    public GameObject pinSet;
    public float distToRaise = 40f;

    private Animator animator;
    private PinCounter pinCounter;
    private Text standingPinsText;
    private bool ballOutOfPlay = false;
    private int lastSettledCount = 10;


    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        pinCounter = FindObjectOfType<PinCounter>();
        standingPinsText = GameObject.Find("StandingPinsText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballOutOfPlay)
        {
            pinCounter.CheckStanding();
        }
    }


    public void RaisePins() {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }
	
    public void Animate(ActionMaster.Action action) {

        int pinFall = lastSettledCount - CountStanding();
        lastSettledCount = CountStanding();

        // do stuff with swiper
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        } else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            pinCounter.ResetLastSettledCount();
        } else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            pinCounter.ResetLastSettledCount();
        }
        else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end game yet.");
        }
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
