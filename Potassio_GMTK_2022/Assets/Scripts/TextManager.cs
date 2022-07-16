using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    [SerializeField]
    private Text mainText;

    private string targetText;

    private int typeIndex = 0;

    private bool isTyping = false;

    private float typeTime;

    private float typeLetterSpeed = .03f;

    [SerializeField]
    GameObject statTextsObj;

    [SerializeField]
    private Text dateText;

    [SerializeField]
    private Text moneyText;

    [SerializeField]
    private Text happinessText;

    [SerializeField]
    private Text dignityText;

    [SerializeField]
    private Text luckText;

    [SerializeField]
    private Text workerText;

    [SerializeField]
    private Text smartsText;

    [SerializeField]
    private Text cheatingText;

    public GameObject StatTextsObj
    {
        get { return statTextsObj; }
    }

    public Text MainText
    {
        get { return mainText; }
    }

    public Text DateText
    {
        get { return dateText; }
    }

    public Text MoneyText
    {
        get { return moneyText; }
        set { moneyText = value; }
    }

    public Text HappinessText
    {
        get { return happinessText; }
        set { happinessText = value; }
    }

    public Text DignityText
    {
        get { return dignityText; }
        set { dignityText = value; }
    }

    public Text LuckText
    {
        get { return luckText; }
        set { luckText = value; }
    }

    public Text WorkerText
    {
        get { return workerText; }
        set { workerText = value; }
    }

    public Text SmartsText
    {
        get { return smartsText; }
        set { smartsText = value; }
    }

    public Text CheatingText
    {
        get { return cheatingText; }
        set { cheatingText = value; }
    }



    public void DoUpdate()
    {
        if (isTyping)
        {
            typeTime += Time.deltaTime;
            if (typeTime >= typeIndex * typeLetterSpeed)
            {
                Debug.Log(typeIndex);
                typeIndex++;
                if (typeIndex < targetText.Length)
                    mainText.text = mainText.text + targetText[typeIndex];
                else
                {
                    isTyping = false;
                }
            }
        }
    }

    public void SetText(string newText)
    {
        mainText.text = newText;
    }

    public void TypeText(string newText)
    {
        mainText.text = "";
        typeTime = 0;
        targetText = newText;
        isTyping = true;
    }

    public void SetAllStatUI (int day, int money, int happiness, int luck, int dignity, int worker, int smarts, int cheating)
    {
        UpdateDay(day);
        MoneyText.text = "Money: " + (money / 100) + "." + (money % 100).ToString("00");

        HappinessText.text = "Happiness: " + happiness;
        LuckText.text = "Luck: " + luck;
        DignityText.text = "Dignity: " + dignity;
        WorkerText.text = "Worker: " + worker;
        SmartsText.text = "Smarts: " + smarts;
        CheatingText.text = "Cheating: " + cheating;
    }

    public void UpdateDay(int day)
    {
        string dayString = "";
        if (day < 7)
            dayString = "" + 4 + "/" + (day + 25);
        else if (day >= 7 && day != 37)
            dayString = "" + 5 + "/" + (day - 6);
        else if (day == 37)
            dayString = "" + 6 + "/" + 1;
        else
            Debug.LogError("Invalid Day Update");
        dateText.text = dayString;
    }
}
