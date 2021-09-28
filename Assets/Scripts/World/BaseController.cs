using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] float fuelRate = 2;
    [SerializeField] GameObject refuelPrefab;

    Coroutine fuelRoutine;
    bool coroutineRunning = false;
    PlayerController myTank;
    bool refuelerOut = false;
    public bool RefuelerOut
    {
        get { return refuelerOut;  }
        set { refuelerOut = value; }
    }

    void Start()
    {
        myTank = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() == null || coroutineRunning) { return; }
        fuelRoutine = StartCoroutine(FuelTank());
        myTank.FireEnabled = false;
        coroutineRunning = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() == null || !coroutineRunning) { return; }
        if (fuelRoutine != null) StopCoroutine(fuelRoutine);
        myTank.FireEnabled = true;
        coroutineRunning = false;
    }

    public void SendRefueler()
    {
        if (refuelerOut) { return; }
        refuelerOut = true;
        
        Vector3 refuelPosition;
        if (transform.position.x > 0) //to the right
        {
            refuelPosition = new Vector3(transform.position.x - 1.5f, transform.position.y, 0);
        }
        else //to the left
        {
            refuelPosition = new Vector3(transform.position.x + 1.5f, transform.position.y, 0);
        }
        Instantiate(refuelPrefab, refuelPosition, Quaternion.identity);
    }

    IEnumerator FuelTank()
    {
        while (myTank.Fuel < myTank.MaxFuel)
        {
            myTank.AddFuel(Time.deltaTime * fuelRate);
            yield return new WaitForEndOfFrame();
        }
    }
}
