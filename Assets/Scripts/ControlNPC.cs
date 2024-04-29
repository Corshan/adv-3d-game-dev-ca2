using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ControlNPCDynamic : MonoBehaviour
{
    public float speed = 3.5f;
    public float hearingDistance = 10.0f;
    public GameObject[] waypoints;
    public GameObject player;
    public int waypointIndex = 0;
    private Animator _anim;
    private AnimatorStateInfo _info;
    private static readonly int CanHear = Animator.StringToHash("CanHear");
    public GameObject _hearing;
    
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = speed;
        _anim = GetComponent<Animator>();
        waypoints = GameObject.FindGameObjectsWithTag("WP");
        player = GameObject.Find("Player");
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void ChangeHearingDistance(float newDistance)
    {
        hearingDistance = newDistance;
        _hearing.transform.localScale = new Vector3(hearingDistance, 0.2f, hearingDistance);
    }

    void Listen()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < hearingDistance)
        {
            _anim.SetBool(CanHear, true);
        }
        else
        {
            _anim.SetBool(CanHear, false);
        }
    }

    void Update()
    {
        _info = _anim.GetCurrentAnimatorStateInfo(0);
        Listen();
        if (_info.IsName("Chase"))
        {
            GetComponent<NavMeshAgent>().speed = speed;
            GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        if (_info.IsName("Patrol"))
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            if (Vector3.Distance(transform.position, waypoints[waypointIndex].transform.position) < 2)
            {
                waypointIndex++;
            }
            if (waypointIndex > waypoints.Length - 1)
            {
                waypointIndex = 0;
            }
            GetComponent<NavMeshAgent>().SetDestination(waypoints[waypointIndex].transform.position);
        }
        
        if (_info.IsName("Idle"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
}