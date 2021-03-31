using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointCounter = 0;
    public float speed = 10, turnSpeed = 5, distanceThreshold = 5;
    private Transform CurrentWaypoint => waypoints[waypointCounter];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 towaypoint = CurrentWaypoint.position - transform.position;
        if (towaypoint.magnitude < distanceThreshold)
        {
            waypointCounter++;
            if (waypointCounter >= waypoints.Length) waypointCounter = 0;
        }
        LookAt(towaypoint);
        
        transform.Translate(0,0,speed*Time.deltaTime);
    }

    void LookAt(Vector3 lookTowards)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookTowards);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed*Time.deltaTime);
    }
}
