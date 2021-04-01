using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal

    private float speed = 5, accurat = 1, rotspeed = 2;

    public GameObject wpManager;

    private GameObject[] waypoints;

    private GameObject currentNode;

    private int current = 0;

    private Graph graph;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = wpManager.GetComponent<WPManager>().waypoints;
        graph = wpManager.GetComponent<WPManager>().graph;
        currentNode = waypoints[2];

    }

    
    void Update()
    {
        
    }
}
