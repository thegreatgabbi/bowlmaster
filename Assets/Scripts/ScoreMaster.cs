using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    // Return a list of individual frame scores, NOT cumulative
    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();
        int intermediateValue = -1;

        for (int i = 1; i <= rolls.Count; i++)
        {
            if (rolls[i - 1] == 10) // strike
            {
                intermediateValue = -1;
            }
            else if (intermediateValue == 10) { // mid-frame
                frameList.Add(rolls[i - 1] + intermediateValue);
                intermediateValue = rolls[i - 1];
            }
            else if (intermediateValue == -1) // mid-frame
            {
                intermediateValue = rolls[i - 1];
            } else { // end-frame
                if (rolls[i - 1] + intermediateValue == 10) {
                    // spare condition
                    intermediateValue = 10;
                } else {
                    frameList.Add(rolls[i - 1] + intermediateValue);
                    intermediateValue = -1;
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
