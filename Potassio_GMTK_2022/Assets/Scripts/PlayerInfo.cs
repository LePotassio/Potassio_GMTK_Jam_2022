using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private int money;

    // #3 (Magic)
    [SerializeField]
    private int luck;

    // #2 (Polarity)
    [SerializeField]
    private int dignity;

    // #6 - 666 - (Materialism, weakness)
    [SerializeField]
    private int cheating;

    // #5 (Curiosity)
    [SerializeField]
    private int smarts;

    // #4 (Stability)
    [SerializeField]
    private int worker;

    // #1 (What is important)
    [SerializeField]
    private int happiness;

    public int Money
    {
        get { return money; }
        set { money = Mathf.Clamp(value, 0, 100000000); ChangeMoney?.Invoke(); }
    }

    public int Luck
    {
        get { return luck; }
        set { luck = Mathf.Clamp(value, 1, 6); ; ChangeLuck?.Invoke(); }
    }

    public int Dignity
    {
        get { return dignity; }
        set { dignity = Mathf.Clamp(value, 1, 6); ChangeDignity?.Invoke(); }
    }

    public int Cheating
    {
        get { return cheating; }
        set { cheating = Mathf.Clamp(value, 1, 6); ChangeCheating?.Invoke(); }
    }

    public int Smarts
    {
        get { return smarts; }
        set { smarts = Mathf.Clamp(value, 1, 6); ChangeSmarts?.Invoke(); }
    }

    public int Worker
    {
        get { return worker; }
        set { worker = value; worker =  Mathf.Clamp(worker, 1, 6); ChangeWorker?.Invoke(); }
    }
    
    public int Happiness
    {
        get { return happiness; }
        set { happiness = value; happiness = Mathf.Clamp(happiness, 1, 6); ChangeHappiness?.Invoke(); }
    }


    // Alternatively could just call an update UI function...
    public event Action ChangeMoney;

    public event Action ChangeLuck;

    public event Action ChangeDignity;

    public event Action ChangeCheating;

    public event Action ChangeSmarts;

    public event Action ChangeWorker;

    public event Action ChangeHappiness;





    // Progression (Could be its own class)
    [SerializeField]
    private int day = 1;

    [SerializeField]
    private int oneProgression;
    [SerializeField]
    private int twoProgression;
    [SerializeField]
    private int threeProgression;
    [SerializeField]
    private int fourProgression;
    [SerializeField]
    private int fiveProgression;
    [SerializeField]
    private int sixProgression;

    public int Day
    {
        get { return day; }
        set { day = value; GameManager.Instance.TextManager.UpdateDay(day); }
    }

    public int OneProgression
    {
        get { return oneProgression; }
        set { oneProgression = value; }
    }

    public int TwoProgression
    {
        get { return twoProgression; }
        set { twoProgression = value; }
    }

    public int ThreeProgression
    {
        get { return threeProgression; }
        set { threeProgression = value; }
    }

    public int FourProgression
    {
        get { return fourProgression; }
        set { fourProgression = value; }
    }

    public int FiveProgression
    {
        get { return fiveProgression; }
        set { fiveProgression = value; }
    }

    public int SixProgression
    {
        get { return sixProgression; }
        set { sixProgression = value; }
    }

    public int GetProgression(int i)
    {
        switch(i)
        {
            case 1:
                return OneProgression;
            case 2:
                return TwoProgression;
            case 3:
                return ThreeProgression;
            case 4:
                return FourProgression;
            case 5:
                return FiveProgression;
            case 6:
                return SixProgression;
            default: return -1;
        }
    }
}
