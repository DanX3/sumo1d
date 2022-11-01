using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContactPoint : MonoBehaviour
{
    [SerializeField] UIArena playerArena;
    [SerializeField] UIArena opponentArena;

    float stepX;

    public void Init()
    {
        transform.localPosition = playerArena.GetComponent<RectTransform>().anchoredPosition;
        stepX = playerArena.GetComponent<RectTransform>().sizeDelta.x / playerArena.startHp;
    }

    public void Move(int damage)
    {
        transform.localPosition += new Vector3(stepX * damage, 0f, 0f);
    }

    void Start()
    {
        Init();
        // StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        Move(10); yield return new WaitForSeconds(2f);
        Move(30); yield return new WaitForSeconds(2f);
        Move(-10); yield return new WaitForSeconds(2f);
        Move(-30); yield return new WaitForSeconds(2f);
    }
}
