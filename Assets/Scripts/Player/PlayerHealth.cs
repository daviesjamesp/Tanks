using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 300;
    [SerializeField] float health = 300;
    public float Health
    {
        get { return health;}
    }
    
    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;
        if (health <= 0) { Kill(); }
    }

    void Kill()
    {
        //TODO add death effects
        Destroy(gameObject);
    }

    public void RestoreHealth()
    {
        health = maxHealth;
    }
}
