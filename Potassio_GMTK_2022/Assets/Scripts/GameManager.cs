using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MainMenu, OptionSelection, Dialogue, DoingOption, NoOptionSelection, Waiting, Pause }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameState state;

    [SerializeField]
    private TextManager textManager;

    [SerializeField]
    private OptionManager optionManager;

    [SerializeField]
    private PlayerInfo playerInfo;


    [SerializeField]
    private List<Dialogue> dialogueList;

    private Dialogue currentDialogue;

    private GameState cacheState;

    // Mini game data manager?

    public GameState State
    {
        get { return state; }
        set { state = value; }
    }

    public TextManager TextManager
    {
        get { return textManager; }
    }

    public OptionManager OptionManager
    {
        get { return optionManager; }
    }

    public PlayerInfo PlayerInfo
    {
        get { return playerInfo; }
    }

    public List<Dialogue> DialogueList
    {
        get { return dialogueList; }
    }

    private void Start()
    {
        State = GameState.Dialogue;
        Instance = this;

        DontDestroyOnLoad(gameObject);

        textManager.StatTextsObj.SetActive(false);
        textManager.SetAllStatUI(playerInfo.Day, playerInfo.Money, playerInfo.Happiness, playerInfo.Luck, playerInfo.Dignity, playerInfo.Worker, playerInfo.Smarts, playerInfo.Cheating);
        optionManager.SetOptions(new List<Option>());


        playerInfo.ChangeMoney += () => textManager.MoneyText.text = "Money: " + playerInfo.Money;

        playerInfo.ChangeHappiness += () => textManager.HappinessText.text = "Happiness: " + playerInfo.Happiness;
        playerInfo.ChangeLuck += () => textManager.LuckText.text = "Luck: " + playerInfo.Luck;
        playerInfo.ChangeDignity += () => textManager.DignityText.text = "Dignity: " + playerInfo.Dignity;
        playerInfo.ChangeWorker += () => textManager.WorkerText.text = "Worker: " + playerInfo.Worker;
        playerInfo.ChangeSmarts += () => textManager.SmartsText.text = "Smarts: " + playerInfo.Smarts;
        playerInfo.ChangeCheating += () => textManager.CheatingText.text = "Cheating: " + playerInfo.Cheating;

        //textManager.SetText("fhnnuiwj");
        //PlayerInfo.Luck = 5;
        //textManager.TypeText("JIOFHE(UIFHIWEUHBF(IOUB");

        DoDialogueNode(dialogueList[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (State != GameState.Pause)
            {
                textManager.StatTextsObj.SetActive(true);
                cacheState = State;
                State = GameState.Pause;
            }
            else
            {
                textManager.StatTextsObj.SetActive(false);
                State = cacheState;
            }
        }

        else if (State == GameState.Dialogue)
            textManager.DoUpdate();

        else if (State == GameState.OptionSelection)
            optionManager.DoUpdate();

        else if (State == GameState.NoOptionSelection && Input.GetKeyDown(KeyCode.Return))
            DoDialogueNode(dialogueList[currentDialogue.DefaultDialogue]);
    }

    public void DoDialogueNode(Dialogue d)
    {
        Dialogue node = d;
        int nodeAlt = node.CheckForAlts();
        if (nodeAlt > -1 && nodeAlt < dialogueList.Count && dialogueList[nodeAlt] != null)
            node = dialogueList[nodeAlt];

        currentDialogue = node;
        State = GameState.Dialogue;
        textManager.SetText(node.Text);
        if (node.AdvanceDay)
            AdvanceDay();
        if (node.OptionsToLoad.Count > 0)
        {
            optionManager.SetOptions(node.OptionsToLoad);
            State = GameState.OptionSelection;
        }
        else
        {
            State = GameState.NoOptionSelection;
        }
    }

    private void AdvanceDay()
    {
        playerInfo.Day++;
    }
}
