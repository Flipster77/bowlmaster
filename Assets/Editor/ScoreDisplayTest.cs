using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest {

    [Test]
    public void T00NoBowls() {
        List<int> bowls = new List<int>();
        string bowlsString = "";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T01Bowl1() {
        int[] bowls = { 1 };
        string bowlsString = "1";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }


}