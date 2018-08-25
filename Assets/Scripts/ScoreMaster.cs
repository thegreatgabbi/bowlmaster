using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    // Return a list of individual frame scores, NOT cumulative
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();

        for (int i = 1; i <= rolls.Count; i++) {
            if (i != 1 && i % 2 == 0) {
                frameList.Add(rolls[i-1] + rolls[i-2]);
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
