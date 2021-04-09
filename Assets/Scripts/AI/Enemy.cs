using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityStandardAssets.Vehicles.Car;

public class Enemy : MonoBehaviour
{
    private StateMachines Brain;
    [SerializeField] NavMeshAgent agent;

    private CarController playerCar;

    private bool vehicleNear;
    private bool canAttack;
    private float change;
    private float AttackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerCar = FindObjectOfType<CarController>();
        agent = GetComponent<NavMeshAgent>();
        Brain = GetComponent<StateMachines>();
        Brain.pushState(Idle, OnIdleEnter);
    }

    // Update is called once per frame
    void Update()
    {
        vehicleNear = Vector3.Distance(transform.position, playerCar.transform.position) < 20;
        canAttack = Vector3.Distance(transform.position, playerCar.transform.position) < 1;
    }

    void OnIdleEnter()
    {
        agent.ResetPath();
    }

    void Idle()
    {
        change -= Time.deltaTime;
        if (vehicleNear)
        {
            Brain.pushState(Chase, null);
        }

        else if (change <= 0)
        {
            Brain.pushState(Wander, OnWanderEnter);
            change = Random.Range(1, 3);
        }
    }

    //Void OnChaseEnter() {}

    void Chase()
    {
        agent.SetDestination(playerCar.transform.position);
        if (Vector3.Distance(transform.position, playerCar.transform.position) > 25)
        {
            Brain.pushState(Idle, OnIdleEnter);
        }
        if (canAttack)
        {
            Brain.pushState(Attack, OnAttackEnter);
        }
    }

    void OnWanderEnter()
    {
        //every few second go in a random direction
        Vector3 direction = (Random.insideUnitSphere * 10f) + transform.position;
        NavMesh.SamplePosition(direction, out NavMeshHit navMeshHit, 10f, NavMesh.AllAreas);
        Vector3 destination = navMeshHit.position;
        agent.SetDestination(destination);
    }

    void Wander()
    {
        if (agent.remainingDistance <= 0.25f)
        {
            agent.ResetPath();
            Brain.pushState(Idle, OnIdleEnter);
        }
        if (vehicleNear)
        {
            Brain.pushState(Chase, null);
        }
    }

    void OnAttackEnter()
    {
        agent.ResetPath();
    }

    void Attack()
    {
        AttackCooldown -= Time.deltaTime;
        if (AttackCooldown <= 0)
        {
            playerCar.Hurt(10, 1);
            Debug.Log("Hit");
            AttackCooldown = 5f;
        }
    }
}
