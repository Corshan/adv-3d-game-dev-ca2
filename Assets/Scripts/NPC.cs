using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField][Range(1, 5)] private float _distance = 1;
    private NavMeshAgent _agent;
    private List<GameObject> _waypoints;
    private GameObject _target;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _waypoints = GameObject.FindGameObjectsWithTag("WP").ToList();

        _target = _waypoints[Random.Range(0, _waypoints.Count)];
    }

    void Update()
    {
        if (Vector3.Distance(_agent.transform.position, _target.transform.position) < _distance)  _target = _waypoints[Random.Range(0, _waypoints.Count)]; 

        _agent.SetDestination(_target.transform.position);
    }
}
