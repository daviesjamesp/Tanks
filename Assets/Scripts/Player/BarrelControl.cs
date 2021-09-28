using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelControl : MonoBehaviour
{
    [SerializeField] int firePower = 70;
    [SerializeField] GameObject myBarrel;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject currentProjectile;
    [SerializeField] GameObject currentAltProjectile;
    [SerializeField] GameObject targetRecticle;
    [SerializeField] GameObject projectileContainer;

    int bombs = 500;
    public int Bombs
    {
        get { return bombs; }
    }

    void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        myBarrel.transform.up = direction;
    }
    
    public void Fire()
    {
        var newProjectile = Instantiate(currentProjectile, endPoint.position, myBarrel.transform.rotation);
        newProjectile.transform.parent = projectileContainer.transform;
        newProjectile.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * firePower);
    }

    public void FireBomb()
    {
        if (bombs <= 0) { return; }
        bombs--;
        var newBomb = Instantiate(currentAltProjectile, endPoint.position, myBarrel.transform.rotation);
        newBomb.transform.parent = projectileContainer.transform;
        newBomb.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * firePower);
    }
}
