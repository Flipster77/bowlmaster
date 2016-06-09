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
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02BowlEightReturnsTidy() {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03BowlTwoThenEightReturnsEndTurn() {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(endTurn, actionMaster.Bowl(2));
    }

    [Test]
    public void T04TwoGutterBalls() {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(0));
    }

    [Test]
    public void T05AllGutterBalls() {
        for (int i = 0; i < 9; i++) {
            Assert.AreEqual(tidy, actionMaster.Bowl(0));
            Assert.AreEqual(endTurn, actionMaster.Bowl(0));
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T06AllSpares() {
        for (int i = 0; i < 9; i++) {
            Assert.AreEqual(tidy, actionMaster.Bowl(8));
            Assert.AreEqual(endTurn, actionMaster.Bowl(2));
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
        Assert.AreEqual(reset, actionMaster.Bowl(2));
        Assert.AreEqual(endGame, actionMaster.Bowl(8));
    }

    [Test]
    public void T07AllStrikes() {
        for (int i = 0; i < 9; i++) {
            Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void T08StrikeThenGutterballsOnLastFrame() {
        for (int i = 0; i < 9; i++) {
            Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endGame, actionMaster.Bowl(0));
    }

    [Test]
    public void T09StrikeThenOnesOnLastFrame() {
        for (int i = 0; i < 9; i++) {
            Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(1));
        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }
}
