using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    private Vector2 playerPos;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        if (player.rotation.y == 0)
        {
            playerPos = new Vector2(player.position.x + 0.8f, player.position.y);
        } 
        else
        {
            playerPos = new Vector2(player.position.x - 0.8f, player.position.y);
        }

        
        Instantiate(item, playerPos, Quaternion.identity);
    }
}
