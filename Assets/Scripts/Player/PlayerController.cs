using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxFuel = 50;
    public float MaxFuel
    {
        get { return maxFuel; }
    }
    [SerializeField] float fuel = 50;
    public float Fuel 
    {
        get { return fuel; }
    }

    bool fireEnabled = false;
    public bool FireEnabled
    {
        get { return fireEnabled;  }
        set { fireEnabled = value; }
    }

    bool isMoveable = true;
    public bool IsMoveable
    {
        get { return isMoveable;  }
        set { isMoveable = value; }
    }

    BarrelControl barrelControl;
    MoveControl moveControl;
    PlayerHealth healthControl;
    BaseController baseControl;


    void Start()
    {
        barrelControl = GetComponent<BarrelControl>();
        moveControl   = GetComponent<MoveControl>();
        healthControl = GetComponent<PlayerHealth>();
        baseControl   = FindObjectOfType<BaseController>();
    }

    void Update()
    {
        if (fuel <= 0) { baseControl.SendRefueler(); }
        if (Input.GetKey(KeyCode.A) && fuel > 0 && isMoveable)
        {
            fuel -= Time.deltaTime;
            moveControl.Move(-1);
        }
        else if (Input.GetKey(KeyCode.D) && fuel > 0 && isMoveable)
        {
            fuel -= Time.deltaTime;
            moveControl.Move(1);
        }
        if (Input.GetMouseButtonDown(0) && fireEnabled)
        {
            barrelControl.Fire();
        }
        if (Input.GetMouseButtonDown(1) && fireEnabled)
        {
            barrelControl.FireBomb();
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        var projectile = collider.gameObject.GetComponent<IProjectile>();
        if (projectile != null)
        {
            healthControl.TakeDamage(projectile.GetDamage());
        }
        var powerup = collider.gameObject.GetComponent<IPowerup>();
        if (powerup != null)
        {
            powerup.Collect();
        }
    }

    public void AddFuel(float add = 0)
    {
        if (add < Mathf.Epsilon)
        {
            fuel = maxFuel;
            return;
        }
        fuel += add;
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
    }    
}
