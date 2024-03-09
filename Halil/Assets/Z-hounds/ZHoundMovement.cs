using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZHoundMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent agent;

    private float _timeMod; public float spd, spdPer = 1;


    //Stats
    [SerializeField] private float currentHP, attack, maxHP, baseAttack;
    [SerializeField] private GameControlScript controlScript;

    private void Awake()
    {
        controlScript = GameObject.FindObjectOfType<GameControlScript>();

        currentHP = maxHP + controlScript.hpVars;
        attack = baseAttack + controlScript.atkVars;
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _timeMod = GetComponent<SpeedController>().thisObjectsTimeScale;
        agent.speed = spd * _timeMod * spdPer;

        agent.destination = player.position;
    }
}
