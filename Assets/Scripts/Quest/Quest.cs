using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest : MonoBehaviour
{
    public bool EndDialog = false;
    public bool IsQuetsComplite = false;
    public bool IsQuetsCompliteEnd = false;

    public GameObject dialog1;
    public GameObject quests;
    public GameObject[] questsArr;

    public static int countBeeKilled;
    public static bool haveHoney = false;

    private int needCountBeeKilled;
    private Color questComplite;
    private Color questNoComplite;


    private void Start()
    {
        questComplite = new Color(51 / 255.0f, 190 / 255.0f, 30 / 255.0f);
        questNoComplite = new Color(255 / 255.0f, 84 / 255.0f, 74 / 255.0f);
        needCountBeeKilled = FindObjectsOfType<AI_Enemy>(true).Length;
    }
    private void Update()
    {
        foreach (Image b in quests.GetComponentsInChildren<Image>())
        {
            if (((needCountBeeKilled == countBeeKilled) && (b.name == "KillBee") && (b.color != questComplite)) || 
               ((haveHoney == true) && (b.name == "TakeHoney") && (b.color != questComplite)))
            {
                b.color = questComplite;
            }
            if ((haveHoney == false) && (b.name == "TakeHoney"))
            {
                b.color = questNoComplite;
            }
        }

        if ((quests.GetComponentsInChildren<Image>()[0].color == questComplite) && (quests.GetComponentsInChildren<Image>()[1].color == questComplite))
        {
            IsQuetsComplite = true;
        }
        else 
        {
            IsQuetsComplite = false; 
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player") && (IsQuetsCompliteEnd == false) && (IsQuetsComplite == true))
        {
            Time.timeScale = 0;
            dialog1.SetActive(true);
        }

        if ((collision.tag == "Player") && (EndDialog == false))
        {
            Time.timeScale = 0;
            dialog1.SetActive(true);
        }
    }
}
