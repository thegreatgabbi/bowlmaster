using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static List<int> pins = new List<int>();

    private ActionMaster actionMaster = new ActionMaster();
    private Animator animator;
    private Ball ball;
    private PinCounter pinCounter;
    private PinSetter pinSetter;

    private void Start()
    {
        animator = GameObject.Find("Pin Setter").GetComponent<Animator>();
        ball = FindObjectOfType<Ball>();
        pinCounter = FindObjectOfType<PinCounter>();
        pinSetter = FindObjectOfType<PinSetter>();
    }

    public void Bowl(int pinFall) {
        // add pinFall to pins
        pins.Add(pinFall);

        int[] pinArr = pins.ToArray();
        Debug.Log(string.Join(",", pinArr.Select(x => x.ToString()).ToArray()));

        // Call ScoreMaster to get scores
        List<int> scores = ScoreMaster.ScoreFrames(pins);

        // Call ActionMaster
        ActionMaster.Action action = ActionMaster.NextAction(pins);
        Debug.Log("Next Action: " + action);

        // Call PinSetter/Animator
        pinSetter.Animate(action);

        // TODO: Call ScoreDisplay to Display scores

        // Call ball to Reset
        ball.Reset();
    }
}
