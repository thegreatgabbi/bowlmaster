using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    // Return a list of individual frame scores, NOT cumulative
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();

        for (int i = 1; i <= rolls.Count; i++) {
            if (i != 1 && i % 2 == 0) { // if end of frame (i.e. even bowls)
                // if strike on previous bowl
                if (rolls[i-1-1] == 10) { // i-1-1: get correct index, then go back one
                    continue;
                }
                // if spare
                else if (rolls[i-1-1] + rolls[i-1] == 10) {
                    continue;
                } else {
                    frameList.Add(rolls[i - 1] + rolls[i - 2]);
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
