using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaneBox : MonoBehaviour {

    PinSetter pinSetter;
    Text standingPinsText;

    private void Start()
    {
        pinSetter = FindObjectOfType<PinSetter>();
        standingPinsText = GameObject.Find("StandingPinsText").GetComponent<Text>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Ball>()) {
            pinSetter.SetBallOutPlay();
            standingPinsText.color = Color.red;
        }
        Debug.Log("Ball left box");
    }
}
