using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTooBright : MonoBehaviour
{

    private void Update()
    {
        if(Screen.brightness < 0.5f) {

            Debug.Log("Done");

        }
    }

}
