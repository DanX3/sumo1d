using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ManaActions
{
    public List<CardV2> cards;
}

[CreateAssetMenu(fileName = "OpponentAction", menuName = "Cards/Actions", order = 1)]
public class OpponentActions : ScriptableObject
{
    public List<ManaActions> actions;
    public List<CardV2> manax;

    public List<List<CardV2>> mana1;
    public List<List<CardV2>> mana2;
    public List<List<CardV2>> mana3;
    public List<List<CardV2>> mana4;
    public List<List<CardV2>> mana5;
    public List<List<CardV2>> mana6;

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {   
        Debug.Log("code executed at compile time");
    }
}
