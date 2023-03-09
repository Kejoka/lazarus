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

    private bool stoppedWalking = false;

    void Start()
    {
        animator.SetTrigger("StartWalking");
        UpdateDestination();
    }

    void Update()
    {      
       if(Vector3.Distance(transform.position, destination) <= 1.4) //done with path
        {
            if (!stoppedWalking)
            {
                animator.SetTrigger("StoppedWalking");
                stoppedWalking = true;
            }

            if (animator.GetBool("IdleFinished"))
            {
                animator.SetBool("IdleFinished", false);
                animator.SetTrigger("StartWalking");
                stoppedWalking = false;
                UpdateDestination();
            }

        }
        
    }

    private void UpdateDestination()
    {
        destination = wayPoints[wayPointIndex].position;
        navAgent.SetDestination(destination);

        if (wayPointIndex == wayPoints.Length-1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }
    }

    
}
