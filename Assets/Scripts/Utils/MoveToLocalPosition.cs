using System;
using UnityEngine;

public class MoveToLocalPosition : MonoBehaviour
{
    private Vector3 endingLocalPosition;
    private float movementSpeed;
    private bool isActive = false;
    private Action callback;

    public void Init(Vector3 endingLocalPosition, float movementSpeed, Action callback = null)
    {
        this.endingLocalPosition = endingLocalPosition;
        this.movementSpeed = movementSpeed;
        this.callback = callback;

        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (transform.localPosition != endingLocalPosition)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endingLocalPosition, movementSpeed * Time.deltaTime);
        else
        {
            if (callback != null)
                callback.Invoke();

            Destroy(this);
        }
    }
}
