using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.IsFull[i] = false;
        }


        if (AreAllBooleansThisState(inventory.IsFull, false)) {
            Quest.haveHoney = false;
        }
        else
        {
            Quest.haveHoney = true;
        }

    }

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }

    bool AreAllBooleansThisState(bool[] arr, bool state)
    {
        bool answer = true;
        for (int i = 0; i < arr.Length; i++)
            {
            if (arr[i] != state)
            {
                answer = false;
                break;
            }
        }
        return answer;
    }
}
