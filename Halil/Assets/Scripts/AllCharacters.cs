using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllCharacters : MonoBehaviour
{
    [SerializeField] private float moveSpeed, maxHP, attack, spdPer = 1;
    private float _timeMod, currentHP;

    [SerializeField] private Transform player;
    private NavMeshAgent agent;
    [SerializeField] private GameControlScript controlScript;

    private void Start()
    {
        currentHP = maxHP;

        controlScript = GameObject.FindObjectOfType<GameControlScript>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        _timeMod = GetComponent<SpeedController>().thisObjectsTimeScale;
        agent.speed = moveSpeed * _timeMod * spdPer;

        agent.destination = player.position;

        //Debug.Log(agent.speed);
    }
}
