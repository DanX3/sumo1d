
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image progress;
    //[SerializeField] Text text;
    [SerializeField] TMPro.TMP_Text text;

    int basevalue;

    public void Init(int baseValue)
    {
        this.basevalue = baseValue;
        text.text = "";
        SetValue(baseValue);
    }

    public void SetValue(int value)
    {
        int baseStatDiff = value - basevalue;
        text.text = baseStatDiff == 0 ? "" : (baseStatDiff + "");
        if (baseStatDiff > 0)
            text.text = "+" + text.text;
        progress.fillAmount = Mathf.Clamp(value, 1, 9) / 9f;
    }
}
