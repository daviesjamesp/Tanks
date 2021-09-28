//CLEANED
using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //configuration settings
    [Header("Enemies")]
    [SerializeField] WaveConfig[] waves; //TODO change to array, select at weighted random
    [Header("Wave Settings")]
    [SerializeField] float waveInterval = 5f;
    [SerializeField] int waveDifficultyIncrease = 5;
    [SerializeField] float waveIntervalModifier = 0.5f;
    [SerializeField] float waveIntervalMinimum = 1f;
    [Header("Spawn Settings")]
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] float spawnRandomFactor = 1f;
    
    //cached refs
    GameSession gameSession;

    //state vars
    float[] waveStartXPos = { -9.5f, 9.5f };

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        while (true)
        {
            StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(waveInterval);
        }
    }


    IEnumerator SpawnWave()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var row = gameObject.transform.GetChild(i);
            if (row.transform.childCount != 0) { continue; }
            gameSession.Wave++;
            if (gameSession.Wave % waveDifficultyIncrease == 0)
            {
                waveInterval = Mathf.Clamp(waveInterval - waveIntervalModifier, waveIntervalMinimum, waveInterval);
            }
            float startXPos = waveStartXPos[Random.Range(0, 2)];
            var wave = waves[Random.Range(0, waves.Length)];
            for (int j = 0; j < wave.GetNewCount(); j++)
            {
                var newEnemy = Instantiate(wave.WaveEnemy, new Vector3(startXPos, row.transform.position.y, 0), Quaternion.identity);
                newEnemy.transform.parent = row.transform;
                newEnemy.GetComponent<EnemyHealth>().gameSession = gameSession;
                yield return new WaitForSeconds(spawnInterval + Random.Range(-spawnRandomFactor, spawnRandomFactor));
            }
        }
    }
}
