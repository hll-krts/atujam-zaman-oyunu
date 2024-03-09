using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent agent; 
    
    private float _timeMod; public float spd;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timeMod = GetComponent<SpeedController>().thisObjectsTimeScale;
        agent.speed = spd * _timeMod;

        player.position = GameObject.FindWithTag("Player").transform.position;
        agent.destination = player.position;
    }
}
