using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform goal;

    public float speed = 5, accuracy = 1, rotspeed = 2;

    public GameObject wpManager;

    private GameObject[] waypoints;

    private GameObject currentNode;

    private int currentWP = 0;

    private Graph graph;

    private bool AtEnd => currentWP >= graph.getPathLength();
    // Start is called before the first frame update
    void Start()
    {
        waypoints = wpManager.GetComponent<WPManager>().waypoints;
        graph = wpManager.GetComponent<WPManager>().graph;
        currentNode = waypoints[2];

    }

    public void GoHome()
    {
        Goto(0);
    }
    
    public void  GoThere()
    {
        Goto(10);
    }
    public void Goto(int i)
    {
        graph.AStar(currentNode, waypoints[i]);
        currentWP = 0;
    }

    private void LateUpdate()
    {
        if(graph.getPathLength() ==0 || AtEnd)
            return;

        currentNode = graph.getPathPoint(currentWP);

        if (Vector3.Distance(graph.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
            
        }

        if (!AtEnd)
        {
            goal = graph.getPathPoint(currentWP).transform;
            Vector3 toGoal = goal.position - transform.position;
            LookAt(toGoal);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        
        


    }
    //Todo: merge with FollowWayPoints.cs
    void LookAt(Vector3 lookTowards)
    {
        Quaternion lookRotation = Quaternion.LookRotation(lookTowards);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotspeed * Time.deltaTime);
    }
}
