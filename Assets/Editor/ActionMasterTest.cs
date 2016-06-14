using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private List<int> bowls;

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    
    [SetUp]
    public void Setup() {
        bowls = new List<int>();
    }

    [Test]
    public void T00PassingTest() {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn() {
        bowls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T02BowlEightReturnsTidy() {
        bowls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T03BowlTwoThenEightReturnsEndTurn() {
        bowls.Add(2);
        bowls.Add(8);
        Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T04TwoGutterBalls() {
        bowls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T05AllGutterBalls() {

        // Check first nine frames behave correctly
        for (int i = 0; i < 9; i++) {
            bowls.Add(0);
            Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
            bowls.Add(0);
            Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
        }

        // Check tenth frame behaves correctly
        bowls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(endGame, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T06AllSpares() {

        // Check first nine frames behave correctly
        for (int i = 0; i < 9; i++) {
            bowls.Add(8);
            Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
            bowls.Add(2);
            Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
        }

        // Check tenth frame behaves correctly
        bowls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
        bowls.Add(2);
        Assert.AreEqual(reset, ActionMaster.GetNextAction(bowls));
        bowls.Add(8);
        Assert.AreEqual(endGame, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T07AllStrikes() {

        // Check first nine frames behave correctly
        for (int i = 0; i < 9; i++) {
            bowls.Add(10);
            Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
        }

        // Check tenth frame behaves correctly
        bowls.Add(10);
        Assert.AreEqual(reset, ActionMaster.GetNextAction(bowls));
        bowls.Add(10);
        Assert.AreEqual(reset, ActionMaster.GetNextAction(bowls));
        bowls.Add(10);
        Assert.AreEqual(endGame, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T08StrikeThenGutterballsOnLastFrame() {

        // Check first nine frames behave correctly
        for (int i = 0; i < 9; i++) {
            bowls.Add(10);
            Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
        }

        // Check tenth frame behaves correctly
        bowls.Add(10);
        Assert.AreEqual(reset, ActionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(endGame, ActionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T09StrikeThenOnesOnLastFrame() {

        // Check first nine frames behave correctly
        for (int i = 0; i < 9; i++) {
            bowls.Add(10);
            Assert.AreEqual(endTurn, ActionMaster.GetNextAction(bowls));
        }

        // Check tenth frame behaves correctly
        bowls.Add(10);
        Assert.AreEqual(reset, ActionMaster.GetNextAction(bowls));
        bowls.Add(1);
        Assert.AreEqual(tidy, ActionMaster.GetNextAction(bowls));
        bowls.Add(1);
        Assert.AreEqual(endGame, ActionMaster.GetNextAction(bowls));
    }
}
