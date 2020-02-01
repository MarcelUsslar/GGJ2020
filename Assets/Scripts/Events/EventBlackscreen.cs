using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlackscreen : MonoBehaviour
{

	bool Flash;

    private void Start()
    {
        Screen.brightness = 0f;
    }

    void update()
	{

		CheckFlash();

	}

    void CheckFlash()
	{

        if (Flash)
		{

			Debug.Log("Done");

		}

	}

}
