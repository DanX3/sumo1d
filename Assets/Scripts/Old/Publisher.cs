using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscriber
{
    public void Signal(string signalName, Publisher pub);
}

class SubCountPair
{
    public ISubscriber sub;
    public int count;
    public int remaining;
    public readonly bool once;

    public void Reset()
    {
        remaining = count;
    }

    public SubCountPair(ISubscriber sub, int count, bool once)
    {
        this.sub = sub;
        this.count = count;
        this.remaining = count;
        this.once = once;
    }
}

public class Publisher : MonoBehaviour
{


    Dictionary<string, List<SubCountPair>> subs = new Dictionary<string, List<SubCountPair>>();

    public void Subscribe(string signalName, ISubscriber sub, int count = 1, bool once = false)
    {
        if (!subs.ContainsKey(signalName))
            subs.Add(signalName, new List<SubCountPair>());
        subs[signalName].Add(new SubCountPair(sub, count, once));
    }

    public void Unsubscribe(string signalName, ISubscriber sub)
    {
        if (!subs.ContainsKey(signalName))
            return;
        
        if (subs[signalName].RemoveAll(pair => pair.sub == sub) == 0)
            Debug.LogWarning($"No sub found to remove with signalName {signalName}");
    }

    public void Signal(string signalName)
    {
        if (!subs.ContainsKey(signalName))
            return;

        foreach (var pair in subs[signalName])
        {
            pair.remaining--;
            if (pair.remaining > 0)
                continue;
            pair.remaining = pair.count;
            pair.sub.Signal(signalName, this);
            if (pair.once)
                Unsubscribe(signalName, pair.sub);
            else
                pair.Reset();
        }
    }
}
