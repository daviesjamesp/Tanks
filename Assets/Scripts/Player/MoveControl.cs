using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    [SerializeField] float moveSpeedModifier = 1f;
    [SerializeField] float flipDelay = 0.5f;
    [SerializeField] float flipVerticalOffset = 0.5f;

    Rigidbody2D tankMover;

    void Start()
    {
        tankMover = GetComponent<Rigidbody2D>();
    }

    public void Move(int direction)
    {
        tankMover.velocity = new Vector2(direction * moveSpeedModifier, tankMover.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<TerrainCreator>() != null)
        {
            StartCoroutine(FlipTank());
        }
    }

    IEnumerator FlipTank()
    {
        yield return new WaitForSeconds(flipDelay);
        print("flip!"); //add in-game notification
        var tankPos = gameObject.transform.position;
        tankPos.y += flipVerticalOffset;
        gameObject.transform.position = tankPos;
        gameObject.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
