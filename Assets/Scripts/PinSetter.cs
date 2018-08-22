using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    Text standingPinsText;
    int standingPins;

	// Use this for initialization
	void Start ()
    {
        FindStandingPinsText();
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
	}

    int CountStanding() {
        standingPins = 0;
        foreach(Pin pin in FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standingPins++;
            }
        }
        return standingPins;
    }
}
