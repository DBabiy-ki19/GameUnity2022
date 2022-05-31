using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform playerTr;
    private NavMeshAgent agent;
    public int maxHealth = 100;
    private int currentHealt;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        currentHealt = maxHealth;
        
    }

    private void FixedUpdate()
    {
        agent.SetDestination(playerTr.position);
    }
    public void TakeDamage(int damage)
    {
        currentHealt -= damage;

        if(currentHealt <= 0)
        {
            Die();
        }
    }
    

    void Die()
    {
        Destroy(gameObject);
    }
}
