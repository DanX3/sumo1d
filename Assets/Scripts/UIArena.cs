using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIArena : MonoBehaviour
{

    int hp;
    public int startHp = Player.StartHP;
    float startSizeX;

    public void Init(int hp)
    {
        startHp = this.hp = hp;
        startSizeX = GetComponent<RectTransform>().sizeDelta.x;
    }

    public void AddDiff(int diff)
    {
        hp += diff;
        float scaleFactor = (float)hp / startHp;
        float targetSize = startSizeX * scaleFactor;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetSize);
    }

    void Start()
    {
        // StartCoroutine(Tester());
    }

    IEnumerator Tester()
    {
        Init(100); yield return new WaitForSeconds(2f);
        AddDiff(10); yield return new WaitForSeconds(2f);
        AddDiff(10); yield return new WaitForSeconds(2f);
        AddDiff(-20); yield return new WaitForSeconds(2f);
        AddDiff(-50); yield return new WaitForSeconds(2f);
    }

}
