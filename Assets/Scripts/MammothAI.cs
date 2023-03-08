using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MammothAI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    AnimatorClipInfo[] animatorinfo;
    string current_animation;

    [SerializeField]
    private NavMeshAgent navAgent;

    [SerializeField]
    private Transform[] wayPoints;

    private int wayPointIndex = 0;

    private Vector3 destination;

    private float range = 30.0f; //radius of sphere

    [SerializeField]
    private Transform centrePoint;

    private bool stoppedWalking = false;

    void Start()
    {
        navAgent.SetDestination(centrePoint.position);
        animator.SetTrigger("StartWalking");
        //UpdateDestination();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (navAgent.remainingDistance <= 1.4) //done with path //navAgent.stoppingDistance
        {
            if (!stoppedWalking)
            {
                animator.SetTrigger("StoppedWalking");
                stoppedWalking = true;
            }
            
            if (animator.GetBool("IdleFinished"))
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                {
                    animator.SetTrigger("StartWalking");
                    stoppedWalking = false;
                    navAgent.SetDestination(point);
                    animator.SetBool("IdleFinished", false);
                }
            }
            
        }
        
        /*
       if(Vector3.Distance(transform.position, destination) < 1.0f) //done with path
        {
            if (!stoppedWalking)
            {
                animator.SetTrigger("StoppedWalking");
                stoppedWalking = true;
            }

            if (animator.GetBool("IdleFinished"))
            {
                animator.SetBool("IdleFinished", false);
                UpdateDestination();
            }

        }
        */
    }

    private void UpdateDestination()
    {
        destination = wayPoints[wayPointIndex].position;
        navAgent.SetDestination(destination);
        animator.SetTrigger("StartWalking");

        if(wayPointIndex == wayPoints.Length-1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
