using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField]
    private int currentSelection;

    // List of current objects...
    [SerializeField]
    private List<Option> options;

    [SerializeField]
    private List<OptionUI> optionUIs;

    [SerializeField]
    private Color selectColor;

    [SerializeField]
    private Color unselectColor;

    [SerializeField]
    private Color selectLockedColor;

    [SerializeField]
    private Color unselectLockedColor;

    public int CurrentSelection
    {
        get { return currentSelection; }
        set { currentSelection = Mathf.Clamp(value, 0, options.Count - 1); ; UpdateSelection(); }
    }

    private void Start()
    {
        currentSelection = 0;
    }

    public void DoUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // DO current selection action
            Option o = options[CurrentSelection];
            SetOptions(new List<Option>());
            GameManager.Instance.State = GameState.DoingOption;
            o.DoOption();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentSelection--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentSelection++;
        }
    }

    public void StartSelection()
    {
        CurrentSelection = 0;
    }

    private void UpdateSelection()
    {
        int c = 0;
        foreach(Option o in options)
        {
            if (c == CurrentSelection)
            {
                if (o.CheckReqs())
                    optionUIs[c].OptionText.color = selectColor;
                else
                    optionUIs[c].OptionText.color = selectLockedColor;
            }
            else
            {
                if (o.CheckReqs())
                    optionUIs[c].OptionText.color = unselectColor;
                else
                    optionUIs[c].OptionText.color = unselectLockedColor;
            }
            c++;
        }
    }

    /*private List<Option> GetActiveOptions()
    {
        List<Option> active = new List<Option>();

        foreach (Option option in options)
        {
            if (option.gameObject.activeInHierarchy)
                active.Add(option);
        }
        return active;
    }*/

    public void SetOptions(List<Option> newOptions)
    {
        options = newOptions;
        CurrentSelection = 0;

        int hardCodeLength = 4;
        for (int a = 0; a < 4; a++)
        {
            if (a < options.Count)
            {
                optionUIs[a].gameObject.SetActive(true);
                optionUIs[a].OptionText.text = options[a].OptionText;
            }
            else
            {
                optionUIs[a].gameObject.SetActive(false);
            }
        }
    }
}
