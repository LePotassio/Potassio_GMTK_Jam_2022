using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Do fade option would be nice here for delays

    [SerializeField]
    private string text;

    [SerializeField]
    private List<Option> optionsToLoad;

    [SerializeField]
    private int defaultDialogue;

    [SerializeField]
    private bool advanceDay = false;

    [SerializeField]
    private List<DialogueAlt> alts;


    [SerializeField]
    private bool changeBackground;

    [SerializeField]
    private int newBackgroundIndex;

    [SerializeField]
    private bool changePortrait;

    [SerializeField]
    private int newPortraitIndex;

    [SerializeField]
    private bool overrideWithRoll = false;

    public string Text
    {
        get { return text; }
    }

    public List<Option> OptionsToLoad
    {
        get { return optionsToLoad; }
    }

    public int DefaultDialogue
    {
        get { return defaultDialogue; }
    }

    public bool AdvanceDay
    {
        get { return advanceDay; }
    }

    public bool ChangeBackground
    {
        get { return changeBackground; }
    }

    public int NewBackgroundIndex
    {
        get { return newBackgroundIndex; }
    }

    public bool ChangePortrait
    {
        get { return changePortrait; }
    }

    public int NewPortraitIndex
    {
        get { return newPortraitIndex; }
    }

    public bool OverrideWithRoll
    {
        get { return overrideWithRoll; }
    }

    // Honestly a terrible way to code this but least taxing brain wise, Prioritizes in order of iteration
    public int CheckForAlts()
    {
        PlayerInfo p = GameManager.Instance.PlayerInfo;
        foreach (DialogueAlt alt in alts)
        {
            if (p.Day == alt.ExactDayRequirement || p.Day == -1)
            {
                if (alt.CharacterProgressionNeeded == -1 || p.GetProgression(alt.CharacterProgressionNeeded) >= alt.CharacterProgressionLevel)
                {
                    return alt.Alt;
                }
            }
        }

        return -1;
    }
}

[System.Serializable]
public class DialogueAlt
{
    [SerializeField]
    int alt;
    [SerializeField]
    int characterProgressionNeeded = -1;

    [SerializeField]
    int characterProgressionLevel = -1;

    [SerializeField]
    int exactDayRequirement = -1;

    public int Alt
    {
        get { return alt; }
    }

    public int CharacterProgressionNeeded
    {
        get { return characterProgressionNeeded; }
    }

    public int CharacterProgressionLevel
    {
        get { return characterProgressionLevel; }
    }

    public int ExactDayRequirement
    {
        get { return exactDayRequirement; }
    }
}
