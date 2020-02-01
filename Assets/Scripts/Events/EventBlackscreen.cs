using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlackscreen : MonoBehaviour
{

	bool Flash;

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
