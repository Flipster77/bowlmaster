using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster actionMaster;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    
    [SetUp]
    public void Setup() {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest() {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn() {
        List<int> bowl = new List<int>(new int[] { 10 });
        Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowl));
    }

    [Test]
    public void T02BowlEightReturnsTidy() {
        List<int> bowl = new List<int>(new int[] { 8 });
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowl));
    }

    [Test]
    public void T03BowlTwoThenEightReturnsEndTurn() {
        List<int> bowls = new List<int>();
        bowls.Add(8);
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
        bowls.Add(2);
        Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T04TwoGutterBalls() {
        List<int> bowls = new List<int>();
        bowls.Add(0);
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T05AllGutterBalls() {
        List<int> bowls = new List<int>();

        for (int i = 0; i < 9; i++) {
            bowls.Add(0);
            Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
            bowls.Add(0);
            Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
        }
        bowls.Add(0);
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(endGame, actionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T06AllSpares() {
        List<int> bowls = new List<int>();

        for (int i = 0; i < 9; i++) {
            bowls.Add(8);
            Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
            bowls.Add(2);
            Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
        }
        bowls.Add(8);
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
        bowls.Add(2);
        Assert.AreEqual(reset, actionMaster.GetNextAction(bowls));
        bowls.Add(8);
        Assert.AreEqual(endGame, actionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T07AllStrikes() {
        List<int> bowls = new List<int>();

        for (int i = 0; i < 9; i++) {
            bowls.Add(10);
            Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
        }
        bowls.Add(10);
        Assert.AreEqual(reset, actionMaster.GetNextAction(bowls));
        bowls.Add(10);
        Assert.AreEqual(reset, actionMaster.GetNextAction(bowls));
        bowls.Add(10);
        Assert.AreEqual(endGame, actionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T08StrikeThenGutterballsOnLastFrame() {
        List<int> bowls = new List<int>();

        for (int i = 0; i < 9; i++) {
            bowls.Add(10);
            Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
        }
        bowls.Add(10);
        Assert.AreEqual(reset, actionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
        bowls.Add(0);
        Assert.AreEqual(endGame, actionMaster.GetNextAction(bowls));
    }

    [Test]
    public void T09StrikeThenOnesOnLastFrame() {
        List<int> bowls = new List<int>();

        for (int i = 0; i < 9; i++) {
            bowls.Add(10);
            Assert.AreEqual(endTurn, actionMaster.GetNextAction(bowls));
        }
        bowls.Add(10);
        Assert.AreEqual(reset, actionMaster.GetNextAction(bowls));
        bowls.Add(1);
        Assert.AreEqual(tidy, actionMaster.GetNextAction(bowls));
        bowls.Add(1);
        Assert.AreEqual(endGame, actionMaster.GetNextAction(bowls));
    }
}
