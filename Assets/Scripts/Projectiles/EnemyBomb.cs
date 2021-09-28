using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour, IProjectile
{
    [SerializeField] float damage = 300f;
    [SerializeField] float explosionRadius = 2;
    [SerializeField] float fxSizeFactor = 0.5f;

    bool hasExploded = false;

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
    }

    public float GetDamage()
    {
        return damage;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
    }

    void Explode()
    {
        if (hasExploded) { return; }
        transform.localScale = new Vector3(explosionRadius, explosionRadius, 1);
        var bombEffects = Instantiate(explosionEffects, transform.position, Quaternion.identity);
        bombEffects.transform.parent = effectsContainer.transform;
        bombEffects.transform.localScale = new Vector3(explosionRadius * fxSizeFactor, explosionRadius * fxSizeFactor, 1);
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        hasExploded = true;
        Destroy(gameObject, 0.1f);
    }
}
