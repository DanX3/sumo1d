using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        var newTarget = target.GetComponentInChildren<SpotlightTarget>();
        if (newTarget != null)
            target = newTarget.transform;
    }

    void Update()
    {
        transform.LookAt(target.position);
    }
}
