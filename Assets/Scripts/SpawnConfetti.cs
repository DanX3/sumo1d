using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConfetti : MonoBehaviour
{
    public GameObject confetti;
    public float spawnPeriod = 0.3f;

    public void Start()
    {
        StartCoroutine(SpawnConfettiRoutine());
    }

    IEnumerator SpawnConfettiRoutine()
    {
        while (true)
        {
            var childIndex = UnityEngine.Random.Range(0, transform.childCount);
            Instantiate(confetti, transform.GetChild(childIndex));
            yield return new WaitForSeconds(spawnPeriod);
        }
    }
}
