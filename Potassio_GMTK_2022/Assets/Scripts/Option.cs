using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Option
{
    // Cutback features here, don't make abstract just hard code adjustments...

    [SerializeField]
    private string optionText;

    [SerializeField]
    private int textNodeToLoad;


    // that day or later
    [SerializeField]
    private int dayReq;

    [SerializeField]
    private int moneyReq;

    [SerializeField]
    private int happinessReq;
    [SerializeField]
    private int dignityReq;
    [SerializeField]
    private int luckReq;
    [SerializeField]
    private int workerReq;
    [SerializeField]
    private int smartsReq;
    [SerializeField]
    private int cheatingReq;

    [SerializeField]
    private int adjustMoney;

    [SerializeField]
    private int adjustHappiness;

    [SerializeField]
    private int adjustDignity;

    [SerializeField]
    private int adjustLuck;

    [SerializeField]
    private int adjustWorker;

    [SerializeField]
    private int adjustSmarts;

    [SerializeField]
    private int adjustCheating;

    [SerializeField]
    private int advanceProgression = -1;

    public string OptionText
    {
        get { return optionText; }
    }

    public void DoOption()
    {
        if (adjustMoney != 0)
            GameManager.Instance.PlayerInfo.Money += adjustMoney;
        if (adjustHappiness != 0)
            GameManager.Instance.PlayerInfo.Happiness += adjustHappiness;
        if (adjustDignity != 0)
            GameManager.Instance.PlayerInfo.Dignity += adjustDignity;
        if (adjustLuck != 0)
            GameManager.Instance.PlayerInfo.Luck += adjustLuck;
        if (adjustWorker != 0)
            GameManager.Instance.PlayerInfo.Worker += adjustWorker;
        if (adjustSmarts != 0)
            GameManager.Instance.PlayerInfo.Smarts += adjustSmarts;
        if (adjustCheating != 0)
            GameManager.Instance.PlayerInfo.Cheating += adjustCheating;

        PlayerInfo p = GameManager.Instance.PlayerInfo;
        if (advanceProgression == 1)
            p.OneProgression++;
        if (advanceProgression == 2)
            p.TwoProgression++;
        if (advanceProgression == 3)
            p.ThreeProgression++;
        if (advanceProgression == 4)
            p.FourProgression++;
        if (advanceProgression == 5)
            p.FiveProgression++;
        if (advanceProgression == 6)
            p.SixProgression++;

        // Deal with mini games down here...

        //

        GameManager.Instance.DoDialogueNode(GameManager.Instance.DialogueList[textNodeToLoad]);
    }

    public bool CheckReqs()
    {
        PlayerInfo p = GameManager.Instance.PlayerInfo;

        if (p.Day < dayReq)
            return false;

        if (p.Money < moneyReq)
            return false;

        if (p.Happiness < happinessReq)
            return false;
        if (p.Dignity < dignityReq)
            return false;
        if (p.Luck < luckReq)
            return false;
        if (p.Worker < workerReq)
            return false;
        if (p.Smarts < smartsReq)
            return false;
        if (p.Cheating < cheatingReq)
            return false;
        return true;
    }
}
