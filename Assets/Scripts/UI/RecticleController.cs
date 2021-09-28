using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecticleController : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = false;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
