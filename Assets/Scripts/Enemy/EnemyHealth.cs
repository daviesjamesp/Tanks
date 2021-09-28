//CLEANED
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //configuration settings
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] float health = 100;
    [SerializeField] int points = 100;

    //cached refs
    public GameSession gameSession;
    Animator animator;
    GameObject effectsContainer;


    void Start()
    {
        animator = GetComponent<Animator>();
        effectsContainer = GameObject.Find("EffectsContainer");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        var otherDamage = other.gameObject.GetComponent<IProjectile>();
        if (otherDamage == null) { return; }
        if (animator != null) { PlayDamageAnim(); }
        health -= otherDamage.GetDamage();
        if (health <= 0) { Kill(); }
    }

    void PlayDamageAnim()
    {
        animator.SetTrigger("damage");
    }

    void Kill()
    {
        var newDeathFX = Instantiate(deathFX, transform.position, Quaternion.identity);
        newDeathFX.transform.parent = effectsContainer.transform;
        gameSession.AddScore(points);
        Destroy(gameObject);
    }
}
