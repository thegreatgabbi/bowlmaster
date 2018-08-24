using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest
{

    private ActionMaster actionMaster;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [SetUp]
    public void Setup()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03RemainingPinsReturnsEndTurn()
    {
        actionMaster.Bowl(2);
        Assert.AreEqual(endTurn, actionMaster.Bowl(7));
    }

    [Test]
    public void T04CheckResetAtStrikeInLastFrame()
    {
        for (int i = 1; i <= 18; i++) // bowl 18 times
        {
            actionMaster.Bowl(1);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T05CheckResetAtSpareInLastFrame()
    {
        for (int i = 1; i <= 19; i++) // bowl 19 times
        {
            actionMaster.Bowl(1);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(9));
    }

    [Test]
    public void T06Bowl21NotAwardedInLastFrame()
    {
        for (int i = 1; i <= 19; i++) // bowl 19 times
        {
            actionMaster.Bowl(1);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void T07GameEndsAtBowl21() {
        for (int i = 1; i <= 19; i++) // bowl 19 times
        {
            actionMaster.Bowl(1);
        }
        actionMaster.Bowl(9); // bowl 20
        Assert.AreEqual(endGame, actionMaster.Bowl(10)); // bowl 21
    }

    [Test]
    public void T08CheckTidyNonStrikeAfterStrikeAtBowl20() {
        for (int i = 1; i <= 18; i++) // bowl 18 times
        {
            actionMaster.Bowl(1);
        }
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(5)); // Bowl 20
    }

}
