using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WPManager : MonoBehaviour
{
    [SerializeField] public GameObject[] waypoints;
    [SerializeField] private Link[] links;
    [SerializeField] public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length > 0)
        {
            foreach (GameObject variableWaypoint in waypoints)
            {
                graph.AddNode(variableWaypoint);
            }

            foreach (Link link in links)
            {
                graph.AddEdge(link.node1, link.node2);
                if (link.dir == Link.direction.BI) graph.AddEdge(link.node2, link.node1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        graph.debugDraw();
    }
}

[Serializable]
public struct Link
{
    public enum direction
    {
        UNI,
        BI
    }

    public GameObject node1, node2;
    public direction dir;
}