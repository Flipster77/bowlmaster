using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster actionMaster = new ActionMaster();

    [Test]
    public void T00PassingTest() {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn() {
        Assert.AreEqual(endTurn, actionMaster.RecordBowl(10));
    }
}
