using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogSwipe : MonoBehaviour
{
    public GameObject TakeQuest;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject TextComplite;

    public Quest questScript;

    private int i = 0;

    private void Awake()
    {
        Debug.LogError("Я загрузился!");
    }
    void Start()
    {
        Debug.Log("Я загрузился!");
    }

    void Update()
    {
        if ((questScript.EndDialog == false) && (questScript.IsQuetsComplite == false))
        {
            TakeQuest.SetActive(false);
            Text1.SetActive(true);
            questScript.EndDialog = true;
            questScript.quests.SetActive(true);
        }

        if (questScript.IsQuetsComplite == true)
        {
            TextComplite.SetActive(true);
            questScript.quests.SetActive(false);
            questScript.IsQuetsCompliteEnd = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Text1.SetActive(false);
            Text2.SetActive(true);
            i++;


            if (i >= 2)
            {
                Time.timeScale = 1;
                questScript.EndDialog = true;
                questScript.dialog1.SetActive(false);
                Text1.SetActive(false);
                Text2.SetActive(false);
                TextComplite.SetActive(false);
            }
        }
    }
}
