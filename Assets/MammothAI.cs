using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MammothAI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent navAgent;

    [SerializeField]
    private Transform[] wayPoints;

    private int wayPointIndex = 0;

    private Vector3 destination;

    void Start()
    {
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(transform.position, destination) < 1.0f)
        {
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        destination = wayPoints[wayPointIndex].position;
        navAgent.SetDestination(destination);

        if(wayPointIndex == wayPoints.Length-1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }
    }
}
