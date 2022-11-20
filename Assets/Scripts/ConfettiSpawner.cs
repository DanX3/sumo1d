using System.Collections;
using UnityEngine;

public class ConfettiSpawner : MonoBehaviour
{
    public GameObject confetti;
    public float spawnPeriod = 0.3f;
    public bool loop = true;

    public void Start()
    {
        if (loop)
            StartCoroutine(SpawnConfettiRoutine());
        else
            SpawnConfetti();
    }

    IEnumerator SpawnConfettiRoutine()
    {
        while (true)
        {
            SpawnConfetti();
            yield return new WaitForSeconds(spawnPeriod);
        }
    }

    private void SpawnConfetti()
    {
        Instantiate(confetti, gameObject.transform);
    }
}
