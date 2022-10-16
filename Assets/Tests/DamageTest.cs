using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class DamageTest
{
    [Test]
    public void HighCrit()
    {
        var player = new Player();
        player.stats = new PlayerStats(3, 3, 3, 3, 3);

        var opponent = new Player();
        opponent.stats = new PlayerStats(3, 3, 3, 3, 3);

        // var attack = ScriptableObject.CreateInstance<SampleAttack>();
        var attack = (SampleAttack) AssetDatabase.LoadAssetAtPath("Assets/Scripts/Old/CardsV2/SampleAttack.asset", typeof(SampleAttack));
        Debug.Log("Card attack: " + attack.damage);

        int damage = Refs.ComputeDamage(player, opponent, attack);
        Debug.Log(damage);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DamageTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
