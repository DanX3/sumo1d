using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSlots : MonoBehaviour
{

    public Color unusedColor;
    public Color usedColor;

    int manaUsed = 0;

    List<Image> manaImages = new List<Image>();

    void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (Transform t in transform)
            manaImages.Add(t.GetComponent<Image>());
        Reset();
    }

    public void UseMana()
    {
        if (manaUsed >= manaImages.Count)
            return;

        manaImages[manaUsed++].color = usedColor;
    }

    public void FreeMana()
    {
        if (manaUsed <= 0)
            return;

        manaImages[--manaUsed].color = unusedColor;
    }

    public void AddManaDiff(int diff)
    {

        while (diff != 0)
        {
            if (diff > 0)
            {
                diff--;
                UseMana();
                continue;
            }

            if (diff < 0)
            {
                diff++;
                FreeMana();
                continue;
            }
        }
    }

    public void Reset()
    {
        manaImages.ForEach(image => image.color = unusedColor);
    }
}