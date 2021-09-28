using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] float damage = 100f;

    GameObject effectsContainer;

    public float Damage
    {
        get { return damage;}
    }

    [SerializeField] ParticleSystem explosionEffects;

    Rigidbody2D myRigidbody;

    public float GetDamage()
    {
        return damage;
    }

    void Start()
    {
        effectsContainer = GameObject.Find("EffectsContainer");
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        var newFX = Instantiate(explosionEffects, transform.position, Quaternion.identity);
        newFX.transform.parent = effectsContainer.transform;
        Destroy(gameObject);
    }
}
