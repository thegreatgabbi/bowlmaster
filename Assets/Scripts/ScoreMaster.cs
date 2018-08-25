using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

    // Return a list of individual frame scores, NOT cumulative
    public static List<int> ScoreFrames(List<int> rolls) {
        List<int> frameList = new List<int>();
        int frameScore = 0;

        foreach (int roll in rolls) {
            frameScore += roll;
        }
        frameList.Add(frameScore);

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
