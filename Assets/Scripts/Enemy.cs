using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealt;

    private void Start()
    {
        currentHealt = maxHealth;
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
