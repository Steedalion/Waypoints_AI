using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointCounter = 0;
    public float speed = 10, turnSpeed = 5, distanceThreshold = 5, trackerSpeed = 10, trackerThreshold = 8;
    private Transform CurrentWaypoint => waypoints[waypointCounter];

    private GameObject tracker;
    
    //todo: need to cache transform and tracker transform

    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        tracker.transform.position = transform.position;
        DestroyImmediate(tracker.GetComponent<Collider>());
    }

    void TrackerUpdate(Vector3 toTracker)
    {
        if (toTracker.magnitude > trackerThreshold) return;

        Vector3 towaypoint = CurrentWaypoint.position - tracker.transform.position;
        if (towaypoint.magnitude < distanceThreshold)
        {
            waypointCounter++;
            if (waypointCounter >= waypoints.Length) waypointCounter = 0;
        }

        tracker.transform.rotation = Quaternion.LookRotation(towaypoint);
        tracker.transform.Translate(0, 0, (speed + trackerSpeed) * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 toTracker = tracker.transform.position - transform.position;
        TrackerUpdate(toTracker);
        LookAt(toTracker);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void LookAt(Vector3 lookTowards)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookTowards);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }
}