using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject fxPrefab;
    [SerializeField] int poolSize = 25;

    private List<GameObject> projectileList;
    private List<GameObject> fxList;

    void Start()
    {
        projectileList = new List<GameObject>();
        fxList = new List<GameObject>();

        GameObject temp;
        while (projectileList.Count < poolSize)
        {
            temp = Instantiate(projectilePrefab);
            temp.SetActive(false);

        }
    }


}
