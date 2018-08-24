using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    private GameManager gameManager;
    private Text standingPinsText;
    private Ball ball;
    private bool ballRolled = false;

    public int lastStandingCount = -1;
    public int lastSettledCount = 10;
    public float lastChangeTime;


    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
        standingPinsText = GameObject.Find("StandingPinsText").GetComponent<Text>();
        ball = FindObjectOfType<Ball>();
    }

    public void CheckStanding()
    {
        // Update the lastStandingCount
        if (lastStandingCount != CountStanding())
        {
            lastStandingCount = CountStanding();
            lastChangeTime = Time.time;
        }
        else if (Time.time - lastChangeTime >= 3f)
        {
            PinsHaveSettled();
            ballRolled = false;
        }
    }

    public void ResetLastSettledCount()
    {
        lastSettledCount = 10;
    }

    private void Update()
    {
        standingPinsText.text = CountStanding().ToString();
        if (ballRolled) {
            CheckStanding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ball>())
        {
            ballRolled = true;
            standingPinsText.color = Color.red;
        }
    }

    private int CountStanding()
    {
        int standingPins = 0;
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standingPins++;
            }
        }
        return standingPins;
    }

    private void PinsHaveSettled()
    {
        int pinFall = lastSettledCount - CountStanding();
        lastSettledCount = CountStanding();

        gameManager.Bowl(pinFall);
        ballRolled = false;

        standingPinsText.color = Color.green;
        lastStandingCount = -1; // Indicates pins have settled, and ball not back in box
    }
}
