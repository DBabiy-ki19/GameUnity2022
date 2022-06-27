using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSwipe : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    public GameObject TextComplite;

    public Quest questScript;

    private int i = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((questScript.EndDialog == false) && (questScript.IsQuetsComplite == false))
        {
            Text1.SetActive(true);
            questScript.EndDialog = true;
            questScript.quests.SetActive(true);
        }

        if (questScript.IsQuetsComplite == true)
        {
            TextComplite.SetActive(true);
            questScript.IsQuetsCompliteEnd = true;
            questScript.quests.SetActive(false);
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
