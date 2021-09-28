using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IPowerup
{
    public void Collect()
    {
        FindObjectOfType<PlayerHealth>().RestoreHealth();
        Destroy(gameObject);
    }
}
