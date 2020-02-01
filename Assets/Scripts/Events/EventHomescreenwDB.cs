using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHomescreenwDB : MonoBehaviour
{
    public GameObject downloadIcon;
    public GameObject settingsButton;
    public static EventHomescreenwDB Instance;

    void Awake()
    {
        Instance = this;
        //downloadIcon.transform.position = IconPress.positionOfDownloadButton;
    }

    public void Downloaded()
    {

        settingsButton.SetActive(true);
 

    }


}
