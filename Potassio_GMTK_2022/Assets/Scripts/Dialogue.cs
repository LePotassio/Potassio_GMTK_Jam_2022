using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private string text;

    [SerializeField]
    private List<Option> optionsToLoad;

    [SerializeField]
    private int defaultDialogue;

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
}
