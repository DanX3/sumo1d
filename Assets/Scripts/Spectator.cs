using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChangeAction();
    }

    void OnDestroy()
    {
        DOTween.KillAll();
    }

    void Jump()
    {
        var power = Random.Range(1, 3);
        var numJumps = Random.Range(4, 8);
        var duration = numJumps * Random.Range(0.5f, 1f);
        transform.DOJump(transform.position, power, numJumps, duration)
            .OnComplete(() => ChangeAction())
            .Play();
    }

    // void Tilt()
    // {
    //     transform.DORotate(new Vector3(0, 0, -30), 1f).From();
    //     transform.DORotate(new Vector3(0, 0, 30), 2f).From();
    //     transform.DORotate(new Vector3(0, 0, 0), 2f).From()

    //         // .OnStepComplete(() => )
    //         // .OnStepComplete(() => transform.DORotate(new Vector3(0, 0, 0), 1f))
    //         .OnComplete(() => ChangeAction())
    //         .Play();
    // }

    void SwitchSide()
    {
        var scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    void DoNothing()
    {
        transform.DORotate(new Vector3(0, 0, 0), 2f)
            .OnComplete(() => ChangeAction());
    }

    void ChangeAction()
    {
        if (Random.Range(0f, 1f) < 0.25)
            SwitchSide();

        var random = Random.Range(0f, 1f);
        if (random < 0.5)
            Jump();
        // else if (random < 0.5)
        //     Tilt();
        else
            DoNothing();
    }


}
