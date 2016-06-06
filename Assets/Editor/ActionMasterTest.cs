using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster actionMaster;

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
        Assert.AreEqual(endTurn, actionMaster.RecordBowl(10));
    }

    [Test]
    public void T02BowlEightReturnsTidy() {
        Assert.AreEqual(tidy, actionMaster.RecordBowl(8));
    }

    [Test]
    public void T03BowlTwoThenEightReturnsEndTurn() {
        Assert.AreEqual(tidy, actionMaster.RecordBowl(8));
        Assert.AreEqual(endTurn, actionMaster.RecordBowl(2));
    }

    [Test]
    public void T04TwoGutterBalls() {
        Assert.AreEqual(tidy, actionMaster.RecordBowl(0));
        Assert.AreEqual(endTurn, actionMaster.RecordBowl(0));
    }
}
