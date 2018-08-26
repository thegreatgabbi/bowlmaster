using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreMaster
{

    public enum Action { NEWFRAME, MIDFRAME, STRIKE1, STRIKE2, SPARE }

    // Return a list of individual frame scores, NOT cumulative
    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();
        Action prevAction = Action.NEWFRAME;
        int intermediateValue = 0;
        int frame = 0;

        for (int i = 1; i <= rolls.Count; i++)
        {
            if (prevAction == Action.NEWFRAME)
            {
                if (frame != 10) frame++;

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
            }
            else if (prevAction == Action.MIDFRAME)
            {
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
            else if (prevAction == Action.SPARE)
            {
                if (frame != 10) frame++;

                if (rolls[i-1] == 10) {
                    intermediateValue = 10;
                    prevAction = Action.STRIKE1;
                } else {
                    frameList.Add(rolls[i - 1] + 10);
                    intermediateValue = rolls[i - 1];
                    prevAction = Action.MIDFRAME;
                }

            }
            else if (prevAction == Action.STRIKE1)
            {
                if (frame != 10) frame++;

                // if previous frame was spare
                if (intermediateValue == 10) {
                    frameList.Add(rolls[i - 1] + intermediateValue);
                }

                prevAction = Action.STRIKE2;
            }
            else if (prevAction == Action.STRIKE2)
            {
                if (frame != 10 && rolls[i-2] == 10) // if previous roll was a strike
                {
                    frame++;
                }

                frameList.Add(rolls[i - 1] + rolls[i - 2] + rolls[i - 3]); // print prev frame score
                // if your previous action was also a strike, you should delay evalation of current frame
                if (rolls[i - 2] == 10)
                {
                    prevAction = Action.STRIKE2;
                }
                else if (rolls[i - 1] + rolls[i-2] == 10) {
                    prevAction = Action.SPARE;
                } else {
                    if (frame != 10)
                    {
                        frameList.Add(rolls[i - 1] + rolls[i - 2]); // current frame score
                    }
                    intermediateValue = 0;
                    prevAction = Action.NEWFRAME;
                }
            }

            var result = String.Join(",", frameList.Select(x => x.ToString()).ToArray());
            Console.WriteLine(String.Format("Frame {0}: {1}", frame, result));
        }
        return frameList;
    }

    // Returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative(List<int> rolls)
    {

        List<int> scoreList = new List<int>();
        List<int> frameScores = ScoreFrames(rolls);
        int score = 0;

        foreach (int frame in frameScores)
        {
            score += frame;
            scoreList.Add(score);

            var result = String.Join(",", scoreList.Select(x => x.ToString()).ToArray());
            Console.WriteLine(result);
        }
        return scoreList;
    }
}
