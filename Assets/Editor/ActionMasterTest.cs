using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{
    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [SetUp]
    public void Setup()
    {
        pinFalls = new List<int>();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03RemainingPinsReturnsEndTurn()
    {
        int[] rolls = { 2, 7 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame()
    {
        for (int i = 1; i <= 18; i++) // bowl 18 times
        {
            pinFalls.Add(1);
        }
        pinFalls.Add(10); // strike
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T05CheckResetAtSpareInLastFrame()
    {
        for (int i = 1; i <= 19; i++) // bowl 18 times
        {
            pinFalls.Add(1);
        }
        pinFalls.Add(9); // spare
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T06Bowl21NotAwardedInLastFrame()
    {
        for (int i = 1; i <= 19; i++) // bowl 18 times
        {
            pinFalls.Add(1);
        }
        pinFalls.Add(1);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T07GameEndsAtBowl21() {
        for (int i = 1; i <= 19; i++) // bowl 18 times
        {
            pinFalls.Add(1);
        }
        pinFalls.Add(9);
        pinFalls.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T08CheckTidyNonStrikeAfterStrikeAtBowl20() {
        for (int i = 1; i <= 18; i++) // bowl 18 times
        {
            pinFalls.Add(1);
        }
        pinFalls.Add(10);
        pinFalls.Add(5);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T09Dondi10thFrameTurkey() {
        for (int i = 1; i <= 18; i++) // bowl 18 times
        {
            pinFalls.Add(1);
        }
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T10ZeroOneGivesEndTurn() {
        int[] rolls = { 0, 1 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

}
