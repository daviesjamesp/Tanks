using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    //TODO: add difficulty settings here that modify:
            //types of enemies included in waves, i.e. add new enemy when diff. raised
            //time before powerups despawn (min. time needed to reach ground)
            //time between powerups

    [SerializeField] int score = 0;
    [SerializeField] int wave  = 0;

    public int Score
    {
        get { return score; }
    }

    public int Wave
    {
        get { return wave;  }
        set { wave = value; }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Time.timeScale = 10;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Time.timeScale = 1;
        }
    }
}
