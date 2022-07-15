using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform [] points;

    //Waypoint collection generated along the path so that the monsters are able to follow the path. 
    void Awake()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i< points.Length;i++)
        {
            points[i] = transform.GetChild(i);
        }

    }

    
}
