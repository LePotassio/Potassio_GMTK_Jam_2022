using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MainMenu, OptionSelection, Dialogue, DoingOption, NoOptionSelection, Waiting }
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
    // Starting Text Node here...

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

        textManager.SetAllStatUI(playerInfo.Money, playerInfo.Happiness, playerInfo.Luck, playerInfo.Dignity, playerInfo.Worker, playerInfo.Smarts, playerInfo.Cheating);

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
        if (State == GameState.Dialogue)
            textManager.DoUpdate();

        else if (State == GameState.OptionSelection)
            optionManager.DoUpdate();

        else if (State == GameState.NoOptionSelection && Input.GetKeyDown(KeyCode.Return))
            DoDialogueNode(dialogueList[currentDialogue.DefaultDialogue]);
    }

    public void DoDialogueNode(Dialogue d)
    {
        currentDialogue = d;
        State = GameState.Dialogue;
        textManager.SetText(d.Text);
        if (d.OptionsToLoad.Count > 0)
        {
            optionManager.SetOptions(d.OptionsToLoad);
            State = GameState.OptionSelection;
        }
        else
        {
            State = GameState.NoOptionSelection;
        }
    }
}
