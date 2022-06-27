using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public bool EndDialog;
    public bool IsQuetsComplite;

    public GameObject dialog1;
    public GameObject quests;
    public GameObject[] questsArr;

    public static int countBeeKilled;
    public static bool haveHoney = false;

    private static int count = 1;
    private int needCountBeeKilled;



    private void Start()
    {
        needCountBeeKilled = FindObjectsOfType<AI_Enemy>(true).Length;
    }
    private void Update()
    {
        foreach (Image b in quests.GetComponentsInChildren<Image>())
        {
            if (((needCountBeeKilled == countBeeKilled) && (b.name == "KillBee") && (b.color != new Color(51 / 255.0f, 190 / 255.0f, 30 / 255.0f))) || 
               ((haveHoney == true) && (b.name == "TakeHoney") && (b.color != new Color(51 / 255.0f, 190 / 255.0f, 30 / 255.0f))))
            {
                b.color = new Color(51 / 255.0f, 190 / 255.0f, 30 / 255.0f);
            }
            if ((haveHoney == false) && (b.name == "TakeHoney"))
            {
                b.color = new Color(255 / 255.0f, 84 / 255.0f, 74 / 255.0f);
            }
        }


        if (IsQuetsComplite == false) //Булевая переменная для закрывания квестов у NPC после их выполненния
        {
            foreach(GameObject quest in questsArr)
            {
                quest.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject quest in questsArr)
            {
                quest.SetActive(false);
            }
        }


        if (EndDialog == true)
        {
            Time.timeScale = 1;
            dialog1.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0;
            dialog1.SetActive(true);
        }
    }
}
