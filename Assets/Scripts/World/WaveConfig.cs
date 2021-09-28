using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemy;
    [SerializeField] int waveMinCount;
    [SerializeField] int waveMaxCount;

    public GameObject WaveEnemy
    {
        get { return enemy; }
    }
    public int GetNewCount()
    {
        return Random.Range(waveMinCount, waveMaxCount + 1);
    }
}
