using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private Text optionText;

    public Text OptionText
    {
        get { return optionText; }
    }
}
