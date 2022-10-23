using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableImage : MonoBehaviour, IPointerClickHandler, ISubscriber
{
    public string signalName;
    public int count;
    public string signalOnTrigger;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void Signal(string signalName, Publisher pub)
    {
        GetComponent<Image>().color = Color.red;
        pub.Signal(signalOnTrigger);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Refs.Instance.pub.Subscribe("")
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
