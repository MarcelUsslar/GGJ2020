using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlackscreen : MonoBehaviour
{

	bool isBrightnessMax;

    private void Awake()
    {
        Screen.brightness = 0f;
    }

    void update()
	{

		CheckBrightness();

	}

    void CheckBrightness()
	{

        if (isBrightnessMax)
		{

			Debug.Log("Done");

		}

	}

}
