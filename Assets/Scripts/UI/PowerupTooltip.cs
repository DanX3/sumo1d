using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupTooltip : MonoBehaviour
{
    [SerializeField] Text text;
    Vector3 offset;

    public void Init(string msg, float offset)
    {
        text.text = msg;
        this.offset = new Vector3(offset, 0f, 0f);
        transform.position = Input.mousePosition + this.offset; 
    }

    void Update()
    {
        transform.position = Input.mousePosition + this.offset;
    }
}
