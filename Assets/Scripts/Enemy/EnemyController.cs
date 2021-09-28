//CLEANED
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //configuration settings
    [Header("Weapon Settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] float fireDelay = 1f;
    [SerializeField] float fireRandomFactor = 0.5f;
    [SerializeField] float projectileOffset = -0.5f;
    [SerializeField] float projectileDownForce = 400;
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float verticalAmplitude = 0.5f;
    [SerializeField] float verticalPeriod = 1f;
    
    //state vars
    int xDirection = 0;
    Vector2 startPos;

    void Start()
    {
        StartCoroutine(RepeatFire());
        startPos  = (Vector2)transform.position;
        xDirection = (int)-Mathf.Sign(startPos.x);
    }

    IEnumerator RepeatFire()
    {
        while (true)
        {
            var newProjectile = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + projectileOffset), Quaternion.identity);
            newProjectile.transform.parent = transform.parent;
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.down * projectileDownForce);
            yield return new WaitForSeconds(fireDelay + Random.Range(-fireRandomFactor, fireRandomFactor));
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(startPos.x))
        {
            Destroy(gameObject);
        }
        var moveTo = (Vector2)transform.position;
        moveTo.x += xDirection * moveSpeed * Time.deltaTime;
        moveTo.y = verticalAmplitude * Mathf.Sin(verticalPeriod * moveTo.x) + startPos.y;
        transform.position = moveTo;
    }
}
