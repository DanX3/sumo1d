using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIContactPoint : MonoBehaviour
{
    public void Move(int damage)
    {
        transform.DOLocalMove(transform.localPosition + damage * Vector3.right, 1f)
            .SetEase(Ease.OutElastic)
            .SetDelay(0.3f)
            .Play();
        // transform.localPosition += damage * Vector3.right;
    }

}
