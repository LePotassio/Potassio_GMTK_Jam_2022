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
    private bool doTextTransitions;

    [SerializeField]
    private float mainTextSpeed;

    [SerializeField]
    private float optionTextSpeed;

    [SerializeField]
    private float optionTextIntermissionSpeed;

    [SerializeField]
    private float optionMainIntermissionSpeed;

    [SerializeField]
    private float brSwitchSpeed;

    [SerializeField]
    private float portraitSwitchSpeed;


    [SerializeField]
    private TextManager textManager;

    [SerializeField]
    private OptionManager optionManager;

    [SerializeField]
    private BRManager brManager;

    [SerializeField]
    private PortraitManager portraitManager;

    [SerializeField]
    private PlayerInfo playerInfo;

    [SerializeField]
    private GamblerStats gamblerStats;


    // "Cheeky"
    [SerializeField]
    public List<string> saveCodes;


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

    public bool DoTextTransitions
    {
        get { return doTextTransitions; }
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

    public GamblerStats GamblerStats
    {
        get { return gamblerStats; }
    }

    public List<Dialogue> DialogueList
    {
        get { return dialogueList; }
    }

    private void Start()
    {

        State = GameState.Dialogue;
        textManager.MainText.text = "";
        Instance = this;

        DontDestroyOnLoad(gameObject);

        StartCoroutine(portraitManager.SwitchPortrait(-1, portraitSwitchSpeed));
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

        StartCoroutine(DoDialogueNode(dialogueList[0]));
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
        {
            State = GameState.Waiting;
            StartCoroutine(DoDialogueNode(dialogueList[currentDialogue.DefaultDialogue]));
        }
    }

    public IEnumerator DoDialogueNode(Dialogue d)
    {
        yield return null;

        if (doTextTransitions)
        {
            yield return optionManager.FadeOptionsOut(optionTextSpeed, optionTextIntermissionSpeed);
            if (optionManager.OptionsCount != 0)
                yield return new WaitForSeconds(optionMainIntermissionSpeed);
            yield return AnimationUtils.FadeTextOut(textManager.MainText, mainTextSpeed);
        }

        optionManager.SetOptions(new List<Option>());

        Dialogue node = d;

        if (node.AdvanceDay)
        {
            AdvanceDay();
        }

        int nodeAlt = node.CheckForAlts();
        if (nodeAlt > -1 && nodeAlt < dialogueList.Count && dialogueList[nodeAlt] != null)
            node = dialogueList[nodeAlt];

        currentDialogue = node;

        if (node.EndGame)
            Application.Quit();

        if (node.ChangeBackground)
            yield return brManager.SwitchBackground(node.NewBackgroundIndex, brSwitchSpeed);

        if (node.ChangePortrait)
            yield return portraitManager.SwitchPortrait(node.NewPortraitIndex, portraitSwitchSpeed);

        if (node.OverrideWithDate)
            textManager.SetText(textManager.UpdateDay(playerInfo.Day));
        else if (!node.OverrideWithRoll)
            textManager.SetText(node.Text);
        else
            textManager.SetText(gamblerStats.PlaySixShooter());

        if (doTextTransitions)
            yield return AnimationUtils.FadeTextIn(textManager.MainText, mainTextSpeed);

        /*if (node.AdvanceDay)
            AdvanceDay();*/
        if (node.OptionsToLoad.Count > 0)
        {
            optionManager.SetOptions(node.OptionsToLoad);
            if (doTextTransitions)
            {
                yield return optionManager.FadeOptionsIn(optionTextSpeed);
            }
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
        saveCodes.Clear();

        // EXTREMLY INNEFICIENT, just for game jam
        foreach (Dialogue d in dialogueList)
        {
            d.ChangeAlts();
        }
    }
}
