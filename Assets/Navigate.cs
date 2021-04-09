using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float distance = 3;

    [SerializeField] private Vector3 goal;
    private int index;
    private WaitForSeconds wait = new WaitForSeconds(0.5f);

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NextWayPoint();
        StartCoroutine(CheckForGoal());
    }

    private IEnumerator CheckForGoal()
    {
        while (true)
        {
            if (Vector3.Distance(goal, transform.position) < distance)
            {
                NextWayPoint();
            }

            yield return wait;
        }
    }

    private void NextWayPoint()
    {
        if (index == waypoints.Length-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }

        goal = waypoints[index++].position;
        agent.SetDestination(goal);
    }
}