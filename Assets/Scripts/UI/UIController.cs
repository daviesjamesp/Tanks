using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject fuelBar;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameSession gameSession;

    PlayerController myTank;
    PlayerHealth tankHealth;

    void Start()
    {
        myTank = FindObjectOfType<PlayerController>();
        tankHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameSession.Score.ToString();
        waveText.text  = "Wave: " + gameSession.Wave.ToString();
        var currentFuel = myTank.Fuel;
        for (int i = 0; i < fuelBar.transform.childCount; i++)
        {
            var level = currentFuel > (i + 1) * 5;
            fuelBar.transform.GetChild(i).gameObject.SetActive(level);
        }
        var currentHealth = tankHealth.Health;
        for (int i = 0; i < fuelBar.transform.childCount; i++)
        {
            var level = currentHealth > (i + 1f) * (100f / 3f);
            healthBar.transform.GetChild(i).gameObject.SetActive(level);
        }
    }
}
