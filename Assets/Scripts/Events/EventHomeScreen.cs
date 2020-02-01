using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHomeScreen : MonoBehaviour
{

    int numberOfTriggers;
    bool isDone;
    public static EventHomeScreen Instance;

    public GameObject downloadIcon;


    private void Awake()
    {
        Instance = this;

        GameObject[] allTriggers = GameObject.FindGameObjectsWithTag("Triggers");
        numberOfTriggers = allTriggers.Length;
        Debug.Log(numberOfTriggers);

    }


    private void Update()
    {
        CheckTriggers();
    }

    void CheckTriggers()
    {


            if (isDone)
        {

            Debug.Log("done");
            
        }


    }

    public void Reduce(int by)
    {

        numberOfTriggers = numberOfTriggers - by;

        Debug.Log("Marcel Wants to Know if you worked " + numberOfTriggers);

        if (numberOfTriggers == 0)
        {

            downloadIcon.transform.position = IconPress.positionOfDownloadButton;
            downloadIcon.SetActive(true);

        }


    }

    public void CheckIfDone()
    {

        if (numberOfTriggers == 0)
        {

            isDone = true;

        }

        else
        {

            isDone = false;

        }
    }

}
