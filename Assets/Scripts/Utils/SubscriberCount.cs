using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubscriberCount : ISubscriber
{
    string signalName;
    public int count = 1;

    protected SubscriberCount(string signalName, int count)
    {
        this.signalName = signalName;
        this.count = count;
    }

    public void Signal(string signalName, Publisher pub)
    {
        count--;
        if (count == 0)
            pub.Unsubscribe(signalName, this);

        OnCountReached();
    }


    protected abstract void OnCountReached();

}
