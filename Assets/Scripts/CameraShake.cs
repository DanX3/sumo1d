using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float scale = .75f;
    public float delay = .3f;
    public float duration = .5f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player.OnDamageDealt += Shake;
        GameManager.Instance.opponent.OnDamageDealt += Shake;
    }


    void Shake(int damage, bool critical)
    {
        StartCoroutine(Delay(damage, critical));
    }

    IEnumerator Delay(int damage, bool critical)
    {
        yield return new WaitForSecondsRealtime(delay);
        var lastPosition = transform.position;
        transform.position = transform.position + (Vector3.up * damage * scale);
        transform.DOMoveY(lastPosition.y, duration)
            .SetEase(Ease.OutElastic)
            .Play();
    }

}
