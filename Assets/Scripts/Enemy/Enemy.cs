using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{

    [Header("Что выпадет из врага: ")]
    public GameObject item;
    [Header("Здоровье")]
    public int maxHealth = 100;
    private int currentHealt;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        currentHealt = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("TakeDamage", typeof(Material)) as Material;
        matDefault = spriteRenderer.material;
    }

    public void TakeDamage(int damage)
    {
        spriteRenderer.material = matBlink;
        currentHealt -= damage;
        if(currentHealt <= 0)
        {
            if (gameObject.name == "Beehive")
            {
                LootFromTheEnemy();
            }
            Die();
        }
        else
        {
            Invoke("ResetMaterial", 0.2f);
        }
    }

    void Die()
    {
        if (gameObject.name != "Beehive")
        {
            Quest.countBeeKilled++;
            Debug.Log(Quest.countBeeKilled);
        }
        Debug.Log("Кого уничтожили: " + gameObject.name);
        Destroy(gameObject);
    }

    void LootFromTheEnemy()
    {
        Instantiate(item, gameObject.transform.position, Quaternion.identity);
    }

    void ResetMaterial()
    {
        spriteRenderer.material = matDefault;
    }

}
