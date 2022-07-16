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

    public int OptionsCount
    {
        get { return options.Count; }
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
            if (!o.CheckReqs())
                return;
            //SetOptions(new List<Option>());
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
        foreach(Option o in FilterOutDayReqs(options))
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
        newOptions = FilterOutDayReqs(newOptions);

        options = newOptions;
        CurrentSelection = 0;

        int hardCodeLength = optionUIs.Count;
        for (int a = 0; a < hardCodeLength; a++)
        {
            if (a < options.Count)
            {
                optionUIs[a].gameObject.SetActive(true);
                optionUIs[a].OptionText.text = "-" + options[a].OptionText + options[a].GetReqText();
                if (GameManager.Instance.DoTextTransitions)
                    optionUIs[a].OptionText.color = AnimationUtils.GetZeroAlphaColor(optionUIs[a].OptionText.color);
            }
            else
            {
                optionUIs[a].gameObject.SetActive(false);
            }
        }
    }

    public List<Option> FilterOutDayReqs(List<Option> newOptions)
    {
        List<Option> filteredOptions = new List<Option>();
        foreach (Option o in newOptions)
        {
            if (o.DayReq <= GameManager.Instance.PlayerInfo.Day)
                filteredOptions.Add(o);
        }
        return filteredOptions;
    }

    public IEnumerator FadeOptionsIn(float speed)
    {
        int hardCodedLength = optionUIs.Count;
        for  (int a = 0; a < hardCodedLength; a++)
        {
            if (optionUIs[a].gameObject.activeInHierarchy)
            yield return AnimationUtils.FadeTextIn(optionUIs[a].OptionText, speed);
        }
    }

    public IEnumerator FadeOptionsOut(float speed, float intermissionSpeed)
    {
        int hardCodedLength = optionUIs.Count;
        for (int a = 0; a < hardCodedLength; a++)
        {
            if (optionUIs[a].gameObject.activeInHierarchy)
            {
                if (a == currentSelection)
                {
                    yield return AnimationUtils.FadeTextOut(optionUIs[a].OptionText, speed);
                }
            }       
        }

        yield return new WaitForSeconds(intermissionSpeed);

        int c = 0;
        int d = 0;
        for (int a = 0; a < hardCodedLength; a++)
        {
            if (optionUIs[a].gameObject.activeInHierarchy)
            {
                if (a != currentSelection)
                {
                    c++;
                    StartCoroutine(AnimationUtils.FadeTextOut(optionUIs[a].OptionText, speed, () => d++));
                }
            }
        }

        //yield return new WaitUntil(() => c == d);
    }
}
