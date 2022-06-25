using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public bool EndDialog;
    public GameObject Dialog1;
    public GameObject Quests;
    public GameObject[] quests;
    [SerializeField]
    private static int count = 1;
    public bool IsQuetsComplite;

    private void Start()
    {
        var foundGameObjects = FindObjectsOfType<AI_Enemy>(true);
        Debug.Log(foundGameObjects + " : " + foundGameObjects.Length);
    }
    private void Update()
    {

        if (IsQuetsComplite == false) //Булевая переменная для закрывания квестов у NPC после их выполненния
        {
            foreach(GameObject quest in quests)
            {
                quest.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject quest in quests)
            {
                quest.SetActive(false);
            }
        }


        if (EndDialog == true)
        {
            Time.timeScale = 1;
            Dialog1.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0;
            Dialog1.SetActive(true);
        }
    }
}
