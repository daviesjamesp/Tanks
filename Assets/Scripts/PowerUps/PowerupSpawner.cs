using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerups;
    [SerializeField] int spawnDelayMin = 1;
    [SerializeField] int spawnDelayMax = 20;
    [SerializeField] float spawnCenterOffset = 6;
    [SerializeField] float spawnYPos = 5;
    [SerializeField] float destroyAfterSecs = 10;

    void Start()
    {
        StartCoroutine(SpawnPowerups());
    }

    IEnumerator SpawnPowerups()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax + 1));
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        var newPowerupIndex = Random.Range(0, powerups.Length);
        var spawnPosition = new Vector3(Random.Range(-spawnCenterOffset, spawnCenterOffset), spawnYPos, 0);
        var newPowerup = Instantiate(powerups[newPowerupIndex], spawnPosition, Quaternion.identity);
        newPowerup.transform.parent = transform;
        Destroy(newPowerup, destroyAfterSecs);
    }
}
