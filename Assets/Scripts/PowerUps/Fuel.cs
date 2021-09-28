using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour, IPowerup
{
    public void Collect()
    {
        var tank = FindObjectOfType<PlayerController>();
        tank.AddFuel();
        Destroy(gameObject);
    }
}
