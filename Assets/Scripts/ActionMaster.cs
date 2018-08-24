using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

    // Reset just means put back the pins
    // EndTurn will change the player's turn (and presumably Reset as well)
    public enum Action {Tidy, Reset, EndTurn, EndGame}

    private int bowl = 1;
    private int[] bowls = new int[21];

    public Action Bowl(int pins) {

        if (pins < 0 || pins > 10) {
            throw new UnityException("Invalid pins");
        }

        // register the current bowl
        bowls[bowl - 1] = pins;

        if (bowl == 21) {
            return Action.EndGame;
        }

        if (bowl >= 19 && Bowl21Awarded()) {
            bowl += 1;
            return Action.Reset;
        }

        if (bowl == 20 && !Bowl21Awarded()) {
            return Action.EndGame;
        }

        if (pins == 10) {
            bowl += 2;
			return Action.EndTurn;
        }

        // if first bowl of frame
        // return Action.Tidy
        if (bowl % 2 != 0) {
            bowl += 1;
            return Action.Tidy;
        } else if (bowl % 2 == 0){
            bowl += 1;
            return Action.EndTurn;
        }

        // other actions here

        throw new UnityException("Not sure what action to return");
    }

    private bool Bowl21Awarded() {
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }
}
