using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContactPoint : MonoBehaviour
{
    public void Move(int damage)
    {
        transform.localPosition += damage * Vector3.right;
    }

}
