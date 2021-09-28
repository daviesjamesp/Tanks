using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelerController : MonoBehaviour
{
    [SerializeField] float fuelRate = 4; //duplicated in base code
    [SerializeField] float moveSpeedModifier = 1f;
    [SerializeField] float stopDistance = 1f;
 
    Transform myTank;
    PlayerController tankController;
    BaseController baseControl;
    Rigidbody2D myRigidBody;
    int direction = 0;
    bool isFueling = false;
    bool isReturning = false;
    Vector2 originalPosition;

    void Start()
    {
        myTank = FindObjectOfType<PlayerController>().gameObject.transform;
        tankController = myTank.GetComponent<PlayerController>();
        baseControl = FindObjectOfType<BaseController>();
        myRigidBody = GetComponent<Rigidbody2D>();
        direction = (int)-Mathf.Sign(transform.position.x);
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fuel();
        CheckForDestroy();        
    }

    void Move()
    {
        if (Vector2.Distance(myTank.position, transform.position) > stopDistance || isReturning)
        {
            myRigidBody.velocity = new Vector2(direction * moveSpeedModifier, myRigidBody.velocity.y);
        }
        else
        {
            isFueling = true;
            tankController.IsMoveable = false;
        }
    }

    void Fuel()
    {
        if (!isFueling) { return; }
        if (tankController.Fuel >= tankController.MaxFuel && !isReturning)
        {
            tankController.IsMoveable = true;
            isFueling = false;
            isReturning = true;
            direction *= -1;
            return;
        }
        print("fueling!");
        tankController.AddFuel(fuelRate * Time.deltaTime);
    }

    void CheckForDestroy()
    {
        if (Mathf.Abs(transform.position.x) >= Mathf.Abs(originalPosition.x) && isReturning)
        {
            baseControl.RefuelerOut = false;
            Destroy(gameObject);
        }
    }
}
