using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, IProjectile
{
    [SerializeField] float damage = 300f;
    [SerializeField] float explosionRadius = 25;
    [SerializeField] float explosionDelay = 0.5f;

    public float Damage
    {
        get { return damage;}
    }

    [SerializeField] ParticleSystem explosionEffects;

    Rigidbody2D myRigidbody;
    GameObject effectsContainer;

    void Start()
    {
        effectsContainer = GameObject.Find("EffectsContainer");
        Invoke("Explode", explosionDelay);
    }

    public float GetDamage()
    {
        return damage;
    }

    void Explode()
    {
        transform.localScale = new Vector3(explosionRadius, explosionRadius, 1);
        gameObject.AddComponent<PolygonCollider2D>();
        var bombEffects = Instantiate(explosionEffects, transform.position, Quaternion.identity);
        bombEffects.transform.parent = effectsContainer.transform;
        bombEffects.transform.localScale = new Vector3(explosionRadius * 0.75f, explosionRadius * 0.75f, 1);
        Destroy(gameObject, 0.1f);
    }
}
