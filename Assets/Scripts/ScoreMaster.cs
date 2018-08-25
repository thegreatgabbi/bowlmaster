using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    public enum Action {NEWFRAME, MIDFRAME, STRIKE1, STRIKE2, SPARE}

    // Return a list of individual frame scores, NOT cumulative
    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();
        Action prevAction = Action.NEWFRAME;
        int intermediateValue = 0;

        for (int i = 1; i <= rolls.Count; i++)
        {
            if (prevAction == Action.STRIKE1)
            {
                intermediateValue = rolls[i - 1];
                prevAction = Action.STRIKE2;
            }
            else if (prevAction == Action.STRIKE2)
            {
                frameList.Add(rolls[i - 1] + intermediateValue + 10); // prev frame score
                frameList.Add(rolls[i - 1] + intermediateValue); // current frame score
                intermediateValue = 0;
                prevAction = Action.NEWFRAME;
            }
            else if (prevAction == Action.SPARE)
            {
                frameList.Add(rolls[i - 1] + 10);
                intermediateValue = rolls[i - 1];
                prevAction = Action.MIDFRAME;
            }
            else if (prevAction == Action.NEWFRAME)
            {
                if (rolls[i - 1] == 10)
                { // got strike
                    intermediateValue = 0;
                    prevAction = Action.STRIKE1;
                }
                else
                {
                    intermediateValue = rolls[i - 1];
                    prevAction = Action.MIDFRAME;
                }
            } else {
                if (rolls[i - 1] + intermediateValue == 10)
                { // got spare
                    intermediateValue = 0;
                    prevAction = Action.SPARE;
                }
                else
                {
                    frameList.Add(rolls[i - 1] + intermediateValue);
                    intermediateValue = 0;
                    prevAction = Action.NEWFRAME;
                }
            }
        }
        return frameList;
    }

    // Returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative(List<int> rolls) {
        int score = 0;
        List<int> scoreList = new List<int>();
        foreach (int roll in rolls) {
            score += roll;
            scoreList.Add(score);
        }
        return scoreList;
    }
}
