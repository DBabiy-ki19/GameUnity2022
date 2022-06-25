using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSwipe : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    private bool isText1 = true;
    public Quest questScript;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if ((touch.phase == TouchPhase.Began) ||  (Input.GetMouseButtonDown(0)))*/
            if (Input.GetMouseButtonDown(0))
            {
                if (isText1 == true)
                {
                    isText1 = false;
                }
                else
                {
                    isText1 = true;
                    questScript.EndDialog = true;
                    questScript.Quests.SetActive(true);
                }
            }
            if (isText1 == true)
            {
                Text1.SetActive(true);
                Text2.SetActive(false);
            }
            else
            {
                Text1.SetActive(false);
                Text2.SetActive(true);
            }
        //}
    }
}
