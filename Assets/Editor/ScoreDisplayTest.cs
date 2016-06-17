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
    public void T01OneBowl() {
        int[] bowls = { 1 };
        string bowlsString = "1";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T02TwoBowls() {
        int[] bowls = { 1, 2 };
        string bowlsString = "12";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T03OneSpare() {
        int[] bowls = { 2, 8 };
        string bowlsString = "2/";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T04OneStrike() {
        int[] bowls = { 10, 0 };
        string bowlsString = " X";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T05OneGutterball() {
        int[] bowls = { 0 };
        string bowlsString = "-";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T06SpareThenThirdBowl() {
        int[] bowls = { 3, 7, 4 };
        string bowlsString = "3/4";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T07StrikeThenThirdBowl() {
        int[] bowls = { 10, 0, 5 };
        string bowlsString = " X5";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T08SpareInLastFrame() {
        int[] bowls = { 1,2, 3,4, 5,5, 9,0, 8,1, 7,2, 6,3, 5,4, 4,5, 6,4 };
        string bowlsString = "12345/9-81726354456/";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T09SpareInLastFrameWithFollowup() {
        int[] bowls = { 1,2, 3,4, 5,5, 9,0, 8,1, 7,2, 6,3, 5,4, 4,5, 6,4,5 };
        string bowlsString = "12345/9-81726354456/5";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T10StrikeInLastFrame() {
        int[] bowls = { 1, 2, 3, 4, 10, 0, 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 10 };
        string bowlsString = "1234 X9-8172635445X";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T11StrikeInLastFrameWithFollowupBowls() {
        int[] bowls = { 1, 2, 3, 4, 10, 0, 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 10, 2, 3 };
        string bowlsString = "1234 X9-8172635445X23";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T12TwoStrikesInLastFrame() {
        int[] bowls = { 1, 2, 3, 4, 10, 0, 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 10, 10, 0 };
        string bowlsString = "1234 X9-8172635445XX-";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T13ThreeStrikesInLastFrame() {
        int[] bowls = { 1, 2, 3, 4, 10, 0, 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 10, 10, 10 };
        string bowlsString = "1234 X9-8172635445XXX";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T14AllGutterballs() {
        int[] bowls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string bowlsString = "--------------------";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T15AllSpares() {
        int[] bowls = { 0, 10, 1, 9, 2, 8, 3, 7, 4, 6, 5, 5, 6, 4, 7, 3, 8, 2, 9, 1, 10 };
        string bowlsString = "-/1/2/3/4/5/6/7/8/9/X";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T16AllStrikes() {
        int[] bowls = { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 10, 10 };
        string bowlsString = " X X X X X X X X XXXX";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }

    [Test]
    public void T17StrikeThenSpareInLastFrame() {
        int[] bowls = { 1, 2, 3, 4, 10, 0, 9, 0, 8, 1, 7, 2, 6, 3, 5, 4, 4, 5, 10, 8, 2 };
        string bowlsString = "1234 X9-8172635445X8/";
        Assert.AreEqual(bowlsString, ScoreDisplay.FormatBowls(bowls.ToList()));
    }
}