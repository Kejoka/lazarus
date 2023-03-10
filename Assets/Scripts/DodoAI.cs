using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DodoAI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent navAgent;

    [SerializeField]
    private float range; //range of movement (sphere radius) from centerpoint

    [SerializeField]
    private Transform centrePoint;

    [SerializeField]
    private AudioSource dodoSound;

    private bool stoppedWalking = false;

    void Start()
    {
        navAgent.SetDestination(centrePoint.position);
        animator.SetTrigger("StartWalking");
    }

    void Update()
    {
        if (navAgent.remainingDistance <= 0.1) //done with path 
        {
            if (!stoppedWalking)
            {
                animator.SetTrigger("StoppedWalking");
                stoppedWalking = true;
                dodoSound.Play();
            }

            if (animator.GetBool("IdleFinished"))
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point)) 
                {
                    animator.SetTrigger("StartWalking");
                    stoppedWalking = false;
                    navAgent.SetDestination(point);
                    animator.SetBool("IdleFinished", false);
                }
            }

        }
    }

    //Generate RandomPoint in a sphere
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //check if randomPoint lies within navmesh
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
