using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreator : MonoBehaviour
{
    [SerializeField] GameObject homeBase;
    [SerializeField] int terrainVolatility = 5;
    [Range(0,1)][SerializeField] float terrainFriction = 1f;

    SpriteShapeController shape;
    float pointDistance;
    int terrainPoints;

    void Awake()
    {
        terrainPoints = Random.Range(2, 5);
        shape = GetComponent<SpriteShapeController>();

        int findBaseSide = Random.Range(0, 2);
        Vector3 leftCorner  = shape.spline.GetPosition(1);
        Vector3 rightCorner = shape.spline.GetPosition(2);

        int indexOffset = 0;

        if (findBaseSide > 0) //lefthand side
        {
            shape.spline.InsertPointAt(2, new Vector3(leftCorner.x + 4f, leftCorner.y, 0));
            GetDistanceBetweenPoints(2, 3);
            homeBase.transform.position = new Vector2(-7.75f, -2.3f);
            homeBase.transform.localScale = new Vector3(-1, homeBase.transform.localScale.y, homeBase.transform.localScale.z);
            indexOffset = 1;
        }
        else //righthand side
        {
            shape.spline.InsertPointAt(2, new Vector3(rightCorner.x - 4f, rightCorner.y, 0));
            homeBase.transform.position = new Vector2(7.75f, -2.3f);
            GetDistanceBetweenPoints(1, 2);
        }

        for (int i = 0; i < terrainPoints; i++)
        {
            float xPos = shape.spline.GetPosition(i + 1 + indexOffset).x + pointDistance;
            shape.spline.InsertPointAt(i + 2 + indexOffset, new Vector3(xPos, Random.Range(0f,terrainVolatility), 0));
        }

        for (int i = 2 + indexOffset; i < (terrainPoints + 2 + indexOffset); i++)
        {
            shape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            shape.spline.SetLeftTangent(i, new Vector3(-1, 0, 0));
            shape.spline.SetRightTangent(i, new Vector3(1, 0, 0));
        }

        PhysicsMaterial2D terrainMaterial = new PhysicsMaterial2D();
        terrainMaterial.friction = terrainFriction;
        shape.gameObject.GetComponent<PolygonCollider2D>().sharedMaterial = terrainMaterial;

        FindObjectOfType<PlayerController>().gameObject.transform.position = 
                                                        new Vector2(homeBase.transform.position.x, 
                                                                    homeBase.transform.position.y - 0.25f);
    }

    void GetDistanceBetweenPoints(int leftVertex, int rightVertex) 
    {
        var totalDistance = shape.spline.GetPosition(rightVertex).x - shape.spline.GetPosition(leftVertex).x;
        pointDistance = totalDistance / (terrainPoints + 2);
    }
}
